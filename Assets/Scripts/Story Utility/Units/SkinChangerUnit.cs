using Diana2.Castomization;
using UnityEngine;


[CreateAssetMenu(fileName = "New unit", menuName = "Story/Create new skin changer")]
public class SkinChangerUnit : VariantUnit
{
    [SerializeField] private SkinPreset[] _availableSkinPresets;
    [SerializeField] private SkinInitializer _dianaModel;

    public void ChangeSkin(int index)
    {
        _dianaModel.LoadPreset(_availableSkinPresets[index]);
    }
}

