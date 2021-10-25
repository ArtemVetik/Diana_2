using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class SkinInitializer : MonoBehaviour
    {
        [SerializeField] private SaveModel _targetModel;
        [SerializeField] private SkinDataBase _dataBase;

        private SkeletonAnimation _skeletonAnimation;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void Start()
        {
            var savedSkin = new SavedSkins(_targetModel, _dataBase);
            savedSkin.Load();

            Skin skin = new Skin("SpineSkin");
            Skeleton skeleton = _skeletonAnimation.skeleton;
            SkeletonData skeletonData = skeleton.Data;

            foreach (var skinData in savedSkin.Data)
            {
                var newSkin = skeletonData.FindSkin(skinData.Name);
                skin.AddSkin(newSkin);
            }

            _skeletonAnimation.skeleton.SetSkin(skin);
            skeleton.SetSlotsToSetupPose();
            _skeletonAnimation.Update(0);
        }
    }
}