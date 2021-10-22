using UnityEngine;
using UnityEngine.Events;

public class VariantViewer : MonoBehaviour
{
    [SerializeField] private Transform _variantButtonSpawnParent;
    [SerializeField] private VariantButton _variantButtonTemplate;

    public void AddVariant(string phrase, int index, out VariantButton button)
    {
        button = Instantiate(_variantButtonTemplate, _variantButtonSpawnParent);
        button.Init(phrase, index);
    }
}
