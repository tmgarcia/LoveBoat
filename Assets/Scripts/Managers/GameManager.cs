using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameConfig _gameConfig;
    public GameConfig Config { get { return _gameConfig; } }

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

    public void GoToTimeOfDay(TimeOfDay time)
    {
        if(time == TimeOfDay.Morning)
        {
            GoToNextDay();
        }
        else
        {
            GameStatus.Instance.CurrentTimeOfDay = time;
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
