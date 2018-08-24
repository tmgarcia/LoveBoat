using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScreen : MonoBehaviour
{
    [SerializeField] private string _fileName;

    private VideoPlayer _videoPlayer = null;
    private Screen _screen = null;

	void OnEnable()
    {
        CreatePlayer();

        if (_screen == null)
            _screen = gameObject.GetComponent<Screen>();
        if (_screen.IsActive)
            ShowVideo();
    }

    private void ShowVideo()
    {
        AudioManager.Instance.StopPlayingMusic();

        CreatePlayer();

        _videoPlayer.Play();
    }

    private void CreatePlayer()
    {
        if (_videoPlayer != null)
            return;

        var instance = new GameObject();
        _videoPlayer = instance.AddComponent<VideoPlayer>();

        _videoPlayer.targetCamera = Camera.main;
        _videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        _videoPlayer.playOnAwake = false;

        _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, _fileName);
        _videoPlayer.loopPointReached += OnVideoEnd;
        _videoPlayer.Prepare();
    }

    void OnVideoEnd(VideoPlayer player)
    {
        _videoPlayer.Stop();
        if(_screen.name == "credits")
            GameManager.Instance.RestartGame();
        else
            ScreenManager.Instance.GoToStartScreen();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnVideoEnd(null);
        }
    }
}
