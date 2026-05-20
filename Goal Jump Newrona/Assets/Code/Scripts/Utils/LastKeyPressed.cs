using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LastKeyTracker : MonoBehaviour
{
    public List<string> keyPressHistory = new List<string>();
    [SerializeField] TMP_Text lastKeyText;

    void Update()
    {
        // Input.inputString captura SOLO teclas que producen texto
        // Ignora teclas como Shift, Ctrl, Alt, flechas, etc.
        foreach (char c in Input.inputString)
        {
            string keyName = c.ToString();
            keyPressHistory.Add(keyName);
            Debug.Log($"Tecla de texto presionada: {keyName}");
            lastKeyText.text = $"Tecla de texto presionada: {keyName}";

            if (keyPressHistory.Count > 100) keyPressHistory.RemoveAt(0);
        }
    }

    public string LastKeyPressed
    {
        get
        {
            if (keyPressHistory.Count == 0) return "None";
            return keyPressHistory[keyPressHistory.Count - 1];
        }
    }
}
