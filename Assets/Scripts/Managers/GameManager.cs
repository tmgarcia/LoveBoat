using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameConfig _gameConfig;
    public GameConfig Config { get { return _gameConfig; } }

    public ActionEvent OnActionStartEvent = new ActionEvent();

    private void Start()
    {
        if (_gameConfig == null)
            Debug.LogError("NO GAME CONFIG SET!! CANNOT DO GAME!!");
        else
            InitializeGame();
    }

    private void InitializeGame()
    {
        GameStatus.Instance.Initialize();
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
            ScreenManager.Instance.GoToScreen("actions");
        }
    }

    public void GoToNextDay()
    {
        GameStatus.Instance.CurrentDay += 1;
        GameStatus.Instance.CurrentTimeOfDay = TimeOfDay.Morning;

        if(!GameStatus.Instance.HasFood || GameStatus.Instance.BoatFullyRepaired || GameStatus.Instance.CurrentDay == Config.HurricaneDay)
        {
            EndGame();
        }
        else
        {
            ScreenManager.Instance.GoToScreen("actions");
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

    private void EndGame()
    {
        if (!GameStatus.Instance.HasFood)
        {
            // Starve Lose
        }
        else if(GameStatus.Instance.BoatFullyRepaired)
        {
            if(!GameStatus.Instance.BoatFullyLoved)
            {
                // Abandonment Lose
            }
            else if(GameStatus.Instance.CurrentDay >= Config.LateDay)
            {
                // Sacrifice Win
            }
            else
            {
                // True Win
            }
        }
        else
        {
            // Hurricane Lose
        }
    }

}
