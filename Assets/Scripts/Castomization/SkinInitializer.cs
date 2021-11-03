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
        [SerializeField] private SkinDataBase _dataBase;

        private SkeletonAnimation _skeletonAnimation;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void Start()
        {
            ReloadSavedSkin();
        }

        private void Initialize(SavedSkins savedSkins)
        {
            Skin skin = new Skin("SpineSkin");
            Skeleton skeleton = _skeletonAnimation.skeleton;
            SkeletonData skeletonData = skeleton.Data;

            foreach (var skinData in savedSkins.Data)
            {
                var newSkin = skeletonData.FindSkin(skinData.Name);
                skin.AddSkin(newSkin);
            }

            _skeletonAnimation.skeleton.SetSkin(skin);
            skeleton.SetSlotsToSetupPose();
            _skeletonAnimation.Update(0);
        }

        public void ReloadSavedSkin()
        {
            var savedSkins = new SavedSkins(_dataBase);
            savedSkins.Load();

            Initialize(savedSkins);
        }

        public void LoadPreset(SkinPreset preset)
        {
            Skin skin = new Skin("SpineSkin");
            Skeleton skeleton = _skeletonAnimation.skeleton;
            SkeletonData skeletonData = skeleton.Data;

            foreach (var skinName in preset.Skins)
            {
                var newSkin = skeletonData.FindSkin(skinName);
                skin.AddSkin(newSkin);
            }

            _skeletonAnimation.skeleton.SetSkin(skin);
            skeleton.SetSlotsToSetupPose();
            _skeletonAnimation.Update(0);
        }
    }
}