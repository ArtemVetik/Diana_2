using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class GUIDData
{
#if UNITY_EDITOR
    [ReadOnly]
#endif
    [SerializeField]
    private string _guid;

    public string GUID => _guid;

    public GUIDData()
    {
        if (string.IsNullOrEmpty(_guid))
            _guid = Guid.NewGuid().ToString();
    }
}
