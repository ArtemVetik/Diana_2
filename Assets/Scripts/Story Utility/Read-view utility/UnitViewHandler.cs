using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhraseViewer))]
[RequireComponent(typeof(VariantViewer))]
[RequireComponent(typeof(EmotionViewer))]
public class UnitViewHandler : MonoBehaviour
{
    [SerializeField] private PhraseViewer _phraseViewer;
    [SerializeField] private VariantViewer _variantViewer;
    [SerializeField] private CharacterViewer _characterViewer;
    [SerializeField] private BackgroundViewer _backgroundViewer;
    [SerializeField] private EmotionViewer _emotionViewer;
    [SerializeField] private UnitReader _reader;
    [SerializeField] private Fader _fader;

    [SerializeField] private Transform _variantButtonSpawnParent;
    [SerializeField] private VariantButton _variantButtonTemplate;

    private WaitForSeconds _delay = new WaitForSeconds(ConstantKeys.GlobalKeys.Delay);
    private WaitForSeconds _backgroundDelay = new WaitForSeconds(ConstantKeys.GlobalKeys.BackgroundMoveDuration);
    private StoryUnit _currentUnit;
    private int _chosenVariant;
    private List<VariantButton> _buttonsPool = new List<VariantButton>();

    public event UnityAction UnitViewed;
    public event UnityAction StandartUnitCompleted;
    public event UnityAction<int> VariantUnitCompleted;

    private void OnEnable()
    {
        _reader.UnitReaded += OnUnitReaded;
        _reader.VariantPrepared += OnVariantPrepared;
    }

    private void OnDisable()
    {
        _reader.UnitReaded -= OnUnitReaded;
        _reader.VariantPrepared -= OnVariantPrepared;
    }

    public void OnUnitReaded(string phrase, StoryUnit unit, int index)
    {
        _currentUnit = unit;
        StartCoroutine(ShowUnit(phrase, index));
    }

    private IEnumerator ShowUnit(string phrase, int index)
    {
        if (index == 0)
        {
            StartCoroutine(ShowStartActions(phrase, index));
        }
        else
        {
            Debug.LogError("pre-message delay");
            yield return _delay;
            
            _phraseViewer.ViewStandartUnit(_currentUnit.Type, phrase);

            UnitViewed?.Invoke();
        }
    }

    private IEnumerator ShowStartActions(string phrase, int emotionIndex)
    {
        _backgroundViewer.SetBackground(_currentUnit.Background);

        if (_currentUnit.FadeOut)
        {
            _fader.FadeOut();

            if (_currentUnit.BackgroundMoveData != null)
            {
                _backgroundViewer.MoveBackground(_currentUnit.BackgroundMoveData);
                yield return _backgroundDelay;
            }
            else
            {

                yield return _delay;
            }
        }
        
        if (_currentUnit.ShowCharacterOnStart)
        {
            _characterViewer.InitCharacter(_currentUnit.Character);
            _characterViewer.MoveCharacter(true);
            yield return _delay;
        }

        if (_currentUnit.HasEmotions)
        {
            _emotionViewer.ShowEmotion(_currentUnit.GetEmotion(emotionIndex));
        }

        _phraseViewer.ViewStandartUnit(_currentUnit.Type, phrase);

        UnitViewed?.Invoke();
    }

    public IEnumerator ShowFinishActions()
    {
        _phraseViewer.ToggleBubble(false);

        if (_currentUnit.HideCharacterOnFinish)
        {
            _characterViewer.MoveCharacter(false);
            yield return _delay;
        }

        if (_currentUnit.FadeIn)
        {
            _fader.FadeIn();
            yield return _delay;
        }

        _emotionViewer.ResetEmotion();

        yield return _delay;

        if(!(_currentUnit is VariantUnit))
        {
            StandartUnitCompleted?.Invoke();
        }
        else
        {
            VariantUnitCompleted?.Invoke(_chosenVariant);
        }
    }

    public void OnVariantPrepared(string phrase, int index, StoryUnit unit)
    {
        _currentUnit = unit;

        _backgroundViewer.SetBackground(_currentUnit.Background);

        if (_currentUnit.FadeOut)
        {
            _fader.FadeOut();
            Debug.LogError("variant Fader delay");
        }

        _phraseViewer.ViewVariableUnit();

        _variantViewer.AddVariant(phrase, index, out VariantButton button);

        button.VariantChosen += OnVariantChosen;
        _buttonsPool.Add(button);
    }

    private void OnVariantChosen(int index)
    {
        _chosenVariant = index;
        ClearButtons();
        StartCoroutine(ShowFinishActions());
    }

    private void ClearButtons()
    {
        foreach (var button in _buttonsPool)
        {
            button.VariantChosen -= OnVariantChosen;
            Destroy(button.gameObject);
        }
        
        _buttonsPool.Clear();
    }
}
