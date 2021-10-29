using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GraphicRaycaster))]
public class Fader : MonoBehaviour
{
    private Image _fader;
    private Canvas _canvas;
    private CanvasScaler _scaler;

    private void Awake()
    {
        _fader = GetComponent<Image>();
        _scaler = GetComponent<CanvasScaler>();
        _canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        InitFader();
        FadeOut();
    }

    public void FadeIn(Action OnFaded = null)
    {
        _fader.DOColor(new Color(_fader.color.r, _fader.color.g, _fader.color.b, 1), ConstantKeys.GlobalKeys.FadingDuration).OnComplete(() => { if (OnFaded != null) OnFaded(); });
    }

    public void FadeOut(Action OnFadedOut = null)
    {
        _fader.DOColor(new Color(_fader.color.r, _fader.color.g, _fader.color.b, 0), ConstantKeys.GlobalKeys.FadingDuration).OnComplete(() => { if (OnFadedOut != null) OnFadedOut(); });
    }

    public void Reset()
    {
        _fader.color = new Color(_fader.color.r, _fader.color.g, _fader.color.b, 1);
    }

    private void InitFader()
    {
        _fader.raycastTarget = false;
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvas.sortingOrder = 2000;
        _scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _scaler.referenceResolution = new Vector2(1920, 1080);
        _scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        _scaler.scaleFactor = 0.5f;
        _fader.color = Color.black;
    }
}
