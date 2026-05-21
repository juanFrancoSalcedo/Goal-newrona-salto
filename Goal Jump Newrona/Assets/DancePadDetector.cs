using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DancePadDetector : MonoBehaviour
{
    [SerializeField] TMP_Text keyText;
    [SerializeField] TMP_Text textDevice;


    [SerializeField] private string[] dancePadKeywords = new string[] 
    { 
        "dance", "pad", "mat", "ddr", "pump", "step", "baile", "tapete" 
    };
    
    [SerializeField] private bool showToastOnConnect = true;
    [SerializeField] private bool showToastOnDisconnect = false;

    private void OnEnable()
    {
        // Suscribirse al evento de cambios de dispositivos [[1]][[34]]
        InputSystem.onDeviceChange += OnDeviceChanged;
        
        // Verificar dispositivos ya conectados al iniciar
        CheckExistingDevices();
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChanged;
    }

    private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
    {
        // Solo procesar en Android y cuando se agrega/remueve un dispositivo
        #if UNITY_ANDROID
        if (change == InputDeviceChange.Added)
        {
            if (IsDancePad(device))
            {
                keyText.text = $"✅ Tapete de baile detectado: {device.name}";
                Debug.Log($"✅ Tapete de baile detectado: {device.name}");
                if (showToastOnConnect)
                    AndroidToast.Show($"🎮 Tapete conectado: {device.name}");
            }
        }
        else if (change == InputDeviceChange.Removed)
        {
            if (IsDancePad(device))
            {
                Debug.Log($"❌ Tapete de baile desconectado: {device.name}");
                keyText.text = $"❌ Tapete de baile desconectado: { device.name}";
                if (showToastOnDisconnect)
                    AndroidToast.Show($"🔌 Tapete desconectado");
            }
        }
        #endif
    }

    private bool IsDancePad(InputDevice device)
    {
        // Verificar por nombre del dispositivo
        string lowerName = device.name.ToLowerInvariant();
        string lowerProduct = device.description.product?.ToLowerInvariant() ?? "";
        string lowerManufacturer = device.description.manufacturer?.ToLowerInvariant() ?? "";
        
        // Buscar palabras clave en nombre, producto o fabricante
        foreach (var keyword in dancePadKeywords)
        {
            if (lowerName.Contains(keyword) || 
                lowerProduct.Contains(keyword) || 
                lowerManufacturer.Contains(keyword))
            {
                return true;
            }
        }
        
        // Verificar si tiene controles típicos de tapete (4 direcciones + select)
        if (device is Gamepad gamepad)
        {
            // Los tapetes suelen mapearse como gamepads con dpad
            // Verificar si solo tiene dpad y botones básicos (sin sticks analógicos activos)
            var buttonControls = device.allControls
                .OfType<UnityEngine.InputSystem.Controls.ButtonControl>()
                .Count();
            
            // Un tapete típico tiene: 4 direcciones + select + start + posiblemente 4 botones extra
            if (buttonControls >= 4 && buttonControls <= 12)
            {
                // Verificar que el dpad esté presente
                if (gamepad.dpad != null && gamepad.dpad.up != null)
                {
                    return true;
                }
            }
        }
        
        // Verificar dispositivo genérico con muchos botones (posible tapete USB)
        if (device.description.interfaceName == "HID" || device.description.interfaceName == "USB")
        {
            var buttonCount = device.allControls
                .OfType<UnityEngine.InputSystem.Controls.ButtonControl>()
                .Count();
            
            // Tapetes suelen tener entre 6-16 botones
            if (buttonCount >= 6 && buttonCount <= 16)
            {
                Debug.Log($"🔍 Posible tapete por conteo de botones: {buttonCount} en {device.name}");
                return true;
            }
        }
        
        return false;
    }

    private void CheckExistingDevices()
    {
        // Verificar dispositivos ya conectados al iniciar la app
        foreach (var device in InputSystem.devices)
        {
            if (IsDancePad(device))
            {
                Debug.Log($"✅ Tapete ya conectado al iniciar: {device.name}");
                if (showToastOnConnect)
                    AndroidToast.Show($"🎮 Tapete detectado: {device.name}");
            }
        }
    }
    
    // Método para debug: listar todos los dispositivos conectados
    public void PrintConnectedDevices()
    {
        textDevice.text = "=== DISPOSITIVOS CONECTADOS ===";
        foreach (var device in InputSystem.devices)
        {
            textDevice.text = $"• {device.name} | Tipo: {device.GetType().Name} | " +
                     $"Producto: '{device.description.product}' | " +
                     $"Fabricante: '{device.description.manufacturer}' | " +
                     $"Interfaz: '{device.description.interfaceName}'";
        }
        Debug.Log("================================");
    }
}