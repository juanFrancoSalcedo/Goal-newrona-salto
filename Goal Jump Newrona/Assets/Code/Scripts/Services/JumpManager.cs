using System;
using B_Extensions;
using Features;
using System.Collections;
using UnityEngine;

namespace Services
{
    public class JumpManager : Singleton<JumpManager>
    {
        [SerializeField] private PlayerJump _playerJump;

        public event Action<float> OnTimeJumpEnd;
        public event Action<float> OnTimeJumpUpdate;

        private InputService _inputService;
        private Coroutine _jumpCoroutine;
        private float _jumpTime;
        bool allowed = false;

        void Start()
        {
            _inputService = InputService.Instance;
        }

        private void OnEnable()
        {
            GameStateContext.GameStateMediator.Subscribe(GameEventType.GameStarted, () => SetAllowed(true));
            GameStateContext.GameStateMediator.Subscribe(GameEventType.GameFinished, () => SetAllowed(false));
        }

        private void OnDisable()
        {
            GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameStarted, () => SetAllowed(true));
            GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameFinished, () => SetAllowed(false));
        }

        public void SetAllowed(bool _allowed) => this.allowed = _allowed;

        void Update()
        {
            if (!allowed)
                return;
            
            if (_inputService.IsAnyKeyReleased() && !_playerJump.IsJumping)
            {
                _jumpCoroutine = StartCoroutine(Jump());
                _jumpTime = 0f;
                _playerJump.StartJump();
            }

            if (_inputService.IsAnyKeyPressed() && _playerJump.IsJumping)
            {
                if (_jumpCoroutine != null)
                {
                    StopCoroutine(_jumpCoroutine);
                    _jumpCoroutine = null;
                    OnTimeJumpEnd?.Invoke(_jumpTime);
                    _playerJump.StopJump();
                }
            }


            if(_playerJump.IsJumping)
                OnTimeJumpUpdate?.Invoke(_jumpTime);
        }

        private IEnumerator Jump()
        {
            while (true)
            {
                _jumpTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
