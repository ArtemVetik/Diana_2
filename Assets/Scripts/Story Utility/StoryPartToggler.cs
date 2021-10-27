using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(StoryTeller))]
public class StoryPartToggler : MonoBehaviour
{
    [SerializeField] List<StoryPart> _storyParts = new List<StoryPart>();
    [SerializeField] private StoryTeller _teller;


    [SerializeField] private Part _partToLoad;
    [SerializeField] private Fader _fader;
    [SerializeField] private bool _defaultStoryNumeration = true;
    [SerializeField] private Dropdown _dropDown;
    [SerializeField] private GameObject _loadCanvas;
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
        Part_20,
        Part_21,
        Part_22,
        Part_23,
        Part_24,
        Part_25,
        Part_26,
        Part_27,
        Part_28,
    }


    private List<StoryPart> _variablePartVariants = new List<StoryPart>();
    private int _index = -1;
    

    private void OnEnable()
    {
        CreateDropDownMenu();
        _teller.NewPartNeeded += OnNewPartNeeded;
        _teller.NewVariantPartNeeded += OnNewVariantPartNeeded;
    }

    private void OnDisable()
    {
        _teller.NewPartNeeded -= OnNewPartNeeded;
        _teller.NewVariantPartNeeded -= OnNewVariantPartNeeded;
    }

    private void CreateDropDownMenu()
    {
        List<string> dropOptions = new List<string>();

        var parts = Enum.GetValues(typeof(Part));

        foreach (var part in parts)
        {
            dropOptions.Add(part.ToString());
        }

        _dropDown.AddOptions(dropOptions);
    }

    public void LoadPart(int index)
    {
        StoryPart nextPart = _storyParts[index];

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

        _loadCanvas.SetActive(false);
    }

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
            ++_index;
            if (EndStoryVerification(_index))
            {
                return;
            }

            nextPart = _storyParts[_index];
        }

        if (nextPart.Variable)
        {
            var variant = nextPart.GetVariantUnit();
            _variablePartVariants.Clear();

            if(variant.NextPartsLength > 1)
            {
                for (int i = 0; i < variant.NextPartsLength; i++)
                {
                    _variablePartVariants.Add(variant.GetNextPart(i));
                }
            }
            else
            {
                for (int i = 0; i < variant.Lenght; i++)
                {
                    _variablePartVariants.Add(variant.GetNextPart(0));
                }
            }
            
        }

        _teller.InitializeStory(nextPart);
    }

    private void OnNewVariantPartNeeded(int variantIndex)
    {
        _index++;

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

    private bool EndStoryVerification(int index)
    {
        if(index == _storyParts.Count)
        {
            _loadCanvas.SetActive(true);
            return true;
        }

        return false;
    }

}
