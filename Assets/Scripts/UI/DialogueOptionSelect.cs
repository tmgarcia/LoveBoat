using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionSelect : MonoBehaviour
{
    [SerializeField] private Text _label;
    [SerializeField] private Button _button;

    public DialogueOption Option { get; private set; }

    void Awake()
    {
        _button.onClick.AddListener(SelectOption);
    }

    public void SetOption(DialogueOption option)
    {
        Option = option;
        _label.text = Option.Text;
    }

    void SelectOption()
    {
        Option.Select();
    }
}
