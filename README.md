# LaunchboxLastPlayedPlugin

**A LaunchBox plugin that automatically generates a .bat file and a history log for the last game played.**

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

This project is licensed under the MIT License. Use of the LaunchBox API is subject to the terms provided by Unbroken Software, LLC.

## Use Case

If you have a Sunshine/Apollo streaming setup, you can use BigBox/LaunchBox to browse and decide on a game. If you find yourself launching the same game constantly, this plugin allows you to bypass BigBox/LaunchBox and skip straight to the game. It creates a last_played.bat file with the same execution instructions as the last game launched with BigBox/LaunchBox. Just add that .bat file as an application in your Sunshine/Apollo settings.

## Features 🚀️

- Automatic Batch Generation: Creates a last_played.bat in a Scripts folder that can re-launch your most recent game.
- Launch History: Maintains a last_played.log with timestamps and full launch commands.
- Smart Pathing: Supports emulated games (with full arguments), standalone PC games, Steam (via URI), and Epic Games.
- Auto-Cleanup: Automatically removes log entries older than one year to keep the file size small.

## Project Setup 🔧

### Prerequisites

- JetBrains Rider (or Visual Studio)
- .NET 9.0 SDK (required for LaunchBox 13.19+)

### How to Build 📦

1. Open the .sln file in JetBrains Rider (or Visual Studio).
2. Build the solution.
3. The output DLL will be located in bin/Release/net9.0/LaunchboxLastPlayedPlugin.dll.

## Installation 📥

- Download the latest LaunchboxLastPlayedPlugin.zip from the Releases page.
- Navigate to your LaunchBox installation folder.
- Extract the LaunchboxLastPlayedPlugin.zip into your LaunchBox\Plugins
- Restart LaunchBox.

## Usage 📖

Once installed, the plugin works automatically in the background:

- **Trigger**: Simply launch any game from LaunchBox or Big Box.
- **Output**: A new folder named Scripts will be created in your LaunchBox root containing:
  - last_played.bat: Run this to instantly relaunch your last game.
  - last_played.log: A history of your game launches.
- Add the last_played.bat file as an Application in Sunshine/Apollo settings.
  - [Here](assets/last_played.png) is a poster you can use.
