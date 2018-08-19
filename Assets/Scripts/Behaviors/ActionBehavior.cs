using UnityEngine;

public abstract class ActionBehavior : MonoBehaviour
{
    [SerializeField] protected Action _action;

	void Awake ()
    {
        GameManager.Instance.OnActionStartEvent.AddListener(ActionStartListener);
	}

    void ActionStartListener(Action action)
    {
        if(action.Id == _action.Id)
            OnActionStart();
    }

    abstract protected void OnActionStart();

    virtual protected void AddDialogueLine(string text)
    {
        EventScreen.Instance.AddDialogueLine(text);
    }

    virtual protected void AddDialogueOption(DialogueOption option)
    {
        EventScreen.Instance.AddDialogueOption(option);
    }

    virtual protected void EndAction()
    {
        GameManager.Instance.EndAction(_action);
    }
}
