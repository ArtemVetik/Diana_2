using Spine.Unity;
using System.Collections;
using UnityEngine;

public class EmotionViewer : MonoBehaviour
{
    [SerializeField] private CharacterViewer _characterViewer;
    private SkeletonAnimation _currentSkeleton;
    private string _previousEmotion = string.Empty;

    private void OnEnable()
    {
        _characterViewer.CharacterChanged += OnCharacterChanged;
    }

    private void OnDisable()
    {
        _characterViewer.CharacterChanged -= OnCharacterChanged;
    }

    private void OnCharacterChanged(Character character)
    {
        _currentSkeleton = character.Skeleton;
    }

    public void ShowEmotion(ConstantKeys.Emotions emotion)
    {
        if (IsAnimationAvailable(emotion.ToString()))
        {
            if(emotion.ToString() != _previousEmotion)
            {
                var newTrack = _currentSkeleton.state.SetAnimation(0, emotion.ToString(), true);
                newTrack.MixDuration = ConstantKeys.GlobalKeys.AnimationMixDuration;
                _previousEmotion = emotion.ToString();
            }
        }
        else
        {
            Debug.LogError($"Emotion {emotion} doesn't found in {_currentSkeleton.name}");
        }
    }

    public void ResetEmotion()
    {
        var newTrack = _currentSkeleton.state.SetAnimation(0, ConstantKeys.Emotions.normal.ToString(), true);
        newTrack.MixDuration = ConstantKeys.GlobalKeys.AnimationMixDuration;
    }

    private bool IsAnimationAvailable(string name)
    {
        var animations = _currentSkeleton.skeleton.Data.Animations;

        foreach (var animation in animations)
        {
            if (animation.Name == name)
            {
                return true;
            }
        }

        return false;
    }
}
