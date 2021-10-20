using Spine;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(SkeletonAnimation))]
public class SkinToggler : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _character;
    [SerializeField] private CharacterAnimationData _animationData;
    
    private Skin _body;
    private Skin _newLook;

    private void Start()
    {
        ChangeSkin();
    }

    public void ChangeSkin()
    {
        _body = _character.Skeleton.Data.FindSkin(_animationData.GetNextSkinName());
        _character.Skeleton.Skin = _body;
        _character.Skeleton.SetSlotsToSetupPose();
    }


    /*
    public void ChangeDress()
    {
        _newLook = new Skin("NewLook");

        var dress = _character.skeleton.Data.FindSkin(_animationData.GetNextDressName());

        _character.Skeleton.Skin = _body;

        _newLook.AddAttachments(_character.Skeleton.Skin);
        _newLook.AddAttachments(dress);

        _character.Skeleton.Skin = _newLook;
        _character.Skeleton.SetSlotsToSetupPose();
    }*/
}
