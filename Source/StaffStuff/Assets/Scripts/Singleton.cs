using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    private bool initialized = false;

	protected Singleton()
	{	
	}

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
    }

    private void Start()
    {
        initialized = false;
    }

    private void Awake()
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

    private void OnLevelWasLoaded(int level)
    {
        Awake();
    }

    protected virtual void Init()
    {
    }
}
