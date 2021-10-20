using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Part", menuName = "Story/Create new story part")]
public class StoryPart : ScriptableObject
{
    [SerializeField] private List<StoryUnit> _units = new List<StoryUnit>();
    [SerializeField] private bool _variable;

    public int Length => _units.Count;
    public bool Variable => _variable;

    private void OnValidate()
    {
        if(_variable && (_units.Count > 1 || !(_units[0] is VariantUnit)))
        {
            Debug.LogError("Must be one variable unit");
            _units.Clear();
        }
    }

    public List<StoryUnit> GetUnits()
    {
        return _units;
    }

    public VariantUnit GetVariantUnit()
    {
        VariantUnit variant;

        if (_units[0] is VariantUnit)
        {
            variant = (VariantUnit)_units[0];
        }
        else
        {
            variant = null;
        }

        return variant;
    }
}
