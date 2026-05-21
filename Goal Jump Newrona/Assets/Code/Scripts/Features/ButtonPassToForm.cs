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

        if (InputService.Instance.GetAxisHorizontal() != 0 || InputService.Instance.GetAxisVertical() != 0)
        {
            jump = true;

        }

        if (jump && InputService.Instance.GetAxisHorizontal() != 0 || InputService.Instance.GetAxisVertical() != 0)
        {
            jump = false;
            Click();
        }
    }
}