using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    [SerializeField] private Image _fader;

    public void FadeIn(Action OnFaded = null)
    {
        if (_fader != null)
        {
            _fader.DOColor(new Color(_fader.color.r, _fader.color.g, _fader.color.b, 1), ConstantKeys.GlobalKeys.Delay).OnComplete(() => { if (OnFaded != null) OnFaded(); });
        }
    }

    public void FadeOut(Action OnFadedOut = null)
    {
        if (_fader != null)
        {
            _fader.DOColor(new Color(_fader.color.r, _fader.color.g, _fader.color.b, 0), ConstantKeys.GlobalKeys.Delay).OnComplete(() => { if (OnFadedOut != null) OnFadedOut(); });
        }
    }

    public void Reset()
    {
        _fader.color = new Color(_fader.color.r, _fader.color.g, _fader.color.b, 1);
    }
}
