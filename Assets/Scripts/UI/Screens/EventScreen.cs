using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Screen))]
public class EventScreen : MonoSingleton<EventScreen>
{
    [SerializeField] private Text _speakerLabel;
    [SerializeField] private Text _dialogueArea;
    [SerializeField] private GameObject _choiceContainer;
    [SerializeField] private Button _continueButton;

    [SerializeField] private GameObject _choicePrefab;

    private Dialogue _currentDialogue;
    private string _currentDisplayedDialogue;
    private int _nextDialogueIndex;

    private Screen _screen = null;

    void OnEnable()
    {
        if (_screen == null)
        {
            _screen = gameObject.GetComponent<Screen>();
            _screen.OnActiveChange.AddListener(OnScreenActiveChange);

            _continueButton.onClick.AddListener(OnContinueClick);
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
        _currentDialogue = null;
        _nextDialogueIndex = 0;
        _currentDisplayedDialogue = "";
        _continueButton.gameObject.SetActive(false);

        _dialogueArea.text = "";

        foreach (Transform child in _choiceContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void OnContinueClick()
    {
        NextDialogueLine();
    }

    public void SetDialogue(Dialogue dialogue)
    {
        ClearAll();

        _currentDialogue = dialogue;
        NextDialogueLine();
    }

    private void NextDialogueLine()
    {
        // We are not passed the last line
        if(_nextDialogueIndex < _currentDialogue.Lines.Count)
        {
            var nextLine = _currentDialogue.Lines[_nextDialogueIndex];

            //if (_nextDialogueIndex > 0)
                //_currentDisplayedDialogue += "\n";

            //_currentDisplayedDialogue += nextLine.Text;
            _dialogueArea.text = nextLine.Text;
            nextLine.Display();
            _speakerLabel.text = nextLine.Speaker;

            // Last Dialogue Option
            if (_nextDialogueIndex == _currentDialogue.Lines.Count - 1)
            {
                if (_currentDialogue.Options != null && _currentDialogue.Options.Count > 0)
                {
                    _currentDialogue.AllDialogueLinesShown();
                    _continueButton.gameObject.SetActive(false);
                    SetDialogueOptions(_currentDialogue.Options);
                }
                else
                {
                    _continueButton.gameObject.SetActive(true);
                }
            }
            // Not Last Dialogue Option
            else
            {
                _continueButton.gameObject.SetActive(true);
            }
            _nextDialogueIndex += 1;
        }
        // We passed the last line, trigger end of dialogue
        else
        {
            _currentDialogue.AllDialogueLinesShown();
        }
    }

    public void SetDialogueOptions(List<DialogueOption> options)
    {
        foreach(DialogueOption option in options)
        {
            var optionObj = Instantiate(_choicePrefab, _choiceContainer.transform);
            var optionSelector = optionObj.GetComponent<DialogueOptionSelect>();
            optionSelector.SetOption(option);
        }
    }
}

public class Dialogue
{
    public List<DialogueOption> Options { get; private set; }
    public List<DialogueLine> Lines { get; private set; }

    // Optional event to listen to for dialogues that don't have options
    private System.Action _onAllLinesShown;

    public Dialogue(List<DialogueLine> lines, List<DialogueOption> options, System.Action onAllLinesShown = null)
    {
        Lines = lines;
        Options = options;

        _onAllLinesShown = onAllLinesShown;
    }

    public void AllDialogueLinesShown()
    {
        if(_onAllLinesShown != null)
        {
            _onAllLinesShown();
        }
    }
}

public class DialogueLine
{
    public string Speaker { get; private set; }
    public string Text { get; private set; }
    private System.Action _onDisplay;

    public DialogueLine(string speaker, string text,System.Action onDisplay)
    {
        Speaker = speaker;
        Text = text;
        _onDisplay = onDisplay;
    }

    public void Display()
    {
        if(_onDisplay != null)
        {
            _onDisplay();
        }
    }
}

public class DialogueOption
{
    public string Text { get; private set; }
    private System.Action _onSelect;

    public DialogueOption(string text, System.Action onSelect)
    {
        Text = text;
        _onSelect = onSelect;
    }

    public void Select()
    {
        if (_onSelect != null)
        {
            _onSelect();
        }
    }
}