using UnityEngine;

[RequireComponent(typeof(Screen))]
public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject _starvedScreen;
    [SerializeField] private GameObject _abandonedScreen;
    [SerializeField] private GameObject _sacrificedScreen;
    [SerializeField] private GameObject _trueWinScreen;
    [SerializeField] private GameObject _hurricaneScreen;

    private Screen _screen = null;

    void OnEnable()
    {
        if (_screen == null)
            _screen = gameObject.GetComponent<Screen>();
        if (_screen.IsActive)
            ShowEnding();
    }

    public void OnRestartClick()
    {
        DisableAll();
        GameManager.Instance.RestartGame();
    }

    public void OnCreditsClick()
    {
        DisableAll();
        ScreenManager.Instance.GoToScreen("credits");
    }

    void DisableAll()
    {
        _starvedScreen.SetActive(false);
        _abandonedScreen.SetActive(false);
        _sacrificedScreen.SetActive(false);
        _trueWinScreen.SetActive(false);
        _hurricaneScreen.SetActive(false);
    }

    void ShowEnding()
    {
        Debug.Log("OnScreenActiveChange");
        if (!GameStatus.Instance.HasFood)
        {
            _starvedScreen.SetActive(true);
        }
        else if (GameStatus.Instance.BoatFullyRepaired)
        {
            if (!GameStatus.Instance.BoatFullyLoved)
            {
                _abandonedScreen.SetActive(true);
            }
            else if (GameStatus.Instance.CurrentDay >= GameManager.Instance.Config.LateDay)
            {
                _sacrificedScreen.SetActive(true);
            }
            else
            {
                _trueWinScreen.SetActive(true);
            }
        }
        else
        {
            _hurricaneScreen.SetActive(true);
        }
    }
}
