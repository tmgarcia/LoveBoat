using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueScreen : MonoBehaviour
{
    [SerializeField] private Text _speakerLabel;
    [SerializeField] private Text _dialogueArea;
    [SerializeField] private GameObject _choiceContainer;
    [SerializeField] private Button _continueButton;
    [SerializeField] private GameObject _choicePrefab;

    private Dialogue _currentDialogue;
    private string _currentDisplayedDialogue;
    private int _nextDialogueIndex;

    void Start()
    {
        _continueButton.onClick.AddListener(OnContinueClick);
    }

    public void SetDialogue(Dialogue dialogue)
    {
        ClearAll();

        _currentDialogue = dialogue;
        NextDialogueLine();
    }

    public void SetDialogueOptions(List<DialogueOption> options)
    {
        foreach (DialogueOption option in options)
        {
            var optionObj = Instantiate(_choicePrefab, _choiceContainer.transform);
            var optionSelector = optionObj.GetComponent<DialogueOptionSelect>();
            optionSelector.SetOption(option);
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

    private void NextDialogueLine()
    {
        // We are not passed the last line
        if (_nextDialogueIndex < _currentDialogue.Lines.Count)
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
}
