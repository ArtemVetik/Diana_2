using UnityEngine;

public class StoryUnitIndexer : MonoBehaviour
{
    private readonly int _endIndex = -1;
    private int _index;
    private StoryUnit _actualUnit;

    public void Init(StoryUnit unit)
    {
        if (unit != _actualUnit)
        {
            _actualUnit = unit;
            _index = _endIndex;
        }
    }

    public int GetNextIndex()
    {
        if (++_index < _actualUnit.Lenght)
        {
            return _index;
        }
        else
        {
            _index = _endIndex;
            return _index;
        }
    }
}
