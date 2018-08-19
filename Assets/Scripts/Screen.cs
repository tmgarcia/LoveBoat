using UnityEngine;
using UnityEngine.Events;


public class Screen : MonoBehaviour
{
	[SerializeField] private string _name;
    public ScreenActivationEvent OnActiveChange = new ScreenActivationEvent();

    private bool _isActive;
	
	public string Name { get { return _name; } }
	
	public void SetActive(bool active)
	{
		_isActive = active;
        OnActiveChange.Invoke(_isActive);
		gameObject.SetActive(_isActive);
    }
}