using TMPro;
using UnityEngine;

public class GameStatsAggregator : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [SerializeField] private TextMeshProUGUI _cubeTotalSpawnedText;
    [SerializeField] private TextMeshProUGUI _cubeTotalCreatedText;
    [SerializeField] private TextMeshProUGUI _cubeCurrentActiveText;

    [SerializeField] private TextMeshProUGUI _bombTotalSpawnedText;
    [SerializeField] private TextMeshProUGUI _bombTotalCreatedText;
    [SerializeField] private TextMeshProUGUI _bombCurrentActiveText;

    private SpawnStatisticsDisplay _cubeDisplay;
    private SpawnStatisticsDisplay _bombDisplay;

    private void Awake()
    {
        // Проверка на null
        if (_cubeTotalSpawnedText == null || _cubeTotalCreatedText == null || _cubeCurrentActiveText == null)
            Debug.Log("Поля пользовательского интерфейса куба не назначены!");

        if (_bombTotalSpawnedText == null || _bombTotalCreatedText == null || _bombCurrentActiveText == null)
            Debug.Log("Поля пользовательского интерфейса бомбы не назначены!");

        _cubeDisplay = new (_cubeTotalSpawnedText, _cubeTotalCreatedText, _cubeCurrentActiveText);
        _bombDisplay = new (_bombTotalSpawnedText, _bombTotalCreatedText, _bombCurrentActiveText);
    }

    private void OnEnable()
    {
        _spawner.CubePool.Spawned += CubeTotalSpawned;
        _spawner.CubePool.Created += CubeTotalCreated;
        _spawner.CubePool.Deactivated += CubeCurrentActive;
        _spawner.CubePool.Spawned += CubeCurrentActive;

        _spawner.BombPool.Spawned += BombTotalSpawned;
        _spawner.BombPool.Created += BombTotalCreated;
        _spawner.BombPool.Deactivated += BombCurrentActive;
        _spawner.BombPool.Spawned += BombCurrentActive;
    }

    private void OnDisable()
    {
        _spawner.CubePool.Spawned -= CubeTotalSpawned;
        _spawner.CubePool.Created -= CubeTotalCreated;
        _spawner.CubePool.Deactivated -= CubeCurrentActive;
        _spawner.CubePool.Spawned -= CubeCurrentActive;

        _spawner.BombPool.Spawned -= BombTotalSpawned;
        _spawner.BombPool.Created -= BombTotalCreated;
        _spawner.BombPool.Deactivated -= BombCurrentActive;
        _spawner.BombPool.Spawned -= BombCurrentActive;
    }

    private void CubeTotalSpawned()=>
        _cubeDisplay.OnTotalSpawned();

    private void BombTotalSpawned() =>
        _bombDisplay.OnTotalSpawned();

    private void CubeCurrentActive() =>
        _cubeDisplay.OnCurrentActive(_spawner.CubePool.CountActive());

    private void BombCurrentActive()=>
        _bombDisplay.OnCurrentActive(_spawner.BombPool.CountActive());

    private void CubeTotalCreated(int count)=>
        _cubeDisplay.OnTotalCreated(count);

    private void BombTotalCreated(int count) =>
        _bombDisplay.OnTotalCreated(count);
}
