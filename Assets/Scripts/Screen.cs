﻿using UnityEngine;
using UnityEngine.Events;


public class Screen : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _music;
    public ScreenActivationEvent OnActiveChange = new ScreenActivationEvent();

    public bool IsActive { get; private set; }

    public string Name { get { return _name; } }
    public AudioClip Music { get { return _music; } }
	
	public void SetActive(bool active)
	{
        IsActive = active;
        OnActiveChange.Invoke(IsActive);
		gameObject.SetActive(IsActive);
    }
}