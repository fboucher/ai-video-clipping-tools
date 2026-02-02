#!/usr/bin/env dotnet run
// Check Status - Check the status of a Reka clip generation job
//
// Run with: dotnet run check_status.cs

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
Console.WriteLine("  Reka Job Status Checker");
Console.WriteLine(new string('=', 40));

Console.Write("\nEnter the Job ID: ");
var jobId = Console.ReadLine()?.Trim();

if (string.IsNullOrEmpty(jobId))
{
    Console.WriteLine("Error: No Job ID provided.");
    return;
}

Console.WriteLine("\n" + new string('-', 40));
Console.WriteLine("Press Ctrl+C to stop checking");
Console.WriteLine(new string('-', 40));

var checkCount = 0;
var running = true;

Console.CancelKeyPress += (sender, e) =>
{
    Console.WriteLine("\n\nStopped by user.");
    running = false;
    e.Cancel = true;
};

using var client = new HttpClient();
client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

while (running)
{
    checkCount++;
    Console.WriteLine($"\nCheck #{checkCount} - Fetching status...");

    try
    {
        var response = await client.GetAsync($"{BASE_URL}/{jobId}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var status = root.TryGetProperty("status", out var statusEl) ? statusEl.GetString() : "unknown";
        Console.WriteLine($"Status: {status}");

        if (status == "completed")
        {
            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("  CLIP(S) READY!");
            Console.WriteLine(new string('=', 40));

            if (root.TryGetProperty("output", out var outputEl))
            {
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
                if (i == 0) Console.WriteLine("No clips in output.");
            }
            break;
        }
        else if (status == "failed")
        {
            var error = root.TryGetProperty("error_message", out var errEl) ? errEl.GetString() : "Unknown error";
            Console.WriteLine($"Job failed: {error}");
            break;
        }
        else
        {
            Console.WriteLine("Waiting 30 seconds before next check...");
            await Task.Delay(30000);
        }
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Connection error: {ex.Message}. Retrying in 30 seconds...");
        await Task.Delay(30000);
    }
}

Console.WriteLine(new string('-', 40));
Console.WriteLine("Done!");
