using Spine.Unity;
using UnityEngine;

public class EmotionViewer : MonoBehaviour
{
    [SerializeField] private CharacterViewer _characterViewer;
    [SerializeField] private ConstantKeys.Emotions _emotionToView;

    private SkeletonAnimation _currentSkeleton;

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
        if (_emotionToView != emotion)
        {
            _emotionToView = emotion;
            var newTrack = _currentSkeleton.state.SetAnimation(0, emotion.ToString(), true);
            newTrack.MixDuration = ConstantKeys.GlobalKeys.AnimationMixDuration;
        }
        else return;
    }

    public void ResetEmotion()
    {
        var newTrack = _currentSkeleton.state.SetAnimation(0, ConstantKeys.Emotions.normal.ToString(), true);
        newTrack.MixDuration = ConstantKeys.GlobalKeys.AnimationMixDuration;
    }
}
