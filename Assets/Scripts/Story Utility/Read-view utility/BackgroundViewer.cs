using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundViewer : MonoBehaviour
{
    [SerializeField] private BackgroundData[] _backgroundsData;
    [SerializeField] private SpriteRenderer _backgroundSprite;

    private BackgroundData _currentData;

    public void SetBackground(ConstantKeys.Backgrounds name)
    {
        if (name.ToString() != _backgroundSprite.sprite.name && name.ToString() != string.Empty)
        {
            foreach (var data in _backgroundsData)
            {
                if (data.Sprite != null && data.Sprite.name == name.ToString())
                {
                    _currentData = data;
                    _backgroundSprite.sprite = data.Sprite;
                    return;
                }
            }
        }
    }

    public void SetBackgroundStartPosition(bool isMoving)
    {
        _backgroundSprite.transform.position = isMoving ? new Vector2(_currentData.MovementStart, _backgroundSprite.transform.position.y) : new Vector2(_currentData.FixedStart, _backgroundSprite.transform.position.y);
    }

    public void MoveBackground()
    {
        if(_backgroundSprite != null)
        {
            _backgroundSprite.transform.DOMoveX(_currentData.Finish, ConstantKeys.GlobalKeys.BackgroundMoveDuration);
        }
    }
}
