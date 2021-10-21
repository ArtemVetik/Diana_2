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

    public event UnityAction<VariantButton> VariantButtonSpawned;
    public event UnityAction UnitViewed;
    public event UnityAction UnitCompleted;

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

    public void OnUnitReaded(string phrase, StoryUnit unit, int index)  //вызывается после чтения юнита ридером
    {
        StartCoroutine(ShowUnit(phrase, unit, index));
    }

    private IEnumerator ShowUnit(string phrase, StoryUnit unit, int index)
    {
        if (index == 0)
        {
            _backgroundViewer.SetBackground(unit.Background); //смена фона при необходимости

            if (unit.FadeOut)
            {
                _fader.FadeOut();
                yield return _delay;
            }

            if (unit.ShowCharacterOnStart)  //проверка на выезд персонажа в начале юнита
            {
                _characterViewer.InitCharacter(unit.Character);
                _characterViewer.MoveCharacter(true);

                yield return _delay;
            }
        }
        else
        {
            yield return _delay;
        }
        _phraseViewer.ToggleBubble(true); //включение пузыря
        _phraseViewer.ToggleTextField(true); //включение текстового поля
        _phraseViewer.SetPhraseBubble(unit.Type);  //подбор цвета пузыря и вывод текста
        _phraseViewer.SetPhrase(phrase);

        if(unit.HasEmotions)
        {
            _emotionViewer.ShowEmotion(unit.GetEmotion(index));
        }

        UnitViewed?.Invoke();
    }

    public IEnumerator ShowFinishActions(StoryUnit unit)
    {
        _phraseViewer.ToggleBubble(false);

        if (unit.HideCharacterOnFinish)  //проверка на выезд персонажа в конце юнита
        {
            _characterViewer.MoveCharacter(false);
            yield return _delay;
        }

        if (unit.FadeIn)  //проверка на фейдинг в конце юнита
        {
            _fader.FadeIn();
            yield return _delay;
        }

        _emotionViewer.ResetEmotion();

        yield return _delay;
        UnitCompleted?.Invoke();
    }

    public void OnVariantPrepared(string phrase, int index, StoryUnit unit) //после нажатия игроком на кнопку выбора сюжета, вызывается для каждой фразы в юните.
    {
        _phraseViewer.ToggleBubble(true);
        _phraseViewer.ToggleTextField(false); //отключаем текстовое поле, если оно включено чтобы кнопки спавнились корректно
        _phraseViewer.SetPhraseBubble(unit.Type); //подбор пузыря
        _variantViewer.AddVariant(phrase, index, out VariantButton button); //спавним кнопку
        VariantButtonSpawned?.Invoke(button); 
    }

    public void ClearUnitView(List<VariantButton> buttons)
    {
        foreach (var button in buttons)
        {
            Destroy(button.gameObject);
        }
        
        buttons.Clear();
    }
}
