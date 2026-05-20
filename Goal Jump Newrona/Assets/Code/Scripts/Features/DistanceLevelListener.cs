using Services;
using UnityEngine;

namespace Features
{
    public class DistanceLevelListener : MonoBehaviour
    {
        [SerializeField] private JumpManager _jumpManager;
        [SerializeField] private PlayerJump _playerJump;
        [SerializeField] private GameObject _cardInfoPrefab;

        private void OnEnable()
        {
            if (_jumpManager != null)
                _jumpManager.OnTimeJumpEnd += HandleJumpEnded;
        }

        private void OnDisable()
        {
            if (_jumpManager != null)
                _jumpManager.OnTimeJumpEnd -= HandleJumpEnded;
        }

        private void HandleJumpEnded(float timeJump)
        {
            if (_cardInfoPrefab == null || _playerJump == null)
                return;

            var card = Instantiate(_cardInfoPrefab, _playerJump.transform.position, Quaternion.identity);
            var cardInfo = card.GetComponent<CardInfo>();
            card.transform.position = _playerJump.transform.position;
            if (cardInfo != null)
                cardInfo.Initialize(timeJump);
        }
    }
}
