using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkeletonAnimation))]
public class AnimationToggler : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _character;
    [SerializeField] private CharacterAnimationData _animationData;

    private string _currentSkin = string.Empty;
    private string _currentDress = string.Empty;
    private string _dependedAnimationName = string.Empty;

    public void PlayAnimation(int index)
    {
        string name = _animationData.GetAnimationNameByIndex(index);

        _character.AnimationState.ClearTrack(0);
        _character.AnimationState.AddAnimation(0, name, true, 0);
    }
}
