using Services;
using System;
using UnityEngine;

namespace Features
{
    public class EndGameManager:MonoBehaviour
    {
        [SerializeField] private int maxAttempts =3;
        [SerializeField] private JumpManager jumpManager;
        [SerializeField] private GameObject canvasEnd;
        public static int attempts = 0;
        bool ended = false;
        private bool allowed = false;

        private void Update()
        {
            if (attempts >= maxAttempts && !ended)
            { 
                ended = true;
                GameStateContext.ChangeState(GameEventType.GameFinished);
                attempts = 0;
                canvasEnd.SetActive(true);
            }
        }
    }
}
