# Legend of Zelda: Ocarina of Time - State Extractor

This project is an "auto-tracker" which attempts to read the memory of Ocarina
of Time, track the game state, and emit it in real time. This allows randomizer
trackers trackers to automatically update their state in accordance with the
actual game.

[Demo video.](https://user-images.githubusercontent.com/11010321/113808278-8c140c00-971a-11eb-940f-ac97b2d0244d.mp4)

## Design

This project is a proof-of-concept [BizHawk](https://github.com/TASVideos/BizHawk)
external tool which starts an API server at `localhost:5000` that returns the state
of the player's inventory and even supports an event stream which live-updates as
the player acquires items. This server is intended to be able to support any UI
client the user may choose.

There is an Angular client which is built and served seperately from the tool,
and consumes the event stream API in real-time. This is more of an example UI
since the point of the extractor service is to be independent of any client UI.

## Local Development

First, download
[BizHawk 2.5.2](https://github.com/TASVideos/BizHawk/releases/tag/2.5.2) and
place it at `BizHawk/` in the root directory of this repository. Other versions
are not tested and may not work.

If using Visual Studio, you should be able to just run the `OotStateExtractor`
project which will build the plugin, copy it to the local `BizHawk/` directory,
and start it.  In BizHawk, go to `Tools > External Tools > OoT State Extractor`
to start the plugin.

If you are not using Visual Studio and building within Linux or WSL2, make sure
the `dotnet` and `mono` commands are installed from .NET 5.0.

If you are using WSL 2, you'll also need to set up an XServer on Windows in
order to display the graphical BizHawk application. VcXsrv is one such
application which must be installed (and started) for BizHawk to display.

Build and run the tool with the following command:

```shell
dotnet build && BizHawk/EmuHawkMono.sh --mono-no-redirect --open-ext-tool-dll=OotStateExtractor
```

This will start BizHawk and open the state extractor tool.

WARNING: Do **NOT** open the log window! It will crash the server. See:
https://github.com/TASVideos/BizHawk/issues/2694.

After opening the tool window, it will be blank but a server is running in the background at
`http://localhost:5000/`. While that is running, `cd` into `OotStateExtractorClient` and run:

```shell
npm start
```

This will start an Angular app at `http://localhost:4200/` which will connect to the API server
and display the game's `SaveContext`, updating in real-time.
