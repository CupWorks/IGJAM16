using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    private bool initialized = false;

    public static T Instance
    {
        get
        {
            if(!instance)
            {
                instance = new GameObject().AddComponent<T>();
            }
            return instance;
        }

        private set
        {

        }
    }

    void Start()
    {
        initialized = false;
    }

    void Update()
    {

    }

    void Awake()
    {
        if(!instance)
        {
            instance = this as T;
            Init();
            initialized = true;
        }
        else if(instance == this)
        {
            if(!initialized)
            {
                Init();
                initialized = true;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        Awake();
    }

    protected virtual void Init()
    {
    }
}
