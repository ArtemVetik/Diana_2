using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnitReader))]
[RequireComponent(typeof(UnitViewHandler))]
public class StoryTeller : MonoBehaviour
{
    [SerializeField] private List<StoryUnit> _story = new List<StoryUnit>();
    [SerializeField] private UnitReader _reader;
    [SerializeField] private UnitViewHandler _viewer;

    private int _unitIndex = -1;
    private StoryUnit _actualUnit;
    private bool _variantsShowed = false;
    private bool _clickAvailable = true;
    private EventSystem _currentEventSystem;

    public event UnityAction NewPartNeeded;
    public event UnityAction<int> NewVariantPartNeeded;

    private void OnEnable()
    {
        _reader.UnitPhrasesEnded += OnUnitPhrasesEnded;
        _viewer.StandartUnitCompleted += OnStandartUnitCompleted;
        _viewer.VariantUnitCompleted += OnVariantUnitCompleted;
        _viewer.UnitViewed += OnUnitViewed;
        _currentEventSystem = EventSystem.current;
    }

    private void OnDisable()
    {
        _reader.UnitPhrasesEnded -= OnUnitPhrasesEnded;
        _viewer.StandartUnitCompleted -= OnStandartUnitCompleted;
        _viewer.VariantUnitCompleted -= OnVariantUnitCompleted;
        _viewer.UnitViewed -= OnUnitViewed;
    }

    private void Start()
    {
        NewPartNeeded?.Invoke();
    }

    private void Update()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0) && !_variantsShowed && _clickAvailable && !_currentEventSystem.IsPointerOverGameObject())
        {
            _clickAvailable = false;
            ReadUnit();
        }
#else
        if (Input.GetMouseButtonDown(0) && !_variantsShowed && _clickAvailable && !_currentEventSystem.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            _clickAvailable = false;
            ReadUnit();
        }
#endif
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

        _clickAvailable = false;
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
            _variantsShowed = true;
        }
    }

    private void OnUnitViewed()
    {
        _clickAvailable = true;
    }

    private void OnUnitPhrasesEnded()
    {
        StartCoroutine(_viewer.ShowFinishActions());
    }

    private void OnStandartUnitCompleted()
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

    private void OnVariantUnitCompleted(int index)
    {
        NewVariantPartNeeded?.Invoke(index);
        _variantsShowed = false;
    }
}
