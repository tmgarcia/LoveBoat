using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private GameConfig _gameConfig;
    public GameConfig Config { get { return _gameConfig; } }

    public ActionEvent OnActionStartEvent = new ActionEvent();


    private void Start()
    {
        if (_gameConfig == null)
            Debug.LogError("NO GAME CONFIG SET!! CANNOT DO GAME!!");
        else
        {
            InitializeGame();
            ScreenManager.Instance.GoToScreen("intro");
        }
    }

    
    private void InitializeGame()
    {
        GameStatus.Instance.Initialize();
        Boat.Instance.Reset();
    }

    public void RestartGame()
    {
        InitializeGame();
        ScreenManager.Instance.GoToStartScreen();
    }

    public void GoNextTimeOfDay()
    {
        TimeOfDay nextTimeOfDay;
        switch (GameStatus.Instance.CurrentTimeOfDay)
        {
            case TimeOfDay.Morning:
                nextTimeOfDay = TimeOfDay.Afternoon;
                break;
            case TimeOfDay.Afternoon:
                nextTimeOfDay = TimeOfDay.Evening;
                break;
            case TimeOfDay.Evening:
                nextTimeOfDay = TimeOfDay.Morning;
                break;
            default:
                nextTimeOfDay = TimeOfDay.Morning;
                break;
        }

        if(nextTimeOfDay == TimeOfDay.Morning)
        {
            GoToNextDay();
        }
        else
        {
            GameStatus.Instance.CurrentTimeOfDay = nextTimeOfDay;
            GameStatus.Instance.CurrentWeather = GetRandomWeather();
            ScreenManager.Instance.GoToScreen("actions");
        }
    }

    private Weather GetRandomWeather()
    {
        if (!GameStatus.Instance.Flags["boatFound"].Status) { return Weather.Sunny; }
        var rand = Random.Range(0, 3);
        Weather weather;
        if (rand == 0)
            weather = Weather.Cloudy;
        else if (rand == 1)
            weather = Weather.Rainy;
        else
            weather = GameStatus.Instance.CurrentTimeOfDay == TimeOfDay.Evening ? Weather.Clear : Weather.Sunny;

        return weather;
    }

    public void GoToNextDay()
    {
        GameStatus.Instance.CurrentDay += 1;
        GameStatus.Instance.CurrentTimeOfDay = TimeOfDay.Morning;
        

        if(!GameStatus.Instance.HasFood || GameStatus.Instance.Flags["sail"].Status || GameStatus.Instance.CurrentDay == Config.HurricaneDay)
        {
            ScreenManager.Instance.GoToScreen("end");
        }
        else
        {
            GameStatus.Instance.FoodLevel.CurrentValue -= 1;
            //ScreenManager.Instance.GoToScreen("actions");
            ScreenManager.Instance.GoToScreen("day");
        }
    }

    public void StartAction(Action action)
    {
        ActionManager.Instance.ActiveAction = action;
        ScreenManager.Instance.GoToScreen("event");
        OnActionStartEvent.Invoke(action);
    }

    public void EndAction(Action action)
    {
        ActionManager.Instance.ActiveAction = null;
        // Do something with results of action? Rewards?
        GoNextTimeOfDay();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            _endScreen.ShortcutEnd = true;
            _endScreen.ShortcuttedEnding = 0;
            ScreenManager.Instance.GoToScreen("end");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            _endScreen.ShortcutEnd = true;
            _endScreen.ShortcuttedEnding = 1;
            ScreenManager.Instance.GoToScreen("end");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            _endScreen.ShortcutEnd = true;
            _endScreen.ShortcuttedEnding = 2;
            ScreenManager.Instance.GoToScreen("end");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            _endScreen.ShortcutEnd = true;
            _endScreen.ShortcuttedEnding = 3;
            ScreenManager.Instance.GoToScreen("end");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            _endScreen.ShortcutEnd = true;
            _endScreen.ShortcuttedEnding = 4;
            ScreenManager.Instance.GoToScreen("end");
        }
    }
}
