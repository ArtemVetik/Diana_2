using UnityEngine;

public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    private static T _instanse;
    private static readonly object Lock = new object();
    private bool _persistent = true;

    public static T Instance
    {
        get
        {
            if(Quitting)
            {
                Debug.LogWarning($"[{nameof(Singleton)} <{typeof(T)}>] Instance will not be returned because app is quitting");
                return null;
            }
            lock(Lock)
            {
                if(_instanse != null)
                    return _instanse;
                var instances = FindObjectsOfType<T>();
                var count = instances.Length;
                if(count > 0)
                {
                    if (count == 1)
                        return _instanse = instances[0];
                    Debug.LogWarning($"[{nameof(Singleton)} <{typeof(T)}>] There should never be more than one instance");
                    for(int i = 0; i < instances.Length; i++)
                        Destroy(instances[i]);
                    return _instanse = instances[0];
                }
                Debug.LogWarning($"[{nameof(Singleton)} <{typeof(T)}>] An instance is needed in the scene and no exists");
                return _instanse = new GameObject($"{nameof(Singleton)}){typeof(T)}").AddComponent<T>();
            }
        }
    }

    private void Awake()
    {
        if(_persistent)
            DontDestroyOnLoad(gameObject);
        OnAwake();
    }

    protected virtual void OnAwake() { }
}

public abstract class Singleton : MonoBehaviour
{
    public static bool Quitting { get; private set; }

    private void OnApplicationQuit()
    {
        Quitting = true;
    }
}
