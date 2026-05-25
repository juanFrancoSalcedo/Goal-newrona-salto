using Services;
using UnityEngine;
using UnityEngine.Events;

namespace Features
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float _jumpAmount = 1f;
        [SerializeField] private CountdownTimer _countdownTimer;

        private Vector3 _initialPosition;
        private bool _isJumping = false;

        public bool IsJumping => _isJumping;

        private void Awake() => _initialPosition = transform.position;

        public void StartJump()
        {
            _isJumping = true;
            print("empieza");
        }

        public void StopJump()
        {
            _isJumping = false;
            print("Stop");
            transform.position = _initialPosition;
            JumpManager.Instance.SetAllowed(false);
            Invoke(nameof(TryStartCount),3f);
        }

        private void TryStartCount() 
        {
            _countdownTimer.Begin();
            EndGameManager.Instance.IncreaseAttempts();
        }

        private void Update()
        {
            if (!_isJumping)
                return;

            var position = transform.position;
            position.y += _jumpAmount * Time.deltaTime;
            transform.position = position;
        }
    }
}
