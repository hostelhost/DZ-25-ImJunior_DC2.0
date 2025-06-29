using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private WaitForSeconds _spawnInterval = new WaitForSeconds(0.5f);

    private ObjectPool<GameObject> _pool;
    private Action<Vector3> _onObjectDead;

    public event Action<int> Created;
    public event Action Spawned;
    public event Action Deactivated;

    public int CountActive => _pool != null ? _pool.CountActive : 0;

    private void Awake()
    {
        CreatePool();
    }

    private void Start() =>
        StartCoroutine(SpawnCubes());

    private IEnumerator SpawnCubes()
    {
        while (enabled)
        {
            GetCube();

            yield return _spawnInterval;
        }
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<GameObject>(
         createFunc: () => 
         {
            Created?.Invoke(_pool.CountAll);
            return Instantiate(_prefab);
         },
         actionOnGet: OnGetObject,
         actionOnRelease:   OnReleaseObject,
         actionOnDestroy: item => { Destroy(item.gameObject); }
        );
    }

    public GameObject GetCube()
    {
        Spawned?.Invoke();
        return _pool.Get();
    }

    private void OnReleaseObject(GameObject item)
    {
        item.gameObject.SetActive(false);
        Deactivated?.Invoke();
    }

    private void OnGetObject(GameObject item)
    {
        item.gameObject.SetActive(true);

        IAppearing appearing = item.GetComponent<IAppearing>();
        appearing.Initialize(UnityEngine.Random.Range(_minLifetime, _maxLifetime),  position => 
            {
            _pool.Release(item);
            _onObjectDead?.Invoke(position);
            }
        );
    }

















    //[SerializeField] private Cube _cubePrefab;
    //[SerializeField] private Bomb _bombPrefab;
    //[SerializeField] private int _minLifetime = 2;
    //[SerializeField] private int _maxLifetime = 5;

    //private WaitForSeconds _spawnInterval = new WaitForSeconds(0.5f);

    //public Pool<Cube> CubePool { get; private set; } = new();
    //public Pool<Bomb> BombPool { get; private set; } = new();

    //private void Awake()
    //{
    //    CreatePools();
    //}

    //private void Start() =>
    //    StartCoroutine(SpawnCubes());

    //private IEnumerator SpawnCubes()
    //{
    //    while (enabled)
    //    {
    //        SetPositionCube(CubePool.Get());

    //        yield return _spawnInterval;
    //    }
    //}

    //private void CreatePools()
    //{
    //    CubePool.CreatePool(_cubePrefab, _minLifetime, _maxLifetime, SpawnBombAtPosition);
    //    BombPool.CreatePool(_bombPrefab, _minLifetime, _maxLifetime);
    //}

    //private void SetPositionCube(Cube cube)
    //{
    //    cube.transform.position = GetRandomPosition();
    //}

    //private Vector3 GetRandomPosition()
    //{
    //    float radius = transform.localScale.x / 2;

    //    return transform.position + Random.insideUnitSphere * radius;
    //}

    //private void SpawnBombAtPosition(Vector3 vector3)
    //{
    //    Bomb bomb = BombPool.Get();
    //    bomb.transform.position = vector3;
    //}
}
