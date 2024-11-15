using System;
using System.Runtime.InteropServices;
using Rainmeter;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace PluginNotification
{
    internal class Measure
    {
        private API _api;
        public string Title { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public string Sound { get; set; }
        public string LeftMouseUpAction { get; set; }

        public Measure()
        {
            Title = string.Empty;
            Text = string.Empty;
            Icon = string.Empty;
            Sound = string.Empty;
            LeftMouseUpAction = string.Empty;
        }

        public void Reload(API api, ref double maxValue)
        {
            _api = api;
            Title = _api.ReadString("Title", "Default Title");
            Text = _api.ReadString("Text", "Default text");
            Icon = _api.ReadString("Icon", "");
            Sound = _api.ReadString("Sound", "Default");
            LeftMouseUpAction = _api.ReadString("LeftMouseUpAction", "");
        }

        public void ShowNotification()
        {
            try
            {
                // Create a new notification with the parameters provided.
                var notification = new NotifyIcon
                {
                    Visible = true,
                    Icon = string.IsNullOrEmpty(Icon) ? SystemIcons.Information : new System.Drawing.Icon(Icon),
                    BalloonTipTitle = Title,
                    BalloonTipText = Text,
                    BalloonTipIcon = ToolTipIcon.Info
                };

                // Show the notification
                notification.ShowBalloonTip(3000); // Show the notification for 3 seconds

                // Play the sound if specified
                if (Sound.ToLower() != "none")
                {
                    PlayNotificationSound();
                }

                // Add click event handler
                notification.MouseClick += (sender, args) =>
                {
                    if (args.Button == MouseButtons.Left)
                    {
                        // Execute the action on left mouse click
                        if (!string.IsNullOrEmpty(LeftMouseUpAction))
                        {
                            _api.Execute(LeftMouseUpAction);
                        }
                    }
                };

                // Keep the notification visible until it's clicked
                Thread.Sleep(10000); // Keep it visible for 10 seconds or until clicked
                notification.Dispose();
            }
            catch (Exception ex)
            {
                _api.Log(API.LogType.Error, $"Error showing notification: {ex.Message}");
            }
        }

        private void PlayNotificationSound()
        {
            try
            {
                if (Sound.ToLower() == "default")
                {
                    SystemSounds.Exclamation.Play(); // Play default notification sound
                }
                else
                {
                    // Custom sound file (if available)
                    if (System.IO.File.Exists(Sound))
                    {
                        using (var player = new SoundPlayer(Sound))
                        {
                            player.Play();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _api.Log(API.LogType.Error, $"Error playing sound: {ex.Message}");
            }
        }
    }

    public static class Plugin
    {
        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            GCHandle.FromIntPtr(data).Free();
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.Reload(new API(rm), ref maxValue);
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            return 0.0; // No continuous update needed for this plugin
        }

        [DllExport]
        public static void ExecuteBang(IntPtr data, [MarshalAs(UnmanagedType.LPWStr)] string args)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.ShowNotification(); // Display the notification
        }
    }
}
