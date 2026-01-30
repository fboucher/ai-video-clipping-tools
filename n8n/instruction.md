
Create a very simple terminal application that calls Reka Clipping API passing a video URL and a prompt to generate a short clip. The application should stream the events from the API and print them to the console as they are received.

BASE_URL = https://vision-agent.api.reka.ai/v1/clips
API_KEY = should be in an environment variable named REKA_API_KEY or in a configuration file.

Here is an example implementation to know how to call the API and stream the events:

```python
import requests, json
from typing import Generator, Dict, Any

endpoint_url = f"{BASE_URL}/v1/clips"
payload = {
    "video_urls": ["<YOUR_VIDEO_URL_HERE>"],
    "prompt": "Create an engaging short video highlighting the best moments",
    "generation_config": {
        "template": "moments",
        "num_generations": 1,
        "min_duration_seconds": 0,
        "max_duration_seconds": 90,
    },
    "rendering_config": {
        "subtitles": True,
        "aspect_ratio": "9:16",
    },
    "stream": True,
}
headers = {
    "Content-Type": "application/json",
    "X-Api-Key": API_KEY,
}

def stream_events(response) -> Generator[Dict[str, Any], None, None]:
    last_data = None
    for line in response.iter_lines():
        if not line:
            continue
        decoded = line.decode("utf-8")
        if decoded.startswith("data: "):
            try:
                data = json.loads(decoded[6:])
                if data != last_data:
                    yield data
                last_data = data
            except json.JSONDecodeError:
                pass

with requests.post(endpoint_url, headers=headers, json=payload, stream=True) as r:
    r.raise_for_status()
    for event in stream_events(r):
        print(json.dumps(event, indent=2))

```


Streaming event example output:

```json
{
  "id": "ada0bc6f-3540-4bb2-baa0-308e9fcf1d06",
  "status": "preprocessing",
  "created_at": "2026-01-18T19:28:49.839838+00:00",
  "updated_at": "2026-01-18T19:29:33.202844+00:00",
  "generation_config": {
    "template": "moments",
    "num_generations": 1,
    "min_duration_seconds": 0,
    "max_duration_seconds": 90
  },
  "rendering_config": {
    "subtitles": true,
    "aspect_ratio": "9:16"
  },
  "video_urls": [
    "https://www.youtube.com/watch?v=9JbE_HtiewI"
  ],
  "prompt": "Create an engaging short video highlighting the best moments"
}

```