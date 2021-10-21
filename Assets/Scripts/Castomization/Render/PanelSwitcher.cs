using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization {
    public class PanelSwitcher : MonoBehaviour
    {
        [SerializeField] private TabButton _template;
        [SerializeField] private Transform _container;

        private Dictionary<TabButton, SkinPanel> _data;
        private SkinPanel _currentPanel;

        public void AddTabButton(string key, SkinPanel panel)
        {
            if (_data == null)
                _data = new Dictionary<TabButton, SkinPanel>();

            var tabButton = Instantiate(_template, _container);
            tabButton.Init(key);
            tabButton.Clicked += OnTabButtonClicked;

            _data.Add(tabButton, panel);
        }

        public void SetCurrentPanel(SkinPanel currentPanel)
        {
            foreach (var panel in _data.Values)
                panel.Hide();

            _currentPanel = currentPanel;
            _currentPanel.Show();
        }

        private void OnTabButtonClicked(TabButton tabButton)
        {
            _currentPanel.Hide();
            _currentPanel = _data[tabButton];
            _currentPanel.Show();
        }

        private void OnDisable()
        {
            foreach (var button in _data.Keys)
                button.Clicked -= OnTabButtonClicked;
        }
    }
}