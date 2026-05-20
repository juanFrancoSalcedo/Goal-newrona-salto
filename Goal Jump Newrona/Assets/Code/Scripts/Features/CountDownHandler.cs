using Features;
using UnityEngine;

[RequireComponent(typeof(CountdownTimer))]
public class CountDownHandler : MonoBehaviour 
{
    [SerializeField] GameEventType gameEventType;
    CountdownTimer countdownTimer;

    private void Start() => countdownTimer = GetComponent<CountdownTimer>();

    private void OnEnable() => GameStateContext.GameStateMediator.Subscribe(gameEventType, StartCountDown);

    private void OnDisable() => GameStateContext.GameStateMediator.Unsubscribe(gameEventType, StartCountDown);

    private void StartCountDown() => countdownTimer.Begin();
}