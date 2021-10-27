using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundViewer : MonoBehaviour
{
    [SerializeField] private Sprite[] _backgrounds;

    private SpriteRenderer _backgroundSprite;

    private void Awake()
    {
        _backgroundSprite = GetComponent<SpriteRenderer>();
    }

    public void SetBackground(ConstantKeys.Backgrounds name)
    {
        _backgroundSprite.transform.position = new Vector2(0, transform.position.y);

        if (name.ToString() != _backgroundSprite.sprite.name && name.ToString() != string.Empty)
        {
            foreach (var sprite in _backgrounds)
            {
                if (sprite != null && sprite.name == name.ToString())
                {
                    _backgroundSprite.sprite = sprite;
                    return;
                }
            }
        }
    }

    public void MoveBackground(BackgroundMovementData data)
    {
        _backgroundSprite.transform.position = new Vector2(data.Start, transform.position.y);
        _backgroundSprite.transform.DOMoveX(data.Finish, ConstantKeys.GlobalKeys.BackgroundMoveDuration);
    }
}
