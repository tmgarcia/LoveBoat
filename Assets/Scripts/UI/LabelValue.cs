using UnityEngine;
using UnityEngine.UI;

public class LabelValue : MonoBehaviour
{
    [SerializeField] private Text _label;
    [SerializeField] private Text _value;

    public void SetLabel(string text)
    {
        _label.text = text;
    }

    public void SetValue(string text)
    {
        _value.text = text;
    }
}
