using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Screen))]
public class MainScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;

	void Start ()
    {
        _startButton.onClick.AddListener(OnStartHandler);

    }

    void OnStartHandler()
    {
        ScreenManager.Instance.GoToScreen("actions");
    }
}
