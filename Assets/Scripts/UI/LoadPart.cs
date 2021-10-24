using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPart : MonoBehaviour
{
    [SerializeField] private Dropdown _dropDown;
    [SerializeField] private Button _loadButton;
    [SerializeField] private StoryPartToggler _storyPartToggler;

    private int index = 0;

    private void OnEnable()
    {
        _loadButton.onClick.AddListener(DropDownToIndex);
        _loadButton.onClick.AddListener(() => _storyPartToggler.LoadPart(index));
    }

    private void DropDownToIndex()
    {
        Debug.LogError(_dropDown.value);

        index = _dropDown.value;
    }
}
