using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

namespace Diana2.Castomization
{
    public class SkinChanger : MonoBehaviour
    {
        [SerializeField] private SkinDataBase _dataBase;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        private Dictionary<string, string> _selectedSkins;

        private void Start()
        {
            InitializeSkins();
        }

        private void InitializeSkins()
        {
            var savedSkins = new SavedSkins(_dataBase);
            savedSkins.Load();

            _selectedSkins = new Dictionary<string, string>();

            foreach (var skin in savedSkins.Data)
                _selectedSkins.Add(skin.GetSkinKey(), skin.Name);

            RebuildSkin();
        }

        public void SelectSkin(SkinData skinData)
        {
            var key = skinData.GetSkinKey();

            if (_selectedSkins.ContainsKey(key))
                _selectedSkins[key] = skinData.Name;
            else
                _selectedSkins.Add(key, skinData.Name);

            RebuildSkin();
        }

        private void RebuildSkin()
        {
            Skin skin = new Skin("SpineSkin");
            Skeleton skeleton = _skeletonAnimation.skeleton;
            SkeletonData skeletonData = skeleton.Data;

            foreach (var value in _selectedSkins.Values)
            {
                var newSkin = skeletonData.FindSkin(value);
                skin.AddSkin(newSkin);
            }

            _skeletonAnimation.skeleton.SetSkin(skin);
            skeleton.SetSlotsToSetupPose();
            _skeletonAnimation.Update(0);
        }

        public void SaveSkins()
        {
            var savedSkins = new SavedSkins(_dataBase);
            foreach (var skinName in _selectedSkins.Values)
            {
                if (_dataBase.TryGetSkinByName(skinName, out SkinData data))
                    savedSkins.AddSkin(data);
            }

            savedSkins.Save();
        }

        public void ResetSkins()
        {
            var saved = new SavedSkins(_dataBase);
            saved.Save();

            InitializeSkins();
        }
    }
}