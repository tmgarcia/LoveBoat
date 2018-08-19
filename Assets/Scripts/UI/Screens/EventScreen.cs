using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Screen))]
public class EventScreen : MonoSingleton<EventScreen>
{
    [SerializeField] private Text _dialogueArea;
    [SerializeField] private GameObject _choiceContainer;

    [SerializeField] private GameObject _choicePrefab;

    private List<string> _dialogueLines = new List<string>();
    private List<DialogueOption> _dialogueOptions = new List<DialogueOption>();

    private Screen _screen = null;

    void OnEnable()
    {
        if (_screen == null)
        {
            _screen = gameObject.GetComponent<Screen>();
            _screen.OnActiveChange.AddListener(OnScreenActiveChange);
        }
    }

    void OnScreenActiveChange(bool screenActive)
    {
        if (!screenActive)
        {
            ClearAll();
        }
    }

    public void ClearAll()
    {
        _dialogueLines = new List<string>();
        _dialogueOptions = new List<DialogueOption>();

        _dialogueArea.text = "";

        foreach (Transform child in _choiceContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void AddDialogueLine(string text)
    {
        _dialogueLines.Add(text);

        // TODO: Not all this
        var combinedDialogue = string.Join("\n", _dialogueLines.ToArray());
        _dialogueArea.text = combinedDialogue;
    }

    public void AddDialogueOption(DialogueOption option)
    {
        _dialogueOptions.Add(option);
        var optionObj = Instantiate(_choicePrefab, _choiceContainer.transform);
        var optionSelector = optionObj.GetComponent<DialogueOptionSelect>();
        optionSelector.SetOption(option);
    }
}

public class DialogueOption
{
    public string Text { get; private set; }
    public UnityEvent OnSelect = new UnityEvent();

    public DialogueOption(string text)
    {
        Text = text;
    }
}