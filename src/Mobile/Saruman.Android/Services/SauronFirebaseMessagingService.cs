using Android.App;
using Android.Content;
using Android.Graphics;
using AndroidX.Core.App;
using Firebase.Messaging;

namespace Saruman.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class SauronFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            ShowNotification(message);
        }

        public override void OnNewToken(string token)
        {
            System.Diagnostics.Debug.WriteLine($"FIREBASE_TOKEN {token}");
            base.OnNewToken(token);
        }

        private void ShowNotification(RemoteMessage message)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this, MainApplication.ChannelID)
                                                            .SetSmallIcon(Resource.Drawable.icon_push)
                                                            .SetContentTitle(message?.GetNotification()?.Title ?? "Sauron")
                                                            .SetContentText(message?.GetNotification()?.Body ?? "New message")
                                                            .SetAutoCancel(true)
                                                            .SetVisibility(NotificationVisibility.Public)
                                                            .SetColor(Color.Blue)
                                                            .SetContentIntent(pendingIntent);

            //var notificationManager = NotificationManager.FromContext(this);
            var notificationService = GetSystemService(NotificationService) as NotificationManager;
            notificationService.Notify(0, notificationBuilder.Build());
        }
    }
}
