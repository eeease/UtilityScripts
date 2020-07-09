using UnityEngine;
using System;

public static class NotificationManager
{
	private static DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	private static AndroidJavaClass _javaNotificationManager;
		
	static NotificationManager ()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaNotificationManager = new AndroidJavaClass ("com.takohi.unity.plugins.UnityNotificationManager");
	}
		
	/// <summary>
	/// Shows immediately the notification. If a notification with the same ID is already displayed, it will be overwritten.
	/// </summary>
	/// <param name="id">Notification's identifier.</param>
	/// <param name="notification">Notification.</param>
	public static void ShowNotification (int id, Notification notification)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaNotificationManager.CallStatic ("showNotification", id, notification._javaUnityNotification);
	}

	/// <summary>
	/// Shows the notification at the specified time. If a notification with the same ID is already scheduled or displayed, it will be overwritten.
	/// Optionally, an interval time in milliseconds can be specified in order to repeat the notification.
	/// </summary>
	/// <param name="id">Notification's identifier.</param>
	/// <param name="notification">Notification.</param>
	/// <param name="triggerAt">The date and time when to trigger the notification.</param>
	/// <param name="interval">Interval in milliseconds between subsequent repeats of the notification. If null or negative, the notification will be triggered once.</param>
	public static void ShowNotification (int id, Notification notification, DateTime triggerAt, long interval=-1)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaNotificationManager.CallStatic ("showNotification", id, notification._javaUnityNotification, (long) (triggerAt.ToUniversalTime() - EPOCH).TotalMilliseconds, interval);
	}

	/// <summary>
	/// Shows the notification after a specified delay. If a notification with the same ID is already scheduled or displayed, it will be overwritten.
	/// Optionally, an interval time in milliseconds can be specified in order to repeat the notification.
	/// </summary>
	/// <param name="id">Notification's identifier.</param>
	/// <param name="notification">Notification.</param>
	/// <param name="triggerIn">The delay in milliseconds when to trigger the notification.</param>
	/// <param name="interval">Interval in milliseconds between subsequent repeats of the notification. If null or negative, the notification will be triggered once.</param>
	public static void ShowNotification (int id, Notification notification, long triggerIn, long interval=-1)
	{
		DateTime triggerAt = DateTime.UtcNow;
		triggerAt = triggerAt.AddMilliseconds (triggerIn);

		ShowNotification (id, notification, triggerAt, interval);
	}

	/// <summary>
	/// Cancels a notification.
	/// </summary>
	/// <param name="id">Notification's identifier to cancel.</param>
	public static void Cancel (int id)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaNotificationManager.CallStatic ("cancel", id);	
	}
		
	/// <summary>
	/// Cancels all notifications.
	/// </summary>
	public static void CancelAll ()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaNotificationManager.CallStatic ("cancelAll");	
	}
}

