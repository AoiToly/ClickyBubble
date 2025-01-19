using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = FindFirstObjectByType<T>();

            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<T>();
            }

            return _instance;
        }
    }
}
