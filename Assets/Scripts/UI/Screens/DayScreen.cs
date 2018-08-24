using UnityEngine;
using UnityEngine.UI;

public class DayScreen : MonoBehaviour
{
    [SerializeField] private Text _dayText;
    [SerializeField] private Button _continueButton;

    private Screen _screen = null;

    void OnEnable()
    {
        if (_screen == null)
        {
            _screen = gameObject.GetComponent<Screen>();

            _continueButton.onClick.AddListener(OnClickContinue);
        }

        if (_screen.IsActive)
        {
            AudioManager.Instance.PlayMusic(_screen.Music, true);
            _dayText.text = "Day " + GameStatus.Instance.CurrentDay;
        }
    }
	
    void OnClickContinue()
    {
        ScreenManager.Instance.GoToScreen("actions");
    }
}
