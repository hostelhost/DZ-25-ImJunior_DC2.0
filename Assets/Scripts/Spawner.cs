using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
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
              cube => { cube.Died -= OnReleaseCube; }
              );

        _bombPool = new ObjectPool<Bomb>(
            CreateBomb,
            OnGetBomb,
            OnRealaseBomb,
            bomb => { bomb.Died -= OnRealaseBomb; }
            );
    }

    private Bomb CreateBomb()
    {
        Bomb bomb = Instantiate(_bombPrefab);
        bomb.Died += OnRealaseBomb;
        return bomb;
    }

    private void OnGetBomb(Bomb bomb)
    {
        bomb.gameObject.SetActive(true);
        bomb.Initialize(Random.RandomRange(_minLifetime, _maxLifetime));
        //bomb.transform.position =                                  //сюда (место появления бомбы)
    }

    private void OnRealaseBomb(Bomb bomb) =>
        bomb.gameObject.SetActive(false);

    private Cube СreateСube()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.Died += OnReleaseCube;
        return cube;
    }

    private void OnGetCube(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.Initialize(Random.RandomRange(_minLifetime, _maxLifetime));
        cube.transform.position = GetRandomPosition();
    }

    private void OnReleaseCube(Cube cube)
    {
        cube.gameObject.SetActive(false); //надо понять как позицию отсюда (место смерти куба) передать



    }

    private Vector3 GetRandomPosition()
    {
        int diameterDivider = 2;
        return (Random.insideUnitSphere + transform.position) * transform.localScale.x / diameterDivider;
    }
}
