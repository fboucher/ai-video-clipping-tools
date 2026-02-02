#!/usr/bin/env dotnet run
// Simple Clip Generator - Create short clips from YouTube videos using Reka AI
//
// Run with: dotnet run clip_generator.cs

using System.Text;
using System.Text.Json;

const string BASE_URL = "https://vision-agent.api.reka.ai/v1/clips";

var apiKey = Environment.GetEnvironmentVariable("REKA_API_KEY");
if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Error: REKA_API_KEY environment variable is not set.");
    Console.WriteLine("Please set it with: export REKA_API_KEY=your_api_key");
    return;
}

Console.WriteLine(new string('=', 40));
Console.WriteLine("  Reka Clip Generator");
Console.WriteLine(new string('=', 40));

Console.Write("\nEnter the YouTube video URL: ");
var videoUrl = Console.ReadLine()?.Trim();

if (string.IsNullOrEmpty(videoUrl))
{
    Console.WriteLine("Error: No URL provided.");
    return;
}

Console.WriteLine("\nStarting clip generation...");
Console.WriteLine(new string('-', 40));

// Build JSON payload as string
var jsonPayload = $$"""
{
    "video_urls": ["{{videoUrl}}"],
    "prompt": "Create an engaging video clip highlighting the best moments",
    "generation_config": {
        "template": "moments",
        "num_generations": 1,
        "min_duration_seconds": 0,
        "max_duration_seconds": 30
    },
    "rendering_config": {
        "subtitles": true,
        "aspect_ratio": "9:16"
    },
    "stream": true
}
""";

var jobIdShown = false;
string? lastJson = null;

using var client = new HttpClient();
client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
client.Timeout = TimeSpan.FromMinutes(10);

try
{
    var request = new HttpRequestMessage(HttpMethod.Post, BASE_URL)
    {
        Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
    };

    using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"API Error: HTTP {(int)response.StatusCode} {response.ReasonPhrase}");
        return;
    }

    using var stream = await response.Content.ReadAsStreamAsync();
    using var reader = new StreamReader(stream);

    string? line;
    while ((line = await reader.ReadLineAsync()) != null)
    {
        if (string.IsNullOrWhiteSpace(line) || !line.StartsWith("data: "))
            continue;

        var json = line.Substring(6);
        if (json == lastJson) continue;
        lastJson = json;

        JsonDocument doc;
        try
        {
            doc = JsonDocument.Parse(json);
        }
        catch
        {
            continue;
        }

        var root = doc.RootElement;

        // Show job ID
        if (!jobIdShown && root.TryGetProperty("id", out var idEl))
        {
            Console.WriteLine($"Job ID: {idEl.GetString()}");
            Console.WriteLine("(Save this ID to retrieve results if interrupted)");
            Console.WriteLine(new string('-', 40));
            jobIdShown = true;
        }

        // Show status
        var status = root.TryGetProperty("status", out var statusEl) ? statusEl.GetString() : "unknown";
        Console.WriteLine($"Status: {status}");

        // Show results when completed
        if (status == "completed" && root.TryGetProperty("output", out var outputEl))
        {
            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("  CLIP(S) READY!");
            Console.WriteLine(new string('=', 40));

            var i = 0;
            foreach (var clip in outputEl.EnumerateArray())
            {
                i++;
                Console.WriteLine($"\n--- Clip {i} ---");
                if (clip.TryGetProperty("title", out var t)) Console.WriteLine($"Title: {t.GetString()}");
                if (clip.TryGetProperty("video_url", out var u)) Console.WriteLine($"URL: {u.GetString()}");
                if (clip.TryGetProperty("caption", out var c)) Console.WriteLine($"Caption: {c.GetString()}");
                if (clip.TryGetProperty("hashtags", out var h))
                {
                    var tags = new List<string>();
                    foreach (var tag in h.EnumerateArray())
                    {
                        var s = tag.GetString();
                        if (s != null) tags.Add(s);
                    }
                    if (tags.Count > 0) Console.WriteLine($"Hashtags: {string.Join(" ", tags)}");
                }
            }
        }
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Error: Could not connect to the API. {ex.Message}");
}
catch (TaskCanceledException)
{
    Console.WriteLine("Error: Request timed out.");
}

Console.WriteLine(new string('-', 40));
Console.WriteLine("Done!");
