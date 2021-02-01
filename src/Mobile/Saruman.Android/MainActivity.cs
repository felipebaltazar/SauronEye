using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.Legacy.Content;
using AndroidX.LocalBroadcastManager.Content;
using DryIoc;
using FFImageLoading.Forms.Platform;
using Saruman.Helpers.Enums;
using Saruman.Interfaces.Services;
using Saruman.Models;

using FormsApp = Xamarin.Forms.Application;

namespace Saruman.Droid
{
    [Activity(
        Label = "Saruman",
        Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_launcher_round",
        Theme = "@style/MainTheme",
        LaunchMode = LaunchMode.SingleTask,
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const string ChannelID = "1001";
        public const int NOTIFICATION_ID = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(enableFastRenderer: true);

            CreateNotificationChannel();

            IContainer container = new InitializerAndroid().GetContainer();

            LoadApplication(container.Resolve<App>());
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                return;

            var channel = new NotificationChannel(ChannelID, "sauron", NotificationImportance.Default);
            channel.Description = "Messages from the eye.";

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



        protected override void OnPostResume()
        {
            base.OnPostResume();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            if (intent.Extras is null || !intent.Extras.ContainsKey("open_notification") || !intent.Extras.GetBoolean("open_notification"))
                return;

            Incident incident = GetIncident(intent);

            ((App)FormsApp.Current).Resolve<IDeeplinkService>().HandleIncidentAsync(incident);
        }

        private Incident GetIncident(Intent intent)
        {
            var incident = new Incident();
            foreach (var key in intent.Extras.KeySet())
            {
                switch (key.ToUpperInvariant())
                {
                    case "PRIORIDADE":
                        incident.Priority = GetPriority(intent.Extras.GetString(key));
                        break;
                    case "TITLE":
                        incident.Title = intent.Extras.GetString(key);
                        break;
                    case "BODY":
                        incident.Description = intent.Extras.GetString(key);
                        break;
                    case "SQUAD":
                        incident.Squad = intent.Extras.GetString(key);
                        break;
                    case "CHAMADO_SN":
                        incident.IncidentId = intent.Extras.GetString(key);
                        break;
                    case "REGISTERED_AT":
                        incident.RegisteredAt = DateTime.FromFileTime(long.Parse(intent.Extras.GetString(key)));
                        break;
                }
            }
            return incident;
        }

        private Priority GetPriority(string value)
            => (value.ToUpperInvariant()) switch
            {
                "P1" => Priority.P1,
                "P2" => Priority.P2,
                "P3" => Priority.P3,
                "P4" => Priority.P4,
                "INFO" => Priority.Info,
                _ => Priority.Unknown
            };
    }
}