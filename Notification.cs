using UnityEngine;
using System;

public class Notification
{
	internal AndroidJavaObject _javaUnityNotification;

	/// <summary>
	/// Initializes a new instance of the <see cref="Notification"/> class.
	/// </summary>
	/// <param name="contentTitle">First line of text in the platform notification.</param>
	/// <param name="contentText">Second line of text in the platform notification.</param>
	public Notification (string contentTitle,
	                     string contentText)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification = new AndroidJavaObject ("com.takohi.unity.plugins.UnityNotification",
		                                                contentTitle, contentText);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Notification"/> class.
	/// </summary>
	/// <param name="smallIconResourceName">Small icon resource, which will be used to represent the notification in the status bar. If not found, the application's icon will be used.</param>
	/// <param name="contentTitle">First line of text in the platform notification.</param>
	/// <param name="contentText">Second line of text in the platform notification.</param>
	public Notification (string smallIconResourceName, string contentTitle,
			string contentText)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification = new AndroidJavaObject ("com.takohi.unity.plugins.UnityNotification",
				smallIconResourceName, contentTitle, contentText);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Notification"/> class assigned to a channel (Android 8+ only).
	/// </summary>
	/// <param name="smallIconResourceName">Small icon resource, which will be used to represent the notification in the status bar. If not found, the application's icon will be used.</param>
	/// <param name="contentTitle">First line of text in the platform notification.</param>
	/// <param name="contentText">Second line of text in the platform notification.</param>
	/// <param name="channelId">Channel ID assigned to the notification (Android 8+ only).</param>
	/// <param name="channelName">Second line of text in the platform notification (Android 8+ only).</param>
	public Notification (string smallIconResourceName, string contentTitle,
			string contentText, string channelId, string channelName)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification = new AndroidJavaObject ("com.takohi.unity.plugins.UnityNotification",
				smallIconResourceName, contentTitle, contentText, channelId, channelName);
	}

	/// <summary>
	/// Add a large icon to the notification content.
	/// </summary>
	/// <param name="texture">Readable texture.</param>
	public void SetLargeIcon (Texture2D texture)
	{
		int width = texture.width;
		int height = texture.height;
		Color32[] pixels;
		try {
			pixels = texture.GetPixels32 ();
		}
		catch (UnityException)
		{
			Debug.Log("Notification's large icon was not set because the texture is not readable.");
			return;
		}

		setLargeIcon (pixels, width, height, true);
	}

	/// <summary>
	/// Add a large icon to the notification content.
	/// </summary>
	/// <param name="colors">Array of colors.</param>
	/// <param name="width">Image's width.</param>
	/// <param name="height">Image's height.</param>
	/// <param name="flipVertically">If set to <c>true</c> flip vertically.</param>
	public void setLargeIcon (Color32[] colors, int width, int height, bool flipVertically=false)
	{
		int[] c = new int[colors.Length];
		int indice;
		for (int i = 0; i < colors.Length; ++i) {
			int argb = (((int)colors [i].a) << 24)
				+ (((int)colors [i].r) << 16)
					+ (((int)colors [i].g) << 8)
					+ ((int) colors [i].b);
			// Flip vertically
			indice = (flipVertically)?((height - (i / width) - 1) * width + i % width):i;
			c [indice] = argb;
		}

		setLargeIcon (c, width, height);
	}

	/// <summary>
	/// Add a large icon to the notification content.
	/// </summary>
	/// <param name="colors">Array of ARGB colors (each channel encoded on 8 bits).</param>
	/// <param name="width">Image's width.</param>
	/// <param name="height">Image's height.</param>
	public void setLargeIcon (int[] colors, int width, int height)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification.Call ("setLargeIcon", colors, width, height);
	}

	/// <summary>
	/// A small piece of additional information pertaining to this notification. The platform template will draw this on the last line of the notification, at the far right (to the right of a smallIcon if it has been placed there).
	/// </summary>
	/// <param name="contentInfo">Content info.</param>
	public void SetContentInfo (string contentInfo)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification.Call ("setContentInfo", contentInfo);
	}

	/// <summary>
	/// Set the large number at the right-hand side of the notification. This is equivalent to setContentInfo, although it might show the number in a different font size for readability.
	/// </summary>
	/// <param name="number">Number.</param>
	public void SetNumber (int number)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification.Call ("setNumber", number);
	}

	/// <summary>
	/// Add a timestamp pertaining to the notification (usually the time the event occurred). It will be shown in the notification content view by default.
	/// </summary>
	/// <param name="time">Time.</param>
	public void SetWhen (long time)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification.Call ("setWhen", time);
	}

	/// <summary>
	/// Set the vibration pattern to use. It requires VIBRATE permission. Pass in an array of ints that are the durations for which to turn on or off the vibrator in milliseconds. The first value indicates the number of milliseconds to wait before turning the vibrator on. The next value indicates the number of milliseconds for which to keep the vibrator on before turning it off. Subsequent values alternate between durations in milliseconds to turn the vibrator off or to turn the vibrator on.
	/// </summary>
	/// <param name="pattern">Pattern.</param>
	public void SetVibrate (long[] pattern)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification.Call ("setVibrate", pattern);
	}

	/// <summary>
	/// Set the desired color for the indicator LED on the device, as well as the blink duty cycle (specified in milliseconds). Not all devices will honor all (or even any) of these values.
	/// </summary>
	/// <param name="color">Color.</param>
	/// <param name="onMs">On ms.</param>
	/// <param name="offMs">Off ms.</param>
	public void SetLights (Color32 color, int onMs, int offMs)
	{
		int argb = color.a << 24
			+ color.r << 16
			+ color.g << 8
			+ color.b;

		SetLights (argb, onMs, offMs);
	}

	/// <summary>
	/// Set the desired color for the indicator LED on the device, as well as the blink duty cycle (specified in milliseconds). Not all devices will honor all (or even any) of these values.
	/// </summary>
	/// <param name="argb">ARGB.</param>
	/// <param name="onMs">On ms.</param>
	/// <param name="offMs">Off ms.</param>
	public void SetLights (int argb, int onMs, int offMs)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification.Call ("setLights", argb, onMs, offMs);
	}

	/// <summary>
	/// Set whether this is an "ongoing" notification. Ongoing notifications cannot be dismissed by the user, so your application or game must take care of canceling them.
	/// </summary>
	/// <param name="sticky">If set to <c>true</c> sticky.</param>
	public void SetSticky (bool sticky) {
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification.Call ("setSticky", sticky);
	}

	/// <summary>
	/// Enable playing the default notification sound.
	/// </summary>
	/// <param name="enable">If set to <c>true</c> play notification sound.</param>
	public void EnableSound(bool enable) {
		if (Application.platform != RuntimePlatform.Android)
			return;

		_javaUnityNotification.Call ("enableSound", enable);
	}
}

