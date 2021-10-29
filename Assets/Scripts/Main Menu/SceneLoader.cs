using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    //[SerializeField] private Object _targetScene;
    [SerializeField] private int _targetSceneIndex;
    [SerializeField] private Fader _fader;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => LoadScene(_targetSceneIndex));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => LoadScene(_targetSceneIndex));
    }

    private void LoadScene(int index)
    {
        _fader.FadeIn(() => SceneManager.LoadScene(_targetSceneIndex));
    }

    /*
    private void LoadScene()
    {
        _fader.FadeIn(() => SceneManager.LoadScene(_targetScene.name));

        if (_targetScene != null)
        {
            
        }
        
    }*/
}
