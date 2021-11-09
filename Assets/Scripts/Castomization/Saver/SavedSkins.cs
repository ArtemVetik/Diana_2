using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization
{
    [Serializable]
    public class SavedSkins
    {
        [SerializeField] private List<string> _savedGuid = new List<string>();

        private SkinDataBase _dataBase;

        public SavedSkins(SkinDataBase dataBase)
        {
            _dataBase = dataBase;

            foreach (var skin in _dataBase.Data)
                if (skin.IsPremanent)
                    if (_dataBase.TryGetSkinByName(skin.Name, out SkinData skinData))
                        AddSkin(skinData);
        }

        public IEnumerable<SkinData> Data => from data in _dataBase.Data
                                             where _savedGuid.Contains(data.GUID)
                                             select data;

        public void AddSkin(SkinData skinData)
        {
            if (_savedGuid.Contains(skinData.GUID))
                return;

            _savedGuid.Add(skinData.GUID);
        }

        public void RemoveAllSkinByKey(string skinKey)
        {
            var allSkins = Data;

            foreach (var skin in allSkins)
            {
                if (skin.GetSkinKey() == skinKey)
                    RemoveSkin(skin);
            }
        }

        public void RemoveSkin(SkinData skinData)
        {
            _savedGuid.Remove(skinData.GUID);
        }

        public void Save()
        {
            var jsonString = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(_dataBase.SaveKey, jsonString);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(_dataBase.SaveKey) == false)
                return;

            var jsonString = PlayerPrefs.GetString(_dataBase.SaveKey);
            var saved = JsonUtility.FromJson<SavedSkins>(jsonString);

            _savedGuid = saved._savedGuid;
        }
    }
}