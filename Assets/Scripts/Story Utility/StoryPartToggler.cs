using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StoryTeller))]
public class StoryPartToggler : MonoBehaviour
{
    [SerializeField] List<StoryPart> _storyParts = new List<StoryPart>();
    [SerializeField] private StoryTeller _teller;

#if UNITY_EDITOR
    [SerializeField] private Part _partToLoad;
    [SerializeField] private Fader _fader;
    [SerializeField] private bool _defaultStoryNumeration = true;

    private enum Part
    {
        Part_1,
        Part_2,
        Part_3,
        Part_4,
        Part_5,
        Part_6,
        Part_7,
        Part_8,
        Part_9,
        Part_10,
        Part_11,
        Part_12,
        Part_13,
        Part_14,
        Part_15,
        Part_16,
        Part_17,
        Part_18,
        Part_19,
        Part_20
    }
#endif

    private List<StoryPart> _variablePartVariants = new List<StoryPart>();
    private int _index = -1;
    

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

#if UNITY_EDITOR
    private void OnNewPartNeeded()
    {
        StoryPart nextPart;

        if (!_defaultStoryNumeration)
        {
            _index = (int)_partToLoad;
            nextPart = _storyParts[_index];
            _defaultStoryNumeration = true;
            _fader.FadeOut();
        }
        else
        {
            nextPart = _storyParts[++_index];
        }

        if (nextPart.Variable)
        {
            var variant = nextPart.GetVariantUnit();
            _variablePartVariants.Clear();

            for (int i = 0; i < variant.NextPartsLength; i++)
            {
                _variablePartVariants.Add(variant.GetNextPart(i));
            }
        }

        _teller.InitializeStory(nextPart);
    }
#else
    private void OnNewPartNeeded()
    {
        var nextPart = _storyParts[++index];

        if(nextPart.Variable)
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
#endif

    private void OnNewVariantPartNeeded(int variantIndex)
    {
        _index++;

        Debug.LogError("On New Varian Part Needed " + variantIndex);

        var chosenPart = _variablePartVariants[variantIndex];
        _teller.InitializeStory(chosenPart);

        ClearAlternateVariants(chosenPart);
    }

    private void ClearAlternateVariants(StoryPart chosenPart)
    {
        foreach (var part in _variablePartVariants)
        {
            if (part != chosenPart)
            {
                for (int i = 0; i < _storyParts.Count; i++)
                {
                    if (_storyParts[i] == part)
                    {
                        _storyParts.Remove(_storyParts[i]);
                    }
                }
            }
        }
    }

    //сделать проверку на конец листа
}
