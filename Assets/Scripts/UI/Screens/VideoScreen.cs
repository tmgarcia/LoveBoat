using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoScreen : MonoBehaviour
{
    [SerializeField] private string _fileName;
    [SerializeField] private Button _optionalPlayButton;

    private VideoPlayer _videoPlayer = null;
    private Screen _screen = null;

	void OnEnable()
    {
        CreatePlayer();

        if (_screen == null)
        {
            _screen = gameObject.GetComponent<Screen>();

            if(_optionalPlayButton != null)
            {
                _optionalPlayButton.onClick.AddListener(PlayVideo);
                _optionalPlayButton.gameObject.SetActive(false);
            }
        }
        if (_screen.IsActive)
            ShowVideo();
    }

    private void ShowVideo()
    {
        AudioManager.Instance.StopPlayingMusic();

        CreatePlayer();

        if(_optionalPlayButton != null && !_optionalPlayButton.IsActive())
        {
            _optionalPlayButton.gameObject.SetActive(true);
        }
        else
        {
            if (_optionalPlayButton != null)
                _optionalPlayButton.gameObject.SetActive(false);

            StartCoroutine(Play());
        }
    }

    public void PlayVideo()
    {
        ShowVideo();
    }

    private void CreatePlayer()
    {
        if (_videoPlayer != null)
            return;

        var instance = new GameObject();
        _videoPlayer = instance.AddComponent<VideoPlayer>();

        _videoPlayer.errorReceived += (src, msg) => { print(msg); };

        _videoPlayer.targetCamera = Camera.main;
        _videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        _videoPlayer.playOnAwake = false;
        _videoPlayer.skipOnDrop = false;

        _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, _fileName);
        _videoPlayer.loopPointReached += OnVideoEnd;
        _videoPlayer.Prepare();
    }

    IEnumerator Play()
    {
        _videoPlayer.Prepare();
        while (!_videoPlayer.isPrepared)
        {
            print("preparing..." + _videoPlayer.frameCount);
            yield return null;
        }
        print("playing!" + _videoPlayer.frameCount);
        _videoPlayer.Play();
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
