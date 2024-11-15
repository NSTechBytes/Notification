
# Rainmeter Notification Plugin

This Rainmeter plugin provides a way to display custom notifications with configurable titles, text, icons, sounds, and actions. It can be triggered via Rainmeter skin actions, and can execute custom commands when the notification is clicked.

## Features

- Display notifications with customizable **Title**, **Text**, and **Icon**.
- Play **Sound** when the notification appears.
- Execute a custom **action** when the notification is clicked.
- Supports both system icons and custom icons (`.ico` files).
- Configurable notification behavior through the Rainmeter skin.

## Installation

### 1. Clone or Download the Plugin

Clone this repository or download the plugin DLL file.

### 2. Add the Plugin to Rainmeter

Place the compiled `Notification.dll` file into your Rainmeter `Plugins` directory:
```
<Rainmeter Directory>\Plugins
```

### 3. Use the Plugin in Your Skin

In your Rainmeter skin `.ini` file, you can define the notification using the `[mNotification]` measure. Here is an example configuration:

```ini
[mNotification]
Measure=Plugin
Plugin=Notification
Title=This is Title
Text=This is custom Text
Icon="path to Icon.ico"
Sound=Default

```

### Parameters:

- **Title**: The title of the notification.
- **Text**: The text content of the notification.
- **Icon**: The path to a custom icon `.ico` file (optional). If not provided, the default system icon will be used.
- **Sound**: The sound to play when the notification appears. Options:
  - `Default`: Play the default system notification sound.
  - `<PathToSoundFile>`: Specify a custom `.wav` or `.mp3` file.
  - `None`: No sound will play.
- **LeftMouseUpAction**: The action to execute when the notification is clicked. For example, you can trigger a Rainmeter bang or any other action.

## Example Usage

Here is an example of a full Rainmeter skin that shows how to use the plugin to display a notification with a custom title, text, icon, and sound:

```ini
[Rainmeter]
Update=1000
BackgroundMode=2
SolidColor=255,255,255,150

[mNotification]
Measure=Plugin
Plugin=Notification
Title=This is Title
Text=This is custom Text
Icon="#ROOTCONFIGPATH#Icon.ico"
Sound=Default

[Text]
Meter=STRING
MeasureName=mNotification
X=120
Y=20
W=220
FontColor=000000
Antialias=1
stringAlign = CenterCenter
clipString = 2
Text="🔔 Click Me To Generate a Custom Notification 🎉"
LeftMouseUpAction=[!RainmeterPluginBang "mNotification ExecuteBatch 1"]
```

When the skin is loaded, it will display the notification with the title **Reminder** and text **Don't forget to check the dashboard!**. When the user clicks the notification, it will trigger the defined `LeftMouseUpAction`.

## Building the Plugin

### Prerequisites

- Visual Studio or any C# development environment
- Rainmeter installed
- .NET Framework 4.5 or higher

### Steps to Build

1. Clone or download the repository.
2. Open the project in Visual Studio.
3. Build the project to compile the `Notification.dll`.
4. Copy the resulting `Notification.dll` to the Rainmeter `Plugins` directory.

## License

This project is licensed under the Apache License - see the [LICENSE](LICENSE) file for details.

## Troubleshooting

- **Notification not showing**: Ensure that the plugin is placed in the correct directory: `<Rainmeter Directory>\Plugins`.
- **Icon not showing**: Make sure the icon file is correctly referenced in the `Icon` parameter and that it is an `.ico` file.
- **No sound**: Ensure the sound file path is correct, or use `"Default"` for system sounds.

For any issues or feature requests, feel free to open an issue on this repository!

## Acknowledgements

- The Rainmeter development community for their API and plugin framework.
- .NET and C# for providing the foundation for this plugin.

