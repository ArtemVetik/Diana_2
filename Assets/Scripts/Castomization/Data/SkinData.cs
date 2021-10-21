using System;
using UnityEngine;

namespace Diana2.Castomization
{
    [Serializable]
    public class SkinData : GUIDData
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _preview;

        public SkinData(string name, Sprite preview)
        {
            _name = name;
            _preview = preview;
        }

        public string Name => _name;
        public Sprite Preview => _preview;
    }
}
