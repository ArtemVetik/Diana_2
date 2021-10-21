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
        Debug.LogError("SHOW EMOTION");

        if (_emotionToView != emotion)
        {
            _emotionToView = emotion;
            var newTrack = _currentSkeleton.state.SetAnimation(0, emotion.ToString(), true);
            newTrack.MixDuration = ConstantKeys.GlobalKeys.Delay;
        }
        else return;
    }

    public void ResetEmotion()
    {
        Debug.LogError("RESET EMOTION");
        var newTrack = _currentSkeleton.state.SetAnimation(0, ConstantKeys.Emotions.normal.ToString(), true);
        newTrack.MixDuration = ConstantKeys.GlobalKeys.Delay;
    }
}
