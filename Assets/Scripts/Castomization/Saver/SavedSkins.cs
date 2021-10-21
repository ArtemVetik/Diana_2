using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization
{
    public enum SaveModel
    {
        Diana,
    }

    [Serializable]
    public class SavedSkins
    {
        [SerializeField] private List<string> _savedGuid = new List<string>();

        private SkinDataBase _dataBase;
        private SaveModel _targetModel;

        public SavedSkins(SaveModel saveModel, SkinDataBase dataBase)
        {
            _dataBase = dataBase;
            _targetModel = saveModel;

            dataBase.TryGetSkinByName(SaveModelConverter.GetInitialSkin(saveModel), out SkinData initialSkin);
            AddSkin(initialSkin);
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

        public void RemoveSkin(SkinData skinData)
        {
            _savedGuid.Remove(skinData.GUID);
        }

        public void Save()
        {
            var jsonString = JsonUtility.ToJson(this);
            var saveKey = SaveModelConverter.GetSaveKey(_targetModel);
            PlayerPrefs.SetString(saveKey, jsonString);
        }

        public void Load()
        {
            var saveKey = SaveModelConverter.GetSaveKey(_targetModel);

            if (PlayerPrefs.HasKey(saveKey) == false)
                return;

            var jsonString = PlayerPrefs.GetString(saveKey);
            var saved = JsonUtility.FromJson<SavedSkins>(jsonString);

            _savedGuid = saved._savedGuid;
        }
    }

    internal static class SaveModelConverter
    {
        public static string GetSaveKey(SaveModel saveModel)
        {
            if (saveModel == SaveModel.Diana)
                return "DianaSkinSaveKey";

            throw new ArgumentOutOfRangeException("Can't find save model");
        }

        public static string GetInitialSkin(SaveModel saveModel)
        {
            if (saveModel == SaveModel.Diana)
                return "Diana_skin";

            throw new ArgumentOutOfRangeException("Can't find save model");
        }
    }
}