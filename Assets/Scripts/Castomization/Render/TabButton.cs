using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Diana2.Castomization
{
    [RequireComponent(typeof(Button))]
    public class TabButton : MonoBehaviour
    {
        [SerializeField] private Text _buttonText;

        private Button _selfButton;

        public event UnityAction<TabButton> Clicked;

        private void Awake()
        {
            _selfButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _selfButton.onClick.AddListener(OnSelfButtonClick);
        }

        private void OnDisable()
        {
            _selfButton.onClick.AddListener(OnSelfButtonClick);
        }

        public void Init(string key)
        {
            _buttonText.text = key;
        }

        private void OnSelfButtonClick()
        {
            Clicked?.Invoke(this);
        }
    }
}