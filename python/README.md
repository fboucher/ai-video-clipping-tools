# Python Clip Tools

Simple Python scripts to create video clips using the Reka AI API.

## Setup

1. Install requests: `pip install requests`
2. Set your API key: `export REKA_API_KEY=your_key_here`

## Scripts

### clip_generator.py

Creates a clip from a YouTube video.

```bash
python3 clip_generator.py
```


Enter the video URL when prompted. The script streams progress and shows the Job ID (save it if interrupted).

**Sample Output:**

![clip generator output](assets/clip_generator.png)

### check_status.py

Check the status of a clip job using its Job ID.

```bash
python3 check_status.py
```

Enter the Job ID when prompted. Checks every 30 seconds until complete. Press `Ctrl+C` to stop.

![Check status output](assets/check_status.png)

## Links

- [Reka API Docs](https://docs.reka.ai/vision/highlight-reel-generation)
- [Reka Discord](https://link.reka.ai/discord)
