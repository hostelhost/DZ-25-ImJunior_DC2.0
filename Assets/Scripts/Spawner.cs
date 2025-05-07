using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour //очень внимательно смотри на последние два вопроса deepSeek помощь в дженериках, добавление интерфейсов.
{
    //домашнее задание
    // 1.”—¬ќ»“№!!! все что находитьс€ в пуле. ј именно Ћямбду!
    //4. ѕродолжать по задаче. ƒелать UI

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;

    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private WaitForSeconds _spawnInterval = new WaitForSeconds(0.5f);

    private Pool<Cube> _cubePool = new();
    private Pool<Bomb> _bombPool = new();

    private void Awake()
    {
        CreatePools();
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            SetPositionCube(_cubePool.Get());

            yield return _spawnInterval;
        }
    }

    private void CreatePools()
    {
        _cubePool.CreatePool(_cubePrefab, _minLifetime, _maxLifetime, SpawnBombAtPosition);
        _bombPool.CreatePool(_bombPrefab, _minLifetime, _maxLifetime);
    }

    private void SetPositionCube(Cube cube)
    {
        cube.transform.position = GetRandomPosition();
    }

    private Vector3 GetRandomPosition()
    {
        float radius = transform.localScale.x / 2;

        return transform.position + Random.insideUnitSphere * radius;
    }

    private void SpawnBombAtPosition(Vector3 vector3)
    {
        Bomb bomb = _bombPool.Get();
        bomb.transform.position = vector3;
    }


































    //private void OnGetCube(Cube cube)
    //{
    //    cube.gameObject.SetActive(true);
    //    cube.Initialize(Random.Range(_minLifetime, _maxLifetime), OnCubeDied);
    //    cube.transform.position = GetRandomPosition();
    //}

    //private void OnGetBomb(Bomb bomb)
    //{
    //    bomb.gameObject.SetActive(true);
    //    bomb.Initialize(Random.Range(_minLifetime, _maxLifetime), OnRealaseBomb);
    //}

    //private void OnCubeDied(Cube cube)
    //{
    //    Vector3 vector3 = cube.transform.position;
    //    OnReleaseCube(cube);
    //    SpawnBombAtPosition(vector3);
    //}

    //private void OnReleaseCube(Cube cube) =>
    //    cube.gameObject.SetActive(false);

    //private void OnRealaseBomb(Bomb bomb) =>
    //    bomb.gameObject.SetActive(false);

}
