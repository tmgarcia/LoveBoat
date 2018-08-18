using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
	[SerializeField] private string _name;
	
	private bool _isActive;
	
	public string Name { get { return _name; } }
	
	public void SetActive(bool active)
	{
		_isActive = active;
		gameObject.SetActive(_isActive);
	}
}