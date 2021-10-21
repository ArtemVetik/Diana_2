using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization {

    [CreateAssetMenu(fileName = "SkinDataBase", menuName = "Castomization/SkinDataBase", order = 51)]
    public class SkinDataBase : ScriptableObject
    {
        [SerializeField] private List<SkinData> _skins = new List<SkinData>();

        public IEnumerable<SkinData> Data => _skins;

        public void Add(SkinData data)
        {
            _skins.Add(data);
        }

        public void Remove(SkinData data)
        {
            _skins.Remove(data);
        }

        public bool TryGetSkinByName(string skinName, out SkinData skinData)
        {
            skinData = null;
            foreach (var data in _skins)
            {
                if (data.Name == skinName)
                {
                    skinData = data;
                    return true;
                }
            }

            return false;
        }

        public void Clear() => _skins.Clear();
    }
}