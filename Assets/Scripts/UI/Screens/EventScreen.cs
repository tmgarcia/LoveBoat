using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Screen))]
public class EventScreen : MonoBehaviour
{
    [SerializeField] private Text _actionLabel;
    [SerializeField] private Button _endButton;

    private Action _activeAction;

	void Start ()
    {
        var screen = gameObject.GetComponent<Screen>();
        screen.OnActiveChange.AddListener(OnScreenActiveChange);

        _endButton.onClick.AddListener(OnActionEndHandler);
    }
	
    void OnScreenActiveChange(bool screenActive)
    {
        if(screenActive)
        {
            _activeAction = ActionManager.Instance.ActiveAction;
        }
    }

    void OnActionEndHandler()
    {
        GameManager.Instance.EndAction(_activeAction);
        _activeAction = null;
    }
}
