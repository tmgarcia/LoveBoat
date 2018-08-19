using UnityEngine;
using UnityEngine.UI;

public class DayScreen : MonoBehaviour
{
    [SerializeField] private Text _dayText;
    [SerializeField] private Button _continueButton;

    private bool _initialized = false;

	void Start ()
    {
		if(!_initialized)
        {
            _continueButton.onClick.AddListener(OnClickContinue);
            _initialized = true;
        }
	}
	
	void Update ()
    {
        _dayText.text = "Day " + GameStatus.Instance.CurrentDay;	
	}

    void OnClickContinue()
    {
        ScreenManager.Instance.GoToScreen("actions");
    }
}
