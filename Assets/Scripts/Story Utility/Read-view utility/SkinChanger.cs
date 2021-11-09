using Diana2.Castomization;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private SkinInitializer _diana;
    [SerializeField] private UnitViewHandler _viewHandler;

    private void OnEnable()
    {
        _viewHandler.SkinChangerUnitCompleted += OnSkinChangerUnitCompleted;
    }

    private void OnDisable()
    {
        _viewHandler.SkinChangerUnitCompleted -= OnSkinChangerUnitCompleted;
    }

    private void OnSkinChangerUnitCompleted(SkinChangerUnit unit, int index)
    {
        _diana.ReloadSavedSkin();
        _diana.LoadPreset(unit.GetSkinPreset(index));
        
        Debug.LogError("CHANGE SKIN");
    }
}
