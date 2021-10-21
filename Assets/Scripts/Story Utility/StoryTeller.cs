using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UnitReader))]
[RequireComponent(typeof(UnitViewHandler))]
public class StoryTeller : MonoBehaviour
{
    [SerializeField] private List<StoryUnit> _story = new List<StoryUnit>();
    [SerializeField] private UnitReader _reader;
    [SerializeField] private UnitViewHandler _viewer;

    private int _unitIndex = -1;
    private StoryUnit _actualUnit;
    private bool _variantsPrepared = false;
    private List<VariantButton> _buttonsPool = new List<VariantButton>();
    private bool _clickAvailable = true;

    public event UnityAction NewPartNeeded;
    public event UnityAction<int> NewVariantPartNeeded;

    private void OnEnable()
    {
        _reader.UnitPhrasesEnded += OnUnitPhrasesEnded;
        _viewer.UnitCompleted += OnUnitCompleted;
        _viewer.VariantButtonSpawned += OnVariantButtonSpawned;
        _viewer.UnitViewed += OnUnitViewed;
    }

    private void OnDisable()
    {
        _reader.UnitPhrasesEnded -= OnUnitPhrasesEnded;
        _viewer.UnitCompleted -= OnUnitCompleted;
        _viewer.VariantButtonSpawned -= OnVariantButtonSpawned;
        _viewer.UnitViewed -= OnUnitViewed;
    }

    private void Start()
    {
        NewPartNeeded?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_variantsPrepared && _clickAvailable)
        {
            _clickAvailable = false;
            ReadUnit();
        }
    }

    private void InitializeActualUnit()
    {
        _actualUnit = _story[++_unitIndex];
    }

    public void InitializeStory(StoryPart part)
    {
        _story = part.GetUnits();

        _unitIndex = -1;

        InitializeActualUnit();
        ReadUnit();

        _clickAvailable = true;
    }

    private void ReadUnit()
    {
        if (!(_actualUnit is VariantUnit))
        {
            StartCoroutine(_reader.ReadUnit(_actualUnit));
        }
        else
        {
            _reader.PrepareVariants(_actualUnit);
            _variantsPrepared = true;
        }
    }

    private void OnUnitViewed()
    {
        _clickAvailable = true;
    }

    private void OnUnitPhrasesEnded()
    {
        StartCoroutine(_viewer.ShowFinishActions(_actualUnit));
    }

    private void OnUnitCompleted()
    {
        if (!_actualUnit.LastUnitInPart)
        {
            InitializeActualUnit();

            if (_actualUnit != null)
            {
                StartCoroutine(_reader.ReadUnit(_actualUnit));
            }
        }
        else
        {
            NewPartNeeded?.Invoke();
        }
    }

    public void OnVariantButtonSpawned(VariantButton button)
    {
        button.VariantChosen += OnVariantChosen;
        _buttonsPool.Add(button);
    }

    public void OnVariantChosen(int index)  //добавить завершающие действия для вариативных юнитов
    {
        _variantsPrepared = false;

        foreach (var button in _buttonsPool)
        {
            button.VariantChosen -= OnVariantChosen;
        }

        _viewer.ClearUnitView(_buttonsPool);

        NewVariantPartNeeded?.Invoke(index);
    }
}
