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

    virtual protected void SetDialogue(Dialogue dialogue)
    {
        EventScreen.Instance.SetDialogue(dialogue);
    }

    virtual protected void EndAction()
    {
        GameManager.Instance.EndAction(_action);
    }
}
