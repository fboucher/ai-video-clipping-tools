[![Reka AI](https://img.shields.io/badge/Power%20By-2E2F2F?style=flat&logo=data%3Aimage%2Fsvg%2Bxml%3Bbase64%2CPD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4KPHN2ZyBpZD0iTGF5ZXJfMSIgZGF0YS1uYW1lPSJMYXllciAxIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCA2NjYuOTQgNjgxLjI2Ij4KICA8ZGVmcz4KICAgIDxzdHlsZT4KICAgICAgLmNscy0xIHsKICAgICAgICBmaWxsOiBub25lOwogICAgICB9CgogICAgICAuY2xzLTIgewogICAgICAgIGZpbGw6ICNmMWVlZTc7CiAgICAgIH0KICAgIDwvc3R5bGU%2BCiAgPC9kZWZzPgogIDxyZWN0IGNsYXNzPSJjbHMtMSIgeD0iLS4yOSIgeT0iLS4xOSIgd2lkdGg9IjY2Ny4yMiIgaGVpZ2h0PSI2ODEuMzMiLz4KICA8Zz4KICAgIDxwYXRoIGNsYXNzPSJjbHMtMiIgZD0iTTMxOC4zNCwwTDgyLjY3LjE2QzM2Ljg1LjE5LS4yOSwzNy4zOC0uMjksODMuMjd2MjM1LjEyaDc0LjkzVjcxLjc1aDI0My43M1YwaC0uMDNaIi8%2BCiAgICA8cGF0aCBjbGFzcz0iY2xzLTIiIGQ9Ik03Mi45NywzNjIuOTdIMHYzMTguMTZoNzIuOTd2LTMxOC4xNloiLz4KICAgIDxwYXRoIGNsYXNzPSJjbHMtMiIgZD0iTTMxNS4zMywzNjIuODRoLTk5LjEzbC0xMDkuNSwxMDcuMjljLTEzLjk1LDEzLjY4LTIxLjgyLDMyLjM3LTIxLjgyLDUxLjkyczcuODYsMzguMjQsMjEuODIsNTEuOTJsMTA5LjUsMTA3LjI5aDEwMS42M2wtMTYyLjQ1LTE2MS43MiwxNTkuOTUtMTU2LjY3di0uMDNaIi8%2BCiAgICA8cGF0aCBjbGFzcz0iY2xzLTIiIGQ9Ik0zNDguNTksODIuOTJ2MTUyLjIzYzAsNDUuOTIsMzcuMTYsODMuMTEsODMuMDUsODMuMTFoMjMwLjI4di03MS43OGgtMjQwLjMzVjg1Ljg3YzAtNy43NCw2LjI4LTE0LjA2LDE0LjA1LTE0LjA2aDE0NC4zMmM3Ljc0LDAsMTQuMDUsNi4yOCwxNC4wNSwxNC4wNiwwLDUuOS0zLjcxLDExLjE3LTkuMjMsMTMuMmwtMTQ3LjQ1LDU2LjIzdjcwLjczbDE3NC41Ny02Mi4yYzMzLjA0LTExLjgsNTUuMTEtNDMuMTMsNTUuMTEtNzguMjZ2LTIuNjdDNjY3LDM3LDYyOS44LS4xOSw1ODMuOTUtLjE5aC0xNTIuMjdjLTQ1Ljg5LDAtODMuMDUsMzcuMTktODMuMDUsODMuMTFoLS4wM1oiLz4KICAgIDxwYXRoIGNsYXNzPSJjbHMtMiIgZD0iTTY2Ni45NCw1OTguMTJ2LTE1Mi4yM2MwLTQ1Ljg5LTM3LjE2LTgzLjExLTgzLjA1LTgzLjExaC0yMzAuMjh2NzEuNzhoMjQwLjMzdjE2MC42MWMwLDcuNzQtNi4yOCwxNC4wNi0xNC4wNSwxNC4wNmgtMTQ0LjMxYy03Ljc0LDAtMTQuMDUtNi4yOC0xNC4wNS0xNC4wNiwwLTUuOSwzLjcxLTExLjE3LDkuMjMtMTMuMmwxNDcuNDUtNTYuMjN2LTcwLjczbC0xNzQuNTcsNjIuMmMtMzMuMDQsMTEuOC01NS4xMSw0My4xMy01NS4xMSw3OC4yNnYyLjY3YzAsNDUuOTIsMzcuMTYsODMuMTEsODMuMDUsODMuMTFoMTUyLjI3YzQ1Ljg5LDAsODMuMDUtMzcuMTksODMuMDUtODMuMTFoLjAzWiIvPgogIDwvZz4KPC9zdmc%2B&logoSize=auto&labelColor=2E2F2F&color=F1EEE7)](https://reka.ai/)

# Auto-clip YouTube videos

Each times there a new video on a specific YouTube channel, create a short clip from it using AI. This repository contains two different approaches using **n8n** to automate the process of clipping YouTube videos using Reka Clips API.

## Single Workflows - Looping

This is composed of a single **n8n** workflows that gets trigger by new video publish. It then create a clipping job and wait until done. Once the clip is ready, it sends an email with all the clip info.

![single n8n workflow](assets/auto-clip-Loop-http.png)

### Installation

1. Create a new workflow in n8n. From the top right corner, select the hamburger menu (three dots) > import from File and select the [auto-clip-with-http.json](n8n/auto-clip-with-http.json) file located in the `n8n` folder.

  ![import file](assets/import-file.png)

1. Follow the instructions in the workflows to configure the nodes
1. If you don't already have an API Key, get your free API key from [Reka Platform](https://link.reka.ai/free).

---

## Dual Workflows - Async Processing

This is composed of 2 **n8n** workflows:

**Submit Reel Creation**: This workflow is triggered when a new video is published on a specific YouTube channel. It sends the video URL to Reka Clips API to create a short clip from the video.

![flow_create-clip-job](assets/flow_create-clip-job.png)

**Check Reel Status**: This workflow checks the status of the clip creation process. Once the clip is ready, it retrieves the clip URL and shares it.

![flow_check-status](assets/flow_check-status.png)

### Installation: Data Table, Workflows

- Create a Data table named `videos` with the following columns:
  - `video_title` (string)
  - `video_url` (string)
  - `job_id` (string)
  - `job_status` (string)

  ![videos table](assets/table_videos.png)

### Workflow: Submit Reel Creation

1. Create a new workflow in n8n. From the top right corner, select the hamburger menu (three dots) > import from File and select the [Submit Reel Creation.json](n8n/Submit%20Reel%20Creation.json) file located in the `n8n` folder.
1. When New Video (trigger):
   - Change the `YouTube channel ID` of the Feed URL for the channel you want to monitor.
1. Create Reel Creation Job:
   - If you don't already have an API Key, get your free API key from [Reka Platform](https://link.reka.ai/free).
   - Add your `API Key` to the field **Bearer Token** in the Bearer Auth.
   - If needed, update the JSON parameters to customize the clip creation. By default it looks like this:

   ```json
    {
        "video_urls": ["{{ $json.link }}"],
        "prompt": "Create an engaging short video highlighting the best moments",
        "generation_config": {
            "template": "moments",
            "num_generations": 1,
            "min_duration_seconds": 0,
            "max_duration_seconds": 30
        },
        "rendering_config": {
            "subtitles": true,
            "aspect_ratio": "9:16"
        }
    }
   ```

   This will create one short vertical clip (9:16) up to 30 seconds long, with subtitles, using the "moments" template.

1. Insert row:
   - Make sure there is no error.

After saving the workflow, activate it to start monitoring the YouTube channel for new videos.

### Flow: Check Reel status

1. Create a new workflow in n8n. From the top right corner, select the hamburger menu (three dots) > import from File and select the [Check Reel status.json](n8n/Check%20Reel%20status.json) file located in the `n8n` folder.
1. Trigger:
   - By default the trigger is manual, I prefer this when testing. Once you are happy with your setup, change it to a time trigger to run periodically (e.g., every 15-30 minutes). Checking too frequently may lead to hitting rate limits.
1. Get Job Status:
   - Same as before, add your `API Key` to the field **Bearer Token** in the Bearer Auth.
1. Append row in sheet:
   - Here I'm using Google Sheets to log the results, but you can use any other method to store or share the clip URL. (email, Slack, etc.) To use Google Sheets, make sure to set up the Google Sheets credentials in n8n.
   - Update the `Document`, `Sheet` and `Columns` fields to match your Google Sheets setup.
   ![Google sheet example](assets/sheet.png)

## Questions or issues

If you have any questions or run into issues, feel free to open an issue. You can also join the [Reka Community Discord](https://link.reka.ai/discord) for support and discussions.

## Documentation

Documentation for Reka Clips API can be found here: [Clip Generation API (Reka Clip](https://docs.reka.ai/vision/highlight-reel-generation)

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
