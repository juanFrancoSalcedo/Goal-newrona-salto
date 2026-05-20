using System.Collections.Generic;
using Services;
using UnityEngine;

public class ScreenResult : MonoBehaviour
{
    [SerializeField] private CardResult[] _cards;

    private List<float> _jumpResults = new();
    private int _currentIndex;

    void OnEnable()
    {
        JumpManager.Instance.OnTimeJumpEnd += OnJumpEnd;
    }

    void OnDisable()
    {
        JumpManager.Instance.OnTimeJumpEnd -= OnJumpEnd;
    }

    private void OnJumpEnd(float time)
    {
        _jumpResults.Add(time);

        if (_currentIndex < _jumpResults.Count && _currentIndex < _cards.Length)
        {
            ShowNextResult();
        }
    }

    private void ShowNextResult()
    {
        if (_currentIndex >= _jumpResults.Count || _currentIndex >= _cards.Length)
            return;

        int attempt = _currentIndex + 1;
        float result = _jumpResults[_currentIndex];

        _cards[_currentIndex].Configure(attempt, result);
        _cards[_currentIndex].AnimateShow();

        _currentIndex++;
    }

    public void ShowNext()
    {
        if (_currentIndex < _jumpResults.Count && _currentIndex < _cards.Length)
        {
            ShowNextResult();
        }
    }

    public void ShowAll()
    {
        _currentIndex = 0;
        for (int i = 0; i < _jumpResults.Count && i < _cards.Length; i++)
        {
            _cards[i].Configure(i + 1, _jumpResults[i]);
            _cards[i].AnimateShow();
        }
        _currentIndex = _jumpResults.Count;
    }

    public void ResetCards()
    {
        foreach (var card in _cards)
        {
            card.gameObject.SetActive(false);
        }
        _currentIndex = 0;
        _jumpResults.Clear();
    }
}
