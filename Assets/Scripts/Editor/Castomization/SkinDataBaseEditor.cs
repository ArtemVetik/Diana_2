#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Diana2.Castomization;

[CustomEditor(typeof(SkinDataBase))]
public class SkinDataBaseEditor : Editor
{
    private enum RenderType
    {
        Lobby, Pages
    }

    private SkinDataBase _dataBase;
    private Dictionary<string, List<SkinData>> _data;
    private RenderType _renderType;
    private int _renderPage;

    private void Awake()
    {
        _dataBase = target as SkinDataBase;
        InitializeData();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (_renderType == RenderType.Lobby)
        {
            RenderMainPage();
        }
        else if (_renderType == RenderType.Pages)
        {
            RenderHeader(_renderPage);
            RenderPageData(_renderPage);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void RenderMainPage()
    {
        var headerStyle = new GUIStyle();
        headerStyle.fontSize = 22;
        headerStyle.normal.textColor = Color.white;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(serializedObject.targetObject.name, headerStyle);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(30);

        GUILayout.BeginVertical("box");
        var saveKeyStyle = new GUIStyle();
        saveKeyStyle.normal.textColor = Color.yellow;
        EditorGUILayout.LabelField("This is the key to save data to PlayerPrefs", saveKeyStyle);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_saveKey"));
        GUILayout.EndVertical();

        GUILayout.Space(30);

        GUILayout.Label($"Total skin count: {_dataBase.Data.Count()}");
        GUILayout.Label($"Permanent skin count: {_dataBase.Data.Count(data => data.IsPremanent)}");
        GUILayout.Space(10);

        GUILayout.BeginVertical();
        foreach (var dictionaryData in _data)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label($"{dictionaryData.Key} count:\t\t{dictionaryData.Value.Count}");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(30);

        if (GUILayout.Button("Show Data"))
            _renderType = RenderType.Pages;
    }

    private void RenderHeader(int page)
    {
        var style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        var key = _data.Keys.ToArray()[page].ToString();
        var count = _data[key].Count;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Go Home"))
            _renderType = RenderType.Lobby;
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label($"{key} ({count})", style);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label($"{page + 1}/{_data.Keys.Count}");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("<"))
            _renderPage--;
        else if (GUILayout.Button(">"))
            _renderPage++;

        _renderPage = Mathf.Clamp(_renderPage, 0, _data.Keys.Count - 1);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void RenderPageData(int page)
    {
        var renderDataList = _data.Values.ToArray()[page];
        var dataBaseList = serializedObject.FindProperty("_skins");

        foreach (var renderData in renderDataList)
        {
            GUILayout.BeginVertical("Box");
            var index = _dataBase.IndexOf(renderData);
            var arrayData = dataBaseList.GetArrayElementAtIndex(index);

            GUI.enabled = false;
            EditorGUILayout.PropertyField(arrayData.FindPropertyRelative("_guid"));
            GUI.enabled = false;
            EditorGUILayout.PropertyField(arrayData.FindPropertyRelative("_name"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(arrayData.FindPropertyRelative("_isPremanent"));

            var previewProperty = arrayData.FindPropertyRelative("_preview");
            var preview = (Sprite)EditorGUILayout.ObjectField("Preview image", previewProperty.objectReferenceValue, typeof(Sprite), true);
            previewProperty.objectReferenceValue = preview;
            GUILayout.EndHorizontal();
        }
    }

    private void InitializeData()
    {
        _data = new Dictionary<string, List<SkinData>>();

        foreach (var skinData in _dataBase.Data)
        {
            var key = skinData.GetSkinKey();
            if (_data.ContainsKey(key) == false)
                _data.Add(key, new List<SkinData>());

            _data[key].Add(skinData);
        }
    }
}
#endif