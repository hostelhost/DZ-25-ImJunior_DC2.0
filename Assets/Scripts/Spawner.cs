using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private WaitForSeconds _spawnInterval = new WaitForSeconds(0.5f);

    public Pool<Cube> CubePool { get; private set; } = new();
    public Pool<Bomb> BombPool { get; private set; } = new();

    private void Awake()
    {
        CreatePools();
    }

    private void Start() =>
        StartCoroutine(SpawnCubes());

    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            SetPositionCube(CubePool.Get());

            yield return _spawnInterval;
        }
    }

    private void CreatePools()
    {
        CubePool.CreatePool(_cubePrefab, _minLifetime, _maxLifetime, SpawnBombAtPosition);
        BombPool.CreatePool(_bombPrefab, _minLifetime, _maxLifetime);
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
        Bomb bomb = BombPool.Get();
        bomb.transform.position = vector3;
    }
}
