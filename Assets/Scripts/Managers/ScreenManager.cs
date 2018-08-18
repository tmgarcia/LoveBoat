using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreenManager : MonoSingleton<ScreenManager> 
{
	[SerializeField] private List<Screen> _screens;
	[SerializeField] private Screen _startScreen;
	
	private Screen _activeScreen;
	
	void Awake()
	{
        InitializeScreens();
    }

    private void InitializeScreens()
    {
        foreach(var screen in _screens)
        {
            screen.SetActive(false);
        }
        _startScreen.SetActive(true);
    }

    public void GoToStartScreen()
    {
        SetScreenActive(_startScreen);
    }
	
	public void GoToScreen(string screenName)
	{
		var screen = _screens.FirstOrDefault<Screen>(s => s.Name == screenName);

        if(screen == null)
        {
            Debug.LogError("Cannot activate screen " + screenName);
            return;
        }

		SetScreenActive(screen);
	}

    private void SetScreenActive(Screen screen)
    {
        if (_activeScreen != null)
        {
            _activeScreen.SetActive(false);
        }

        screen.SetActive(true);
        _activeScreen = screen;
    }
}
