using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeviceDetector : MonoBehaviour
{
    [SerializeField] private TMP_Text deviceInfoText;

    [System.Serializable]
    public struct UsbDeviceInfo
    {
        public string deviceName;
        public int vendorId;
        public int productId;
        public string deviceClass;
        public int subclass;
        public int protocol;
        public bool hasPermission;
    }

    public List<UsbDeviceInfo> detectedDevices = new List<UsbDeviceInfo>();

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            ScanUsbDevices();
#endif
    }

    void ScanUsbDevices()
    {
        try
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var context = activity.Call<AndroidJavaObject>("getApplicationContext"))
            using (var usbManagerClass = new AndroidJavaClass("android.hardware.usb.UsbManager"))
            using (var usbManager = context.Call<AndroidJavaObject>("getSystemService", usbManagerClass.GetStatic<string>("USB_SERVICE")))
            {
                if (usbManager == null)
                {
                    Debug.LogError("[USB] No se pudo acceder a UsbManager");
                    return;
                }

                // Obtener lista de dispositivos conectados
                var deviceList = usbManager.Call<AndroidJavaObject>("getDeviceList");
                var values = deviceList.Call<AndroidJavaObject>("values");
                var iterator = values.Call<AndroidJavaObject>("iterator");

                detectedDevices.Clear();

                while (iterator.Call<bool>("hasNext"))
                {
                    var device = iterator.Call<AndroidJavaObject>("next");

                    UsbDeviceInfo info = new UsbDeviceInfo
                    {
                        deviceName = device.Call<string>("getDeviceName"),
                        vendorId = device.Call<int>("getVendorId"),
                        productId = device.Call<int>("getProductId"),
                        deviceClass = device.Call<string>("getClassName"),
                        subclass = device.Call<int>("getDeviceSubclass"),
                        protocol = device.Call<int>("getDeviceProtocol"),
                        hasPermission = usbManager.Call<bool>("hasPermission", device)
                    };

                    detectedDevices.Add(info);
                    deviceInfoText.text += $"[USB] Detectado: {info.deviceName} | VID:{info.vendorId} PID:{info.productId} | Clase:{info.deviceClass} | Permiso:{info.hasPermission}\n";
                    Debug.Log($"[USB] Detectado: {info.deviceName} | VID:{info.vendorId} PID:{info.productId} | Clase:{info.deviceClass} | Permiso:{info.hasPermission}");
                }

                if (detectedDevices.Count == 0)
                { 
                    deviceInfoText.text = "[USB] No hay dispositivos USB conectados o el TV no expone la API Host.";
                    Debug.Log("[USB] No hay dispositivos USB conectados o el TV no expone la API Host.");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[USB] Error al escanear: {e.Message}\n{e.StackTrace}");
            deviceInfoText.text = $"[USB] Error al escanear: {e.Message}";
        }
    }
}
