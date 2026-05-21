using B_Extensions;
using Services;

public class ButtonPassToForm: BaseButtonAttendant
{
    void Start()
    {
        buttonComponent.onClick.AddListener(PassToForm);
    }
    private void PassToForm()
    {
        if (GameStateContext.State != GameEventType.Intro)
            return;
        GameStateContext.ChangeState(GameEventType.Form);
    }

    bool jump = false;
    private void Update()
    {
        if (GameStateContext.State != GameEventType.Intro)
            return;

        if (InputService.Instance.IsAnyAxisMoved())
        {
            jump = true;
        }

        if (jump && !InputService.Instance.IsAnyAxisMoved())
        {
            jump = false;
            Click();
        }
    }
}