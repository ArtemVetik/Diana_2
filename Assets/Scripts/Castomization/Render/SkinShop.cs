using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization {
    public class SkinShop : MonoBehaviour
    {
        [SerializeField] private List<string> _skinKeys;
        [SerializeField] private SkinChanger _skinChanger;
        [SerializeField] private PanelSwitcher _panelSwitcher;
        [SerializeField] private SkinPanel _skinPanel;
        [SerializeField] private Transform _panelContainer;

        private List<SkinPanel> _instPanels;

        private void OnEnable()
        {
            _instPanels = new List<SkinPanel>();

            foreach (var skinKey in _skinKeys)
            {
                var panel = Instantiate(_skinPanel, _panelContainer);
                panel.Render(skinKey);
                _panelSwitcher.AddTabButton(skinKey, panel);

                foreach (var presenter in panel.Data)
                    presenter.Clicked += OnSkinPresenterClicked;

                _instPanels.Add(panel);
            }

            _panelSwitcher.SetCurrentPanel(_instPanels[0]);
        }

        private void OnSkinPresenterClicked(SkinPresenter presenter)
        {
            _skinChanger.SelectSkin(presenter.Data);
        }
    }
}