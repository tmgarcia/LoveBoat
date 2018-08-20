using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private VideoClip _videoClip;

    private Screen _screen = null;

	void OnEnable()
    {
        if (_screen == null)
            _screen = gameObject.GetComponent<Screen>();
        if (_screen.IsActive)
            ShowVideo();
    }
	
    private void ShowVideo()
    {
        _videoPlayer.playOnAwake = false;
        _videoPlayer.clip = _videoClip;
        _videoPlayer.loopPointReached += OnVideoEnd;
        _videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer player)
    {
        _videoPlayer.Stop();
        _videoPlayer.clip = null;
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
