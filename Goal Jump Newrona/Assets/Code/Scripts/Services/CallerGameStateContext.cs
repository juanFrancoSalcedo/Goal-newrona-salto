using System.Collections;
using UnityEngine;

public class CallerGameStateContext: MonoBehaviour
{
    [SerializeField] private bool onAwake = false;
    [SerializeField] GameEventType type;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        if(onAwake)
            GameStateContext.ChangeState(type);
    }

    public void ChangeState()
    {
        GameStateContext.ChangeState(type);
    }

    [ContextMenu("Read State")]

    private void ReadState()
    {
        Debug.Log(GameStateContext.State);
    }
}