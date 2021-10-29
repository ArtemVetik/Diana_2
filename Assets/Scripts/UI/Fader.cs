using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class Fader : MonoBehaviour
{
    private Image _fader;

    private void Awake()
    {
        _fader = GetComponent<Image>();
    }

    private void Start()
    {
        FadeOut();
    }

    public void FadeIn(Action OnFaded = null)
    {
        _fader.DOColor(new Color(_fader.color.r, _fader.color.g, _fader.color.b, 1), ConstantKeys.GlobalKeys.Delay).OnComplete(() => { if (OnFaded != null) OnFaded(); });
    }

    public void FadeOut(Action OnFadedOut = null)
    {
        _fader.DOColor(new Color(_fader.color.r, _fader.color.g, _fader.color.b, 0), ConstantKeys.GlobalKeys.Delay).OnComplete(() => { if (OnFadedOut != null) OnFadedOut(); });
    }

    public void Reset()
    {
        _fader.color = new Color(_fader.color.r, _fader.color.g, _fader.color.b, 1);
    }
}
