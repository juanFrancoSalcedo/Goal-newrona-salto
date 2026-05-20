using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Features
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float _duration = 3f;
        [SerializeField] private float _startDelay = 0f;
        [SerializeField] private TMP_Text _displayText;
        [SerializeField] private float _punchScale = 0.3f;
        [SerializeField] private float _punchDuration = 0.2f;

        public event Action OnCountdownBegin;
        public event Action OnCountdownFinished;
        public UnityEvent onCountdownFinished;
        public UnityEvent OnCountdownStarted;
        public UnityEvent OnCountdownPreWarm;

        private float _remaining;
        private bool _isRunning;
        private bool _isDelaying;
        private int _previousValue;

        public void Begin()
        {
            OnCountdownBegin?.Invoke();
            OnCountdownPreWarm?.Invoke();
            if (_startDelay > 0f)
            {
                _remaining = _startDelay;
                _isDelaying = true;
                _isRunning = true;
                _previousValue = int.MaxValue;
            }
            else
            {
                _remaining = _duration;
                _isRunning = true;
                _previousValue = int.MaxValue;
                UpdateDisplay();
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _isDelaying = false;
        }

        private void Update()
        {
            if (!_isRunning)
                return;

            if (_isDelaying)
            {
                _remaining -= Time.deltaTime;
                if (_remaining <= 0f)
                {
                    _remaining = _duration;
                    _isDelaying = false;
                    _previousValue = int.MaxValue;
                    OnCountdownStarted?.Invoke();
                    UpdateDisplay();
                }
                return;
            }

            _remaining -= Time.deltaTime;
            UpdateDisplay();

            if (_remaining <= 0f)
            {
                _remaining = 0f;
                _isRunning = false;
                UpdateDisplay();
                OnCountdownFinished?.Invoke();
                onCountdownFinished?.Invoke();
            }
        }

        private void UpdateDisplay()
        {
            if (_displayText == null)
                return;

            var currentValue = Mathf.CeilToInt(_remaining);
            _displayText.text = currentValue.ToString();// > 0 ? currentValue.ToString() : _zeroPlaceholder;

            if (currentValue != _previousValue)
            {
                _displayText.transform.DOPunchScale(Vector3.one * _punchScale, _punchDuration);
                _previousValue = currentValue;
            }
        }
    }
}
