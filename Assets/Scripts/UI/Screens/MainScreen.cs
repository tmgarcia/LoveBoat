using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Screen))]
public class MainScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private Screen _screen = null;

    void OnEnable()
    {
        if (_screen == null)
        {
            _screen = gameObject.GetComponent<Screen>();
            _startButton.onClick.AddListener(OnStartHandler);
        }
    }

    void OnStartHandler()
    {
        ScreenManager.Instance.GoToScreen("actions");
    }
}
