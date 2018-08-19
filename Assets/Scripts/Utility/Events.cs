using UnityEngine.Events;

[System.Serializable]
public class ScreenActivationEvent : UnityEvent<bool> { }

[System.Serializable]
public class ActionSelectEvent : UnityEvent<Action> { }