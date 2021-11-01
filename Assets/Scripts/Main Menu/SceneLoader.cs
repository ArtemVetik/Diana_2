using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Tymski;
using System.Collections;

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
        _button.onClick.AddListener(LoadAfterFadeIn);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadAfterFadeIn);
    }

    private void LoadAfterFadeIn()
    {
        Singleton<Fader>.Instance.FadeIn(Load);
    }

    private void Load()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        var sceneLoading = SceneManager.LoadSceneAsync(_targetScene.ScenePath);

        sceneLoading.allowSceneActivation = false;

        while(sceneLoading.progress < 0.9f)
        {
            yield return null;
        }

        sceneLoading.allowSceneActivation = true;
    }
}
