using B_Extensions;
using Features.Score;
using Services;
using UnityEngine;

namespace Features
{
    public class EndGameManager:Singleton<EndGameManager>
    {
        [SerializeField] private int maxAttempts =3;
        [SerializeField] private JumpManager jumpManager;
        [SerializeField] private GameObject canvasEnd;
        public static int attempts = 0;
        public static event System.Action OnIncreaseAttempt;
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
                RankingManager.Instance.UpdateRanking();
            }
        }

        public void IncreaseAttempts()
        {
            attempts++;
            ManagerAudio.Instance.PlayWhistelRandom();
            OnIncreaseAttempt?.Invoke();
        }
    }
}
