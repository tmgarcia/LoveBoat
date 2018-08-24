using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Screen))]
[RequireComponent(typeof(DialogueScreen))]
public class EventScreen : MonoSingleton<EventScreen>
{
    private Screen _screen = null;
    private DialogueScreen _dialogueScreen = null;

    void OnEnable()
    {
        if (_screen == null)
        {
            _screen = gameObject.GetComponent<Screen>();
            _dialogueScreen = gameObject.GetComponent<DialogueScreen>();
            _screen.OnActiveChange.AddListener(OnScreenActiveChange);
        }
    }

    void OnScreenActiveChange(bool screenActive)
    {
        if (!screenActive)
        {
            _dialogueScreen.ClearAll();
        }
    }

    public void ClearAll()
    {
        _dialogueScreen.ClearAll();
    }

    public void SetDialogue(Dialogue dialogue)
    {
        _dialogueScreen.SetDialogue(dialogue);
    }

    public void SetDialogueOptions(List<DialogueOption> options)
    {
        _dialogueScreen.SetDialogueOptions(options);
    }
}