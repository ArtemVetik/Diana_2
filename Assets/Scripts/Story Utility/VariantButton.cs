using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VariantButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _phrase;

    public event UnityAction<int> VariantChosen;

    public void Init(string text, int nextPartIndex)
    {
        _phrase.text = text;
        _button.onClick.AddListener(() => SetNextUnit(nextPartIndex));
    }

    private void SetNextUnit(int index)
    {
        transform.parent.gameObject.SetActive(false);
        VariantChosen?.Invoke(index);
    }
}
