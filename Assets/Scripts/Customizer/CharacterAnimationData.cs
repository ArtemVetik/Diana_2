using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Create new character")]
public class CharacterAnimationData : ScriptableObject
{
    [SerializeField] private SkeletonDataAsset _characterData;
    [SerializeField] private List<string> _animationKeys = new List<string>();
    [SerializeField] private List<string> _skinsKeys = new List<string>();

    private int _skinIndex = 0;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
    }

    public string GetAnimationNameByIndex(int index)
    {
        if (index > _animationKeys.Count - 1)
        {
            return string.Empty;
        }
        else
        {
            return _animationKeys[index];
        }
    }

    public string GetNextSkinName()
    {
        _skinIndex++;

        if (_skinIndex > _skinsKeys.Count - 1)
        {
            _skinIndex = 0;
        }

        return _skinsKeys[_skinIndex];
    }

    private void Initialize()
    {
        if (_characterData != null)
        {
            var animations = _characterData.GetSkeletonData(false).Animations;
            var skins = _characterData.GetSkeletonData(false).Skins;

            _animationKeys.Clear();
            _skinsKeys.Clear();

            foreach (var animation in animations)
            {
                _animationKeys.Add(animation.Name);
            }

            foreach (var skin in skins)
            {
                _skinsKeys.Add(skin.Name);
            }

            _skinsKeys.RemoveAt(0);
        }
    }
}
