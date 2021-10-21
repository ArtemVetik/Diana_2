using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Diana2.Castomization {
    [RequireComponent(typeof(Button))]
    public class SkinPresenter : MonoBehaviour
    {
        [SerializeField] private Text _buttonText;

        private Button _selfButton;

        public SkinData Data { get; private set; }

        public event UnityAction<SkinPresenter> Clicked;

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
            _selfButton.onClick.RemoveListener(OnSelfButtonClick);
        }

        public void Render(SkinData data)
        {
            Data = data;

            _buttonText.text = data.Name;
        }

        private void OnSelfButtonClick()
        {
            Clicked?.Invoke(this);
        }
    }
}