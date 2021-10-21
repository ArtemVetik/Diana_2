using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UnitReader : MonoBehaviour   //осуществляет первичную проверку юнита на наличие фразы/конец юнита. в случае успеха, передает визуализатору данные через событие
{
    [SerializeField] private StoryUnitIndexer _indexer;

    private string _phrase;
    
    public event UnityAction<string, StoryUnit, int> UnitReaded;
    public event UnityAction<string, int, StoryUnit> VariantPrepared;
    public event UnityAction UnitPhrasesEnded;

    public IEnumerator ReadUnit(StoryUnit unit)
    {
        _indexer.Init(unit);

        yield return null;

        if(TryReadPhrase(unit, out int index))
        {
            UnitReaded?.Invoke(_phrase, unit, index);
        }
        else
        {
            UnitPhrasesEnded?.Invoke();
        }
    }

    private bool TryReadPhrase(StoryUnit unit, out int index)
    {
        index = _indexer.GetNextIndex();
        
        if (index >= 0)
        {
            _phrase = unit.GetPhrase(index);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool TryGetVariant(StoryUnit unit)
    {
        _indexer.Init(unit);
        var index = _indexer.GetNextIndex();

        if (index >= 0)
        {
            _phrase = unit.GetPhrase(index);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PrepareVariants(StoryUnit unit)
    {
        for (int i = 0; i < unit.Lenght; i++)
        {
            if (TryGetVariant(unit))
            {
                VariantPrepared?.Invoke(_phrase, i, unit);
            }
        }
    }
}
