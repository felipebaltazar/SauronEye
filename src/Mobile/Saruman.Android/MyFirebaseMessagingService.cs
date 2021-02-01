using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using AndroidX.Core.App;
using Firebase.Messaging;

namespace Saruman.Droid
{

    [Service(Name = "com.sauroneye.saruman.MyFirebaseMessagingService", Enabled = true)]
    [IntentFilter(new[] {"com.google.firebase.MESSAGING_EVENT"})]
    public class MyFirebaseMessagingService : FirebaseMessagingService {

        public const string ActionNotificationIdKey = "action_notification_id";

        public const string ActionNotificationTagKey = "action_notification_tag";
        private static Context _context;

        public static void Initialize(Context context) {
            _context = context;
        }

        public override void OnNewToken(string token) {
            System.Diagnostics.Debug.WriteLine($"FIREBASE_TOKEN {token}");
            SaveToken(token);
            base.OnNewToken(token);
        }

        private void SaveToken(string token) {
            var prefs = GetSharedPreferences("FCM_TOKEN", FileCreationMode.Private);
            if (!string.IsNullOrWhiteSpace(token)) {
                //CrossPushNotification.Current.SetToken(token);
                var editor = prefs.Edit();
                editor.PutString("FCM_TOKEN", token);
                editor.Apply();
            }
        }

        // private string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message) {
            
            try {
                InitializePush();
                var data = message.Data.Keys.ToDictionary(key => key, key => (object)message.Data[key]);
                data.Add("gcm.message_id", message.MessageId);
                //var tmp = $"####### Push Data Payload received: {JsonConvert.SerializeObject(data)}";
                //Console.WriteLine(tmp);
                //CrossPushNotification.Current.NotificationReceived(data);
                base.OnMessageReceived(message);
                SendNotification(message.Data);
                
            }
            catch (Exception e) {
                Console.WriteLine("\n\n\n###-PUSH ERROR APP CLOSED AND CAN'T START SERVICES-###\n\n\n");
                Console.WriteLine(e);
            }
        }
        
        private void SendNotification(IDictionary<string, string> data) {

            var intent = new Intent(_context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            foreach (var key in data.Keys) {
                intent.PutExtra(key, data[key]);
            }
            intent.PutExtra("open_notification", true);

            var title = data.ContainsKey("title") ? data["title"] : "Saruman";
            var body = data.ContainsKey("body") ? data["body"] : "Pode ter um novo incidente.";

            Random random = new Random();
            int pushCount = random.Next(9999 - 1000) + 1000; //for multiplepushnotifications
            var pendingIntent =
                PendingIntent.GetActivity(_context, pushCount, intent, PendingIntentFlags.Immutable);


            var notificationBuilder = new NotificationCompat.Builder(this, MainApplication.ChannelID)
                .SetSmallIcon(Resource.Drawable.icon_push).SetContentTitle(title)
                .SetContentText(body).SetAutoCancel(true).SetContentIntent(pendingIntent);
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
        }

        private void InitializePush() {
            try {
                //if (!IoC.IsContainerInitialized) {
                //    IoC.BuildContainer(true);
                //}

                //var pushService = IoC.Resolve<IPushNotifService>();
                //if (!pushService.IsInitialized) {
                //    pushService.Initialize();
                //}                               
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void ProcessIntent(Activity activity, Intent intent, bool enableDelayedResponse = true) {
            //var DefaultNotificationActivityType = activity.GetType();

            var extras = intent?.Extras;
            if (extras != null && !extras.IsEmpty) {
                var parameters = new Dictionary<string, object>();
                foreach (var key in extras.KeySet()) {
                    if (!parameters.ContainsKey(key) && extras.Get(key) != null)
                        parameters.Add(key, $"{extras.Get(key)}");
                }

                NotificationManager manager = _context.GetSystemService(NotificationService) as NotificationManager;
                var notificationId = extras.GetInt(ActionNotificationIdKey, -1);
                if (notificationId != -1) {
                    var notificationTag = extras.GetString(ActionNotificationTagKey, string.Empty);
                    if (notificationTag == null)
                        manager.Cancel(notificationId);
                    else
                        manager.Cancel(notificationTag, notificationId);
                }

                //CrossPushNotification.Current.NotificationOpened(parameters);
            }
        }


    }
}