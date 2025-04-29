using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour //очень внимательно смотри на последние два вопроса deepSeek помощь в дженериках, добавление интерфейсов.
{
    float time = 0f;
    private void Update()
    {
        if (time >= 0.5)
        {
            _cubePool.Get();
            time = 0;
        }

        time += Time.deltaTime;
    }

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;

    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private ObjectPool<Cube> _cubePool;
    private ObjectPool<Bomb> _bombPool;

    private void Start()
    {
        CreatePools();
    }

    private void CreatePools()
    {
        _cubePool = new ObjectPool<Cube>(
              СreateСube,
              OnGetCube,
              OnReleaseCube,
              cube => { cube.Died -= OnCubeDied; }
              );

        _bombPool = new ObjectPool<Bomb>(
            CreateBomb,
            OnGetBomb,
            OnRealaseBomb,
            bomb => bomb.Died -= OnRealaseBomb
            );
    }

    private Cube СreateСube()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.Died += OnCubeDied;
        return cube;
    }

    private Bomb CreateBomb()
    {
        Bomb bomb = Instantiate(_bombPrefab);
        bomb.Died += OnRealaseBomb;
        return bomb;
    }

    private void OnGetCube(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.Initialize(Random.Range(_minLifetime, _maxLifetime));
        cube.transform.position = GetRandomPosition();
    }

    private void OnGetBomb(Bomb bomb)
    {
        bomb.gameObject.SetActive(true);
        bomb.Initialize(Random.Range(_minLifetime, _maxLifetime));
    }

    private void OnCubeDied(Cube cube)
    {
        Vector3 vector3 = cube.transform.position;
        OnReleaseCube(cube);
        SpawnBombAtPosition(vector3);
    }

    private void SpawnBombAtPosition(Vector3 vector3)
    {
        Bomb bomb = _bombPool.Get();
        bomb.transform.position = vector3;
    }

    private void OnReleaseCube(Cube cube) =>
        cube.gameObject.SetActive(false);

    private void OnRealaseBomb(Bomb bomb) =>
        bomb.gameObject.SetActive(false);

    private Vector3 GetRandomPosition()
    {
        float radius = transform.localScale.x / 2;

        return transform.position + Random.insideUnitSphere * radius;
    }
}
