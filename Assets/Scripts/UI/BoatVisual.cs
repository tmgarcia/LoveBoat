﻿using UnityEngine;
using UnityEngine.UI;

public class BoatVisual : MonoBehaviour
{
    [SerializeField] private GameObject _boatContainer;
    [SerializeField] private GameObject _sailContainer;
    [SerializeField] private Image _face;

    private bool _initialized = false;

    private void Awake()
    {
        if (!_initialized)
        {
            Boat.Instance.OnUpdate.AddListener(OnBoatUpdate);
            _initialized = true;
        }
    }

    void OnBoatUpdate()
    {
        _boatContainer.SetActive(Boat.Instance.IsVisible);
        _sailContainer.SetActive(Boat.Instance.HasSail);
        _face.sprite = Boat.Instance.BoatFaceSprites[Boat.Instance.CurrentFace];
    }
}
