using System;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Firebase;
using Firebase.Messaging;
//using Plugin.CurrentActivity;

namespace Saruman.Droid
{
    //=============================================================================================
#if DEBUG
    [Application(Debuggable = true)]
#else
	[Application(Debuggable = false)]
#endif
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks {

        #region Properties
        //-----------------------------------------------------------------------------------------
        public const string ChannelID = "1001";
        //-----------------------------------------------------------------------------------------
        #endregion
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
            : base(handle, transer) {
        }

        public override void OnCreate() {
            base.OnCreate();

            var builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            MyFirebaseMessagingService.Initialize(this);

            if (IsPlayServicesAvailable())
            {
                FirebaseMessaging.Instance.SubscribeToTopic("Default");
            }
            CreateNotificationChannel();
            var prefs = GetSharedPreferences("FCM_TOKEN", FileCreationMode.Private);
            var token = prefs.GetString("FCM_TOKEN", string.Empty);
            if (!string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine($"FCM TOKEN:   {token}");
                //CrossPushNotification.Current.SetToken(token);
            }
            FirebaseApp.InitializeApp(this);
            //CrossCurrentActivity.Current.Init(this);

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

        public bool IsPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Console.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Console.WriteLine("This device is not supported");
                    //Finish();
                }

                return false;
            }

            Console.WriteLine("Google Play Services is available.");
            return true;
        }

        public override void OnTerminate() {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState) {
            //CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity) {
        }

        public void OnActivityPaused(Activity activity) {
        }

        public void OnActivityResumed(Activity activity) {
            //CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) {
        }

        public void OnActivityStarted(Activity activity) {
            //CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity) {
        }

    }
    //=============================================================================================
}