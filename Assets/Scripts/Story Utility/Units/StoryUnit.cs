using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New unit", menuName = "Story/Create new story unit")]
public class StoryUnit : ScriptableObject
{
    [SerializeField] protected string[] Phrases;
    [SerializeField] private ConstantKeys.PhraseTypes _type;

    [SerializeField] private List<ConstantKeys.Emotions> _emotions = new List<ConstantKeys.Emotions>();
    [SerializeField] private ConstantKeys.Backgrounds _background;
    [SerializeField] private ConstantKeys.Characters _character;

    [SerializeField] private bool _showCharacterOnStart = false;
    [SerializeField] private bool _hideCharacterOnFinish = false;
    [SerializeField] private bool _moveBackgroundOnStart = false;
    [SerializeField] private bool _fadeIn = false;
    [SerializeField] private bool _fadeOut = false;
    [SerializeField] private bool _lastUnitInPart = false;

    public int Lenght => Phrases.Length;
    public ConstantKeys.PhraseTypes Type => _type;
    public ConstantKeys.Backgrounds Background => _background;
    public ConstantKeys.Characters Character => _character;
    public bool ShowCharacterOnStart => _showCharacterOnStart;
    public bool HideCharacterOnFinish => _hideCharacterOnFinish;
    public bool MoveBackgroundOnStart => _moveBackgroundOnStart;
    public bool FadeIn => _fadeIn;
    public bool FadeOut => _fadeOut;
    public bool LastUnitInPart => _lastUnitInPart;

    public bool HasEmotions => _emotions.Count > 0;

    public string GetPhrase(int index)
    {
        return Phrases[index];
    }

    public ConstantKeys.Emotions GetEmotion(int index)
    {
        return _emotions[index];
    }

}
