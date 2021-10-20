using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StoryTeller))]
public class StoryPartToggler : MonoBehaviour
{
    [SerializeField] List<StoryPart> _storyParts = new List<StoryPart>();
    [SerializeField] private StoryTeller _teller;

    [SerializeField] private List<StoryPart> _variablePartVariants = new List<StoryPart>();

    private int index = -1;

    private void OnEnable()
    {
        _teller.NewPartNeeded += OnNewPartNeeded;
        _teller.NewVariantPartNeeded += OnNewVariantPartNeeded;
    }

    private void OnDisable()
    {
        _teller.NewPartNeeded -= OnNewPartNeeded;
        _teller.NewVariantPartNeeded -= OnNewVariantPartNeeded;
    }

    private void OnNewPartNeeded()
    {
        var nextPart = _storyParts[++index];

        if(nextPart.Variable)  //тут происходит подготовка NextParts для ветвления повествования
        {
            var variant= nextPart.GetVariantUnit();
            _variablePartVariants.Clear();

            for (int i = 0; i < variant.NextPartsLength; i++)
            {
                _variablePartVariants.Add(variant.GetNextPart(i));
            }
        }

        _teller.InitializeStory(nextPart);
    }

    private void OnNewVariantPartNeeded(int variantIndex)   //сюда приходит индекс выбранного юнита из текущей VariantPart
    {
        index++;

        var chosenPart = _variablePartVariants[variantIndex];
        _teller.InitializeStory(chosenPart);

        ClearAlternateVariants(chosenPart);
    }

    private void ClearAlternateVariants(StoryPart chosenPart) 
    {
        foreach (var part in _variablePartVariants)
        {
            if(part != chosenPart)
            {
                for(int i = 0; i < _storyParts.Count; i++)
                {
                    if(_storyParts[i] == part)
                    {
                        _storyParts.Remove(_storyParts[i]);
                    }
                }
            }
        }
    }

    //сделать проверку на конец листа
}
