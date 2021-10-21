using UnityEngine;
using UnityEngine.Events;

public class CharacterViewer : MonoBehaviour
{
    [SerializeField] private Character[] _characters;

    private Character _currentCharacter;

    public event UnityAction<Character> CharacterChanged;

    private void OnEnable()
    {
        _currentCharacter = _characters[0];
    }

    private void Start()
    {
        CharacterChanged?.Invoke(_currentCharacter);
    }

    public void InitCharacter(ConstantKeys.Characters name)
    {
        if (name.ToString() != _currentCharacter.name && name.ToString() != string.Empty)
        {
            foreach (var character in _characters)
            {
                if (character.name == name.ToString())
                {
                    _currentCharacter = character;
                    CharacterChanged?.Invoke(_currentCharacter);
                    return;
                }
            }
        }
    }

    public void MoveCharacter(bool isAppearing)
    {
        if (isAppearing)
        {
            _currentCharacter.Appear();
        }
        else
        {
            _currentCharacter.Dissapear();
        }
    }
}
