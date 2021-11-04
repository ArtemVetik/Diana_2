#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Spine.Unity;
using Diana2.Castomization;

public class SkinDataBaseInitializer : EditorWindow
{
    private SkinDataBase _dataBase;
    private SkeletonDataAsset _skeletonAsset;

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
        _skeletonAsset = EditorGUILayout.ObjectField("SkeletonDataAsset", _skeletonAsset, typeof(SkeletonDataAsset)) as SkeletonDataAsset;

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
        if (_skeletonAsset == null)
        {
            Debug.LogError("SkeletonAnimation is null");
            return;
        }

        var skeletonData = _skeletonAsset.GetSkeletonData(true);

        _dataBase.Clear();
        foreach (var skin in skeletonData.Skins)
            _dataBase.Add(new SkinData(skin.Name, null));

        EditorUtility.SetDirty(_dataBase);

        Debug.Log("Database update: successful");
        Debug.Log($"Added {skeletonData.Skins.Count} skins");
    }
}
#endif