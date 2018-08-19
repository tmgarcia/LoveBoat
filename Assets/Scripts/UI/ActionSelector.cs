using UnityEngine;
using UnityEngine.UI;

public class ActionSelector: MonoBehaviour
{
    [SerializeField] private Text _label;
    [SerializeField] private Button _button;

    private Action _action;
    public ActionSelectEvent OnSelect = new ActionSelectEvent();

    void Start()
    {
        _button.onClick.AddListener(OnSelectHandler);
    }

    public void SetAction(Action action)
    {
        _action = action;
        if(_action != null)
        {
            _label.text = action.Label;
        }
        else
        {
            _label.text = "";
        }
    }

    public void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    void OnSelectHandler()
    {
        OnSelect.Invoke(_action);
    }
}
