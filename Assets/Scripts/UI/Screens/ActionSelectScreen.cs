using UnityEngine;

[RequireComponent(typeof(Screen))]
public class ActionSelectScreen : MonoBehaviour
{
    [SerializeField] private ActionSelector OutsideAction;
    [SerializeField] private ActionSelector InsideAction;
    [SerializeField] private ActionSelector BoatAction;

    private Screen _screen = null;

    void OnEnable()
    {
        if (_screen == null)
        {
            _screen = gameObject.GetComponent<Screen>();

            OutsideAction.OnSelect.AddListener(OnActionSelected);
            InsideAction.OnSelect.AddListener(OnActionSelected);
            BoatAction.OnSelect.AddListener(OnActionSelected);
        }

        if(_screen.IsActive)
        {
            SetActionOptions();
        }
    }

    void OnActionSelected(Action action)
    {
        GameManager.Instance.StartAction(action);
    }

    void SetActionOptions()
    {
        var newActions = ActionManager.Instance.GetAvailableActions();

        if (newActions.ContainsKey(ActionLocation.OutsideCamp))
        {
            OutsideAction.SetAction(newActions[ActionLocation.OutsideCamp]);
            OutsideAction.SetEnabled(true);
        }
        else
        {
            OutsideAction.SetEnabled(false);
            OutsideAction.SetAction(null);
        }

        if (newActions.ContainsKey(ActionLocation.InsideCamp))
        {
            InsideAction.SetAction(newActions[ActionLocation.InsideCamp]);
            InsideAction.SetEnabled(true);
        }
        else
        {
            InsideAction.SetEnabled(false);
            InsideAction.SetAction(null);
        }

        if (newActions.ContainsKey(ActionLocation.Boat))
        {
            BoatAction.SetAction(newActions[ActionLocation.Boat]);
            BoatAction.SetEnabled(true);
        }
        else
        {
            BoatAction.SetEnabled(false);
            BoatAction.SetAction(null);
        }
    }
}
