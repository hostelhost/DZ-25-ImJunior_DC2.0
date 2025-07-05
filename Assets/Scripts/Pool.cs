using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Appearing
{
    private Queue<T> _pool = new();
    private T _prefab;

    private int _minLifetime;
    private int _maxLifetime;
    private Action<Vector3> _onObjectDead;

    public event Action<int> Created;
    public event Action Spawned;
    public event Action Deactivated;

    public int CountActive { get; private set; }
    public int CountTotal { get; private set; }

    public void CreatePool(T prefab, int minLifetime, int maxLifetime, Action<Vector3> onObjectDead = null)
    {
        _prefab = prefab;
        _minLifetime = minLifetime;
        _maxLifetime = maxLifetime;
        _onObjectDead = onObjectDead;
    }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : CreateNew();

        item.gameObject.SetActive(true);
        item.Initialize(UnityEngine.Random.Range(_minLifetime, _maxLifetime), position =>
        {
            Release(item);
            _onObjectDead?.Invoke(position);
        });

        CountActive++;
        Spawned?.Invoke();
        return item;
    }

    public void Release(T item)
    {
        item.gameObject.SetActive(false);
        _pool.Enqueue(item);
        CountActive--;
        Deactivated?.Invoke();
    }

    private T CreateNew()
    {
        T instance = UnityEngine.Object.Instantiate(_prefab);
        CountTotal++;
        Created?.Invoke(CountTotal);
        return instance;
    }
}