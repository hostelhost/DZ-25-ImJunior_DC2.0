using System;
using System.Collections;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : IAppearing
{
    [SerializeField] private T _prefab;

    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;
    [SerializeField] private bool _autoSpawn;

    private WaitForSeconds _spawnInterval = new WaitForSeconds(0.5f);
    public Pool<T> Pool { get; private set; } = new();
    public Action<Vector3> OnDead; 

    private void Awake()
    {
        Pool.CreatePool(_prefab, _minLifetime, _maxLifetime, position =>
        {
            OnDead?.Invoke(position);
        });
    }

    private void Start()
    {
        if (_autoSpawn)
            StartCoroutine(AutoSpawnLoop());
    }

    private IEnumerator AutoSpawnLoop()
    {
        while (enabled)
        {
            Spawn(GetRandomPosition());
            yield return _spawnInterval;
        }
    }

    public void Spawn(Vector3 position)
    {
        T appearing = Pool.Get();
        appearing.transform.position = position;
    }

    private Vector3 GetRandomPosition()
    {
        float radius = transform.localScale.x / 2;
        return transform.position + UnityEngine.Random.insideUnitSphere * radius;
    }
}