using Foundation;
using Microsoft.Azure.NotificationHubs;
using UIKit;
using UserNotifications;

namespace TestAppRazorMAUI;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
	{
		application.RegisterForRemoteNotifications();
		UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err)=>
		{

		});
		return base.FinishedLaunching(application, launchOptions);

	}

    [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
    public void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
    {
        Task.Run(async () =>
        {
            try
            {
                var deviceTokenString = BitConverter.ToString(deviceToken.ToArray()).Replace("-", "");



                var install = new Installation
                {
                    Platform = NotificationPlatform.Apns,
                    PushChannel = deviceTokenString,
                    ExpirationTime = DateTime.UtcNow.AddYears(1),
                    InstallationId = deviceTokenString,
                };



                var connectionString = "Endpoint=sb://Trafikverket.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=rAHMLL2O9YFo1WMbmT5TRZJRNY5TF61vqNdoCf2MDZo=";
                var hubClient = NotificationHubClient.CreateClientFromConnectionString(connectionString, "Trafikverket");
                await hubClient.CreateOrUpdateInstallationAsync(install);
            }
            catch (Exception ex)
            {
            }
        });
    }



    [Export("application:didFailToRegisterForRemoteNotificationsWithError:")]
    public void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
    {
        Console.WriteLine($"EVENT!!! FailedToRegisterForRemoteNotifications {error.Code} {error.Description}");
    }



    [Export("application:didReceiveRemoteNotification:fetchCompletionHandler:")]
    public void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
    {
        Console.WriteLine("EVENT!!! DidReceiveRemoteNotification");
    }
}
