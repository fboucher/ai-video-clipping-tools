"""
Check Status - Check the status of a Reka clip generation job

This script polls the Reka API every 30 seconds to check if your
clip is ready, then displays the results.
"""

import os
import time
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


def check_job_status(job_id, api_key):
    """Check the status of a clip generation job."""
    url = f"{BASE_URL}/{job_id}"
    headers = {"X-Api-Key": api_key}

    response = requests.get(url, headers=headers)
    response.raise_for_status()
    return response.json()


def display_results(data):
    """Display the clip results when job is completed."""
    print("\n" + "=" * 40)
    print("  CLIP(S) READY!")
    print("=" * 40)

    output = data.get("output", [])
    if not output:
        print("No clips in output.")
        return

    for i, clip in enumerate(output, 1):
        print(f"\n--- Clip {i} ---")
        print(f"Title: {clip.get('title', 'N/A')}")
        print(f"URL: {clip.get('video_url', 'N/A')}")
        print(f"Caption: {clip.get('caption', 'N/A')}")
        hashtags = clip.get("hashtags", [])
        if hashtags:
            print(f"Hashtags: {' '.join(hashtags)}")


def main():
    """Main function - entry point of the script."""
    print("=" * 40)
    print("  Reka Job Status Checker")
    print("=" * 40)

    # Get API key
    api_key = get_api_key()

    # Ask user for the job ID
    job_id = input("\nEnter the Job ID: ").strip()

    if not job_id:
        print("Error: No Job ID provided.")
        exit(1)

    print("\n" + "-" * 40)
    print("Press Ctrl+C to stop checking")
    print("-" * 40)

    check_count = 0

    try:
        while True:
            check_count += 1
            print(f"\nCheck #{check_count} - Fetching status...")

            try:
                data = check_job_status(job_id, api_key)
                status = data.get("status", "unknown")
                print(f"Status: {status}")

                if status == "completed":
                    display_results(data)
                    break
                elif status == "failed":
                    error = data.get("error_message", "Unknown error")
                    print(f"Job failed: {error}")
                    break
                else:
                    print("Waiting 30 seconds before next check...")
                    time.sleep(30)

            except requests.exceptions.HTTPError as e:
                print(f"API Error: {e}")
                break
            except requests.exceptions.ConnectionError:
                print("Connection error. Retrying in 30 seconds...")
                time.sleep(30)

    except KeyboardInterrupt:
        print("\n\nStopped by user.")

    print("-" * 40)
    print("Done!")


# This runs when you execute the script directly
if __name__ == "__main__":
    main()
