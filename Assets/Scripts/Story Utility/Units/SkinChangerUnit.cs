using Diana2.Castomization;
using UnityEngine;


[CreateAssetMenu(fileName = "New unit", menuName = "Story/Create new skin changer")]
public class SkinChangerUnit : VariantUnit
{
    [SerializeField] private SkinPreset[] _availableSkinPresets;

    public SkinPreset GetSkinPreset(int index)
    {
        return _availableSkinPresets[index];
    }
}

