#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Spine.Unity;
using Diana2.Castomization;

public class SkinDataBaseInitializer : EditorWindow
{
    private SkinDataBase _dataBase;
    private SkeletonAnimation _skeletonAnimation;

    [MenuItem("Castomization/SkinDataBaseInitializer")]
    public static void OpenWindow()
    {
        var window = GetWindow<SkinDataBaseInitializer>("SkinDataBaseInitializer window");
        window.minSize = new Vector2(500, 200);
        window.maxSize = new Vector2(500, 200);
    }

    [System.Obsolete]
    private void OnGUI()
    {
        EditorGUILayout.HelpBox("Updates the selected skin database for the selected model", MessageType.Info);

        _dataBase = EditorGUILayout.ObjectField("SkinDataBase", _dataBase, typeof(SkinDataBase)) as SkinDataBase;
        _skeletonAnimation = EditorGUILayout.ObjectField("SkeletonAnimation", _skeletonAnimation, typeof(SkeletonAnimation)) as SkeletonAnimation;

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Update"))
            UpdateDataBase();
    }

    private void UpdateDataBase()
    {
        if (_dataBase == null)
        {
            Debug.LogError("DataBase is null");
            return;
        }
        if (_skeletonAnimation == null)
        {
            Debug.LogError("SkeletonAnimation is null");
            return;
        }

        _dataBase.Clear();
        foreach (var skin in _skeletonAnimation.skeleton.Data.Skins)
        {
            _dataBase.Add(new SkinData(skin.Name, null));
        }

        EditorUtility.SetDirty(_dataBase);
    }
}
#endif