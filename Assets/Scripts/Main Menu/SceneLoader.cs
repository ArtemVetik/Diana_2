using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Tymski;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference _targetScene;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadSceneAsync);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadSceneAsync);
    }

    private void LoadSceneAsync()
    {
        Singleton<Fader>.Instance.FadeIn(() => SceneManager.LoadSceneAsync(_targetScene.ScenePath));
    }
}
