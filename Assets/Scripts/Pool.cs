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

    public event Action<int> Created;
    public event Action Spawned;
    public event Action Deactivated;

    public int CountActive()
    {
        return _pool.CountActive;
    }

    public T Get()
    {
        Spawned?.Invoke();
        return _pool.Get();
    }

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
            () => { Created?.Invoke(_pool.CountAll); 
                return UnityEngine.Object.Instantiate(_prefab); 
            },
            OnGetObject,
            OnReleaseObject,
            item => { UnityEngine.Object.Destroy(item.gameObject); }
        );
    }

    private void OnGetObject(T item)
    {
        item.gameObject.SetActive(true);
        item.Initialize(UnityEngine.Random.Range(_minLifetime, _maxLifetime), position => {
            _pool.Release(item);
            _onObjectDead?.Invoke(position);
        }
        );
    }

    private void OnReleaseObject(T item)
    {
        item.gameObject.SetActive(false);
        Deactivated?.Invoke();
    }
}
