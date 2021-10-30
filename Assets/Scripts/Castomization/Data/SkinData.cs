using System;
using UnityEngine;

namespace Diana2.Castomization
{
    [Serializable]
    public class SkinData : GUIDData
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _preview;
        [SerializeField] private bool _isPremanent;

        public SkinData(string name, Sprite preview, bool isPermanent = false)
        {
            _name = name;
            _preview = preview;
            _isPremanent = isPermanent;
        }

        public string Name => _name;
        public Sprite Preview => _preview;
        public bool IsPremanent => _isPremanent;
    }
}
