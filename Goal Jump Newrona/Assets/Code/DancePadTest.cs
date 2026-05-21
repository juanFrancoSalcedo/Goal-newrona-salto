using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DancePadTest : MonoBehaviour
{
    [SerializeField] private DancePadDetector detector;
    [SerializeField] private TMP_Text textl;
    
    void Start()
    {
        // Mostrar dispositivos al iniciar para debug
        detector?.PrintConnectedDevices();
        
        // Toast de bienvenida
        AndroidToast.Show("🎮 App iniciada - Conecta tu tapete");
    }
    
    void Update()
    {
        // Detectar input del tapete para probar
        if ((Gamepad.current?.dpad.up.isPressed ?? false))
        {
            Debug.Log("⬆️ Arriba presionado");
            textl.text = "⬆️ Arriba presionado";
        }
        
        if ((Gamepad.current?.dpad.down.isPressed ?? false))
        {
            textl.text = "⬇️ Abajo presionado";
        }
        
        if ((Gamepad.current?.dpad.left.isPressed ?? false))
        {
            textl.text = "⬅️ Izquierda presionado";
        }
        
        if ((Gamepad.current?.dpad.right.isPressed ?? false))
        {
            textl.text = "➡️ Derecha presionado";
        }
        
        // Botón SELECT/OK del tapete
        if ((Gamepad.current?.buttonSouth.wasPressedThisFrame ?? false))
        {   
            textl.text = "✅ SELECT presionado";
            AndroidToast.Show("¡Botón OK detectado!");
        }
    }
}