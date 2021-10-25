using UnityEngine;
using UnityEngine.UI;

public class PhraseViewer : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField] private Image _bubble;

    [SerializeField] private Sprite _dianaSpeechBubble;  //заменить на выпадающий список в инспекторе
    [SerializeField] private Sprite _dianaThougthBubble;
    [SerializeField] private Sprite _autorBubble;
    [SerializeField] private Sprite _npcBubble;

    public void SetPhraseBubble(ConstantKeys.PhraseTypes type)
    {
        switch (type)
        {
            case ConstantKeys.PhraseTypes.Thought:
                _bubble.sprite = _dianaThougthBubble;
                break;
            case ConstantKeys.PhraseTypes.Speech:
                _bubble.sprite = _dianaSpeechBubble;
                break;
            case ConstantKeys.PhraseTypes.Author:
                _bubble.sprite = _autorBubble;
                break;
            case ConstantKeys.PhraseTypes.Choise:
                break;
            case ConstantKeys.PhraseTypes.Npc:
                _bubble.sprite = _npcBubble;
                break;
            default:
                Debug.LogError("TYPE NOT FOUND");
                break;
        }
    }

    public void SetPhrase(string phrase)
    {
        _textField.text = phrase;
    }

    public void ToggleTextField(bool value)
    {
        _textField.gameObject.SetActive(value);

    }

    public void ToggleBubble(bool value)
    {
        _bubble.gameObject.SetActive(value);
    }

    public void ViewStandartUnit(ConstantKeys.PhraseTypes type, string phrase)
    {
        if(phrase != string.Empty)
        {
            ToggleBubble(true);
            ToggleTextField(true);
            SetPhraseBubble(type);
            SetPhrase(phrase);
        }
    }

    public void ViewVariableUnit()
    {
        ToggleBubble(true);
        ToggleTextField(false);
        SetPhraseBubble(ConstantKeys.PhraseTypes.Choise);
    }
}
