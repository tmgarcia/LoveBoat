using UnityEngine;

[RequireComponent(typeof(Screen))]
public class ActionSelectScreen : MonoBehaviour
{
    [SerializeField] private ActionSelector OutsideAction;
    [SerializeField] private ActionSelector InsideAction;
    [SerializeField] private ActionSelector BoatAction;

    void Awake ()
    {
        var screen = gameObject.GetComponent<Screen>();
        screen.OnActiveChange.AddListener(OnScreenActiveChange);

        OutsideAction.OnSelect.AddListener(OnActionSelected);
        InsideAction.OnSelect.AddListener(OnActionSelected);
        BoatAction.OnSelect.AddListener(OnActionSelected);
    }

    void OnActionSelected(Action action)
    {
        GameManager.Instance.StartAction(action);
    }

    void OnScreenActiveChange(bool screenActive)
    {
        if(screenActive)
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
}
