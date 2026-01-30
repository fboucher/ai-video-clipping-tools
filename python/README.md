# Python Clip Generator

A simple Python script that creates short video clips from YouTube videos using the Reka AI API.

## What This Does

This script takes a YouTube video URL and uses AI to automatically create a short, engaging clip (up to 90 seconds) with subtitles. Perfect for creating social media content!

## Prerequisites

- Python 3.7 or higher
- A Reka API key (get one at [reka.ai](https://reka.ai))

## Installation

1. **Install the required library**

   Open your terminal and run:

   ```bash
   pip install requests
   ```

2. **Set your API key**

   You need to set the `REKA_API_KEY` environment variable.

   On Mac/Linux:

   ```bash
   export REKA_API_KEY=your_api_key_here
   ```

   On Windows (Command Prompt):

   ```cmd
   set REKA_API_KEY=your_api_key_here
   ```

   On Windows (PowerShell):

   ```powershell
   $env:REKA_API_KEY="your_api_key_here"
   ```

## How to Run

1. Open your terminal
2. Navigate to this folder
3. Run the script:

   ```bash
   python clip_generator.py
   ```

4. When prompted, paste the YouTube video URL and press Enter

## Example

```
==========================================
  Reka Clip Generator
==========================================

Enter the YouTube video URL: https://www.youtube.com/watch?v=example

Starting clip generation...
----------------------------------------
Status: preprocessing
Status: analyzing
Status: generating
Status: rendering

Clip(s) ready!
  Clip 1: https://example.com/your-clip.mp4
----------------------------------------
Done!
```

## Troubleshooting

**"REKA_API_KEY environment variable is not set"**

Make sure you set the environment variable before running the script. See the Installation section above.

**"Could not connect to the API"**

Check your internet connection and try again.

## Learn More

- [Reka Clips API Documentation](https://docs.reka.ai/vision/highlight-reel-generation)
- [Reka Community Discord](https://link.reka.ai/discord)
