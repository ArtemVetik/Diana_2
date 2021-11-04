using System;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization
{
    [CreateAssetMenu(fileName = "SkinPreset", menuName = "Castomization/SkinPreset", order = 51)]
    public class SkinPreset : ScriptableObject
    {
        [SerializeField] private SkinDataBase _dataBase;
        [SerializeField] private List<string> _skins = new List<string>();

        public IEnumerable<string> Skins => _skins;

        public void Add(string skin)
        {
            _skins.Add(skin);
        }

        public void RemoveLast()
        {
            if (_skins.Count > 0)
                _skins.RemoveAt(_skins.Count - 1);
        }

        public void Replace(int index, string skin)
        {
            _skins[index] = skin;
        }

        public bool Contains(string skin)
        {
            return _skins.Contains(skin);
        }

        public void RemoveAt(int index)
        {
            _skins.RemoveAt(index);
        }
    }
}