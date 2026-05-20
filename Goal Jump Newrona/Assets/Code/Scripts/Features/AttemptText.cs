using Features;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class AttemptText : MonoBehaviour
{
    [SerializeField] private DataCountdown[] countdown;
    [SerializeField] private TMP_Text titleText;

    void Update()
    {
        if (EndGameManager.attempts >= countdown.Length)
            return;
        titleText.text = countdown[EndGameManager.attempts].Title;
    }
}

[System.Serializable]
public struct DataCountdown 
{
    public string Title;
}
