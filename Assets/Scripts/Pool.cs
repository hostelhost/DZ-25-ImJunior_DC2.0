using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> where T : MonoBehaviour, IAppearing
{
    private T _prefab;
    private int _minLifetime;
    private int _maxLifetime;
    private ObjectPool<T> _pool;
    private Action<Vector3> _onObjectDead;

    public T Get() 
    {
        return _pool.Get();
    }

    public void Release(T item) => 
        _pool.Release(item);

    public void CreatePool(T prefab, int minLifetime, int maxLifetime, Action<Vector3> onObjectDead = null)
    {
        _prefab = prefab;
        _minLifetime = minLifetime;
        _maxLifetime = maxLifetime;
        _onObjectDead = onObjectDead;
        InitializePool();
    }

    private void InitializePool()
    {
        _pool = new ObjectPool<T>(
            () => { return UnityEngine.Object.Instantiate(_prefab); },
            OnGetObject,
            OnReleaseObject,
            T => { UnityEngine.Object.Destroy(T.gameObject); }
        );
    }

    private void OnGetObject(T item)
    {
        item.gameObject.SetActive(true);
        item.Initialize(UnityEngine.Random.Range(_minLifetime, _maxLifetime), 
            pos => 
            { 
                _pool.Release(item);
                _onObjectDead?.Invoke(pos); 
            }                                                                                  //ÍÈÕÓß ÍÅ ÏÎÍßÒÍÎ!!!
        );
    }

    private void OnReleaseObject(T item) =>
        item.gameObject.SetActive(false);
}
