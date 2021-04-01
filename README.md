# Legend of Zelda: Ocarina of Time - State Extractor

This project attempts to read the memory of Ocarina of Time, track the game
state, and emit it in real time. This allows randomizer trackers trackers to
automatically update their state in accordance with the actual game.

## Local Development

First, download
[BizHawk 2.5.2](https://github.com/TASVideos/BizHawk/releases/tag/2.5.2) and
place it at `BizHawk/` in the root directory of this repository. Other versions
are not tested and may not work.

If using Visual Studio, you should be able to just run the `OotStateExtractor`
project which will build the plugin, copy it to the local `BizHawk/` directory,
and start it.  In BizHawk, go to `ools > External Tools > OoT State Extractor`
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
