using UnityEngine;

public class CharacterViewer : MonoBehaviour
{
    [SerializeField] private Character[] _characters;

    private Character _character;
    private bool _hide = false;

    private void OnEnable()
    {
        _character = _characters[0];
    }

    public void InitCharacter(ConstantKeys.Characters name)
    {
        if (name.ToString() != _character.name && name.ToString() != string.Empty)
        {
            foreach (var character in _characters)
            {
                if (character.name == name.ToString())
                {
                    _character = character;
                    return;
                }
            }
        }
    }

    public void MoveCharacter(bool isAppearing)
    {
        if (isAppearing)
        {
            _character.Appear();
        }
        else
        {
            _character.Dissapear();
        }
    }
}
