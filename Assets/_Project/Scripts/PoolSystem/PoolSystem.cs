using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolSystem<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;
    [SerializeField] protected bool collectionCheck = true;
    [SerializeField] protected int poolCapacity = 10;
    [SerializeField] protected int poolMaxSize = 20;
    protected ObjectPool<T> pool;

    protected virtual void Awake()
    {
        pool = new ObjectPool<T>(Create, OnGet, OnRelease, OnDestroyPoolObj, collectionCheck, poolCapacity, poolMaxSize);
    }

    protected abstract T Create();
    protected virtual void OnGet(T obj) => obj.gameObject.SetActive(true);
    protected virtual void OnRelease(T obj) => obj.gameObject.SetActive(false);
    protected virtual void OnDestroyPoolObj(T obj) => Destroy(obj.gameObject);

    public void Release(T obj) => pool.Release(obj);
}