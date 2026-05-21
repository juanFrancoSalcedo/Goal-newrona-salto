// AndroidToast.cs
using UnityEngine;

public static class AndroidToast
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaClass _unityPlayer;
    private static AndroidJavaObject _currentActivity;
    private static AndroidJavaClass _toastClass;
    
    private static void Initialize()
    {
        if (_unityPlayer == null)
        {
            _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            _toastClass = new AndroidJavaClass("android.widget.Toast");
        }
    }
    
    public static void Show(string message, int duration = 0)
    {
        Initialize();
        
        // duration: 0 = ToastLength.Short (2s), 1 = ToastLength.Long (3.5s)
        int toastDuration = (duration == 1) ? 1 : 0;
        
        _currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            using (var toast = _toastClass.CallStatic<AndroidJavaObject>(
                "makeText", _currentActivity, message, toastDuration))
            {
                toast?.Call("show");
            }
        }));
    }
    
    public static void ShowLong(string message) => Show(message, 1);
    
#else
    // Fallback para editor y otras plataformas
    public static void Show(string message, int duration = 0)
    {
        Debug.Log($"[TOAST] {message}");
    }
    
    public static void ShowLong(string message)
    {
        Debug.Log($"[TOAST-LONG] {message}");
    }
#endif
}