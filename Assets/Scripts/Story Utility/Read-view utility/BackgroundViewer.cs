using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundViewer : MonoBehaviour
{
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private SpriteRenderer _background;

    public void SetBackground(ConstantKeys.Backgrounds name)
    {
        if (name.ToString() != _background.name && name.ToString() != string.Empty)
        {
            foreach (var bg in _backgroundSprites)
            {
                if (bg.name == name.ToString())
                {
                    _background.sprite = bg;
                    return;
                }
            }
        }
        else
        {
            Debug.LogError($"Background name {_background.name} = {name}!");
        }
    }
}
