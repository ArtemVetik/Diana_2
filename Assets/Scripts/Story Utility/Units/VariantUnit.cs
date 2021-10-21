using UnityEngine;

[CreateAssetMenu(fileName = "New unit", menuName = "Story/Create new variant unit")]
public class VariantUnit : StoryUnit
{
    [SerializeField] private StoryPart[] _nextParts;

    public int NextPartsLength => _nextParts.Length;

    public StoryPart GetNextPart(int index)
    {
        return _nextParts[index];
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        if(_nextParts.Length == 0)
        {
            Debug.LogError($"Add at least 2 next parts to {name}");
        }
    }
}
