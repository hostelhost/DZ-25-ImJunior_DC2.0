using System;
using System.Collections;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : SpawnedObject
{
    [SerializeField] private T _prefab;

    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;
    [SerializeField] private bool _autoSpawn;

    private WaitForSeconds _spawnInterval = new WaitForSeconds(0.5f);
    public Pool<T> Pool { get; private set; } = new();
    private Action<Vector3> OnDead = null;

    private void Start()
    {
        Pool.CreatePool(_prefab, _minLifetime, _maxLifetime, position =>
        {
            OnDead?.Invoke(position);
        });

        if (_autoSpawn)
            StartCoroutine(AutoSpawnLoop());
    }

    public void SetDeathCallback(Action<Vector3> onDead) =>   
        OnDead = onDead;
    
    public void SpawnInPosition(Vector3 position)
    {
        T appearing = Pool.Get();
        appearing.transform.position = position;
    }

    private IEnumerator AutoSpawnLoop()
    {
        while (enabled)
        {
            SpawnInPosition(GetRandomPosition());
            yield return _spawnInterval;
        }
    }

    private Vector3 GetRandomPosition()
    {
        float radius = transform.localScale.x / 2;
        return transform.position + UnityEngine.Random.insideUnitSphere * radius;
    }
}