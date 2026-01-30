"""
Simple Clip Generator - Create short clips from YouTube videos using Reka AI

This script demonstrates how to use the Reka Clips API to generate
short video clips from YouTube videos.
"""

import os
import json
import requests


# API Configuration
BASE_URL = "https://vision-agent.api.reka.ai/v1/clips"


def get_api_key():
    """Get the API key from environment variable."""
    api_key = os.environ.get("REKA_API_KEY")
    if not api_key:
        print("Error: REKA_API_KEY environment variable is not set.")
        print("Please set it with: export REKA_API_KEY=your_api_key")
        exit(1)
    return api_key


def stream_events(response):
    """
    Read streaming events from the API response.
    The API sends events as lines starting with 'data: ' followed by JSON.
    """
    last_data = None
    for line in response.iter_lines():
        if not line:
            continue
        decoded = line.decode("utf-8")
        if decoded.startswith("data: "):
            try:
                data = json.loads(decoded[6:])
                # Only yield if data changed (avoid duplicates)
                if data != last_data:
                    yield data
                last_data = data
            except json.JSONDecodeError:
                pass


def create_clip(video_url, api_key):
    """
    Call the Reka API to create a clip from the video.
    Streams the response and prints status updates.
    """
    # Prepare the request
    payload = {
        "video_urls": [video_url],
        "prompt": "Create an engaging video clip highlighting the best moments",
        "generation_config": {
            "template": "moments",
            "num_generations": 1,
            "min_duration_seconds": 0,
            "max_duration_seconds": 30,
        },
        "rendering_config": {
            "subtitles": True,
            "aspect_ratio": "9:16",
        },
        "stream": True,
    }

    headers = {
        "Content-Type": "application/json",
        "X-Api-Key": api_key,
    }

    print("\nStarting clip generation...")
    print("-" * 40)

    job_id_shown = False

    try:
        # Make the API request with streaming enabled
        with requests.post(BASE_URL, headers=headers, json=payload, stream=True) as response:
            response.raise_for_status()

            # Process each event as it arrives
            for event in stream_events(response):
                # Show job ID as soon as available (important for recovery)
                if not job_id_shown and "id" in event:
                    print(f"Job ID: {event['id']}")
                    print("(Save this ID to retrieve results if interrupted)")
                    print("-" * 40)
                    job_id_shown = True

                status = event.get("status", "unknown")
                print(f"Status: {status}")

                # Show clip details when job is completed
                if status == "completed" and "output" in event:
                    print("\n" + "=" * 40)
                    print("  CLIP(S) READY!")
                    print("=" * 40)
                    for i, clip in enumerate(event["output"], 1):
                        print(f"\n--- Clip {i} ---")
                        print(f"Title: {clip.get('title', 'N/A')}")
                        print(f"URL: {clip.get('video_url', 'N/A')}")
                        print(f"Caption: {clip.get('caption', 'N/A')}")
                        hashtags = clip.get("hashtags", [])
                        if hashtags:
                            print(f"Hashtags: {' '.join(hashtags)}")

    except requests.exceptions.HTTPError as e:
        print(f"API Error: {e}")
    except requests.exceptions.ConnectionError:
        print("Error: Could not connect to the API. Check your internet connection.")


def main():
    """Main function - entry point of the script."""
    print("=" * 40)
    print("  Reka Clip Generator")
    print("=" * 40)

    # Get API key
    api_key = get_api_key()

    # Ask user for the video URL
    video_url = input("\nEnter the YouTube video URL: ").strip()

    if not video_url:
        print("Error: No URL provided.")
        exit(1)

    # Create the clip
    create_clip(video_url, api_key)

    print("-" * 40)
    print("Done!")


# This runs when you execute the script directly
if __name__ == "__main__":
    main()
