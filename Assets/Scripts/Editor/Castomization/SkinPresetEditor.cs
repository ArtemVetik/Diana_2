using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Diana2.Castomization;
using UnityEngine;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(SkinPreset))]
public class SkinPresetEditor : Editor
{
    private const int PreviewTextureWidth = 100;
    private const int PreviewTextureHeight = 100;

    private SkinDataBase _dataBase;
    private SkinPreset _preset;
    private string[] _popupData;

    private void Awake()
    {
        _preset = target as SkinPreset;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_dataBase"));
        _dataBase = serializedObject.FindProperty("_dataBase").objectReferenceValue as SkinDataBase;
        serializedObject.ApplyModifiedProperties();

        if (_dataBase == null)
        {
            EditorGUILayout.HelpBox("DataBase field cannot be empty", MessageType.Error);
            return;
        }
        if (_dataBase.Data.Count() == 0)
        {
            EditorGUILayout.HelpBox("DataBase data cannot be empty", MessageType.Error);
            return;
        }

        _popupData = _dataBase.Data.Select(data => data.Name).ToArray();

        GUILayout.Space(10);
        RenderControlButtons();
        GUILayout.Space(10);

        var dataBaseData = _dataBase.Data.ToArray();
        var presetSkins = _preset.Skins.ToArray();

        for (int i = 0; i < presetSkins.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            var selectedIndex = _dataBase.TryGetSkinByName(presetSkins[i], out SkinData skinData) ? _dataBase.IndexOf(skinData) : 0;
            var newIndex = EditorGUILayout.Popup(selectedIndex, _popupData);
            var newData = dataBaseData[newIndex];
            _preset.Replace(i, newData.Name);

            if (GUILayout.Button("Remove"))
            {
                _preset.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            var previewTexture = newData.Preview ? newData.Preview.texture : Texture2D.blackTexture;
            var rect = EditorGUILayout.GetControlRect();
            EditorGUI.DrawPreviewTexture(new Rect(rect.x, rect.y, PreviewTextureWidth, PreviewTextureHeight), previewTexture);
            EditorGUILayout.Space(PreviewTextureHeight - rect.height);
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(_preset);
    }

    private void RenderControlButtons()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
                
        if (GUILayout.Button("Add Skin", GUILayout.Width(60), GUILayout.Height(30)))
            _preset.Add(_dataBase.Data.ToArray()[0].Name);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
