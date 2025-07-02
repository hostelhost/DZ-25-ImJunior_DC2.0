using TMPro;
using UnityEngine;

public class GameStatsAggregator : MonoBehaviour
{
    [SerializeField] private Spawner<Cube> _spawnerCube;
    [SerializeField] private Spawner<Bomb> _spawnerBomb;

    [SerializeField] private TextMeshProUGUI _cubeTotalSpawnedText;
    [SerializeField] private TextMeshProUGUI _cubeTotalCreatedText;
    [SerializeField] private TextMeshProUGUI _cubeCurrentActiveText;

    [SerializeField] private TextMeshProUGUI _bombTotalSpawnedText;
    [SerializeField] private TextMeshProUGUI _bombTotalCreatedText;
    [SerializeField] private TextMeshProUGUI _bombCurrentActiveText;

    private StatisticsDisplay<Cube> _cubeDisplay;
    private StatisticsDisplay<Bomb> _bombDisplay;

    private void Awake()
    {
        _cubeDisplay = new(_spawnerCube.Pool, _cubeTotalSpawnedText, _cubeTotalCreatedText, _cubeCurrentActiveText);
        _bombDisplay = new(_spawnerBomb.Pool, _bombTotalSpawnedText, _bombTotalCreatedText, _bombCurrentActiveText);
    }

    private void OnEnable()
    {
        _cubeDisplay.Subscribe();
        _bombDisplay.Subscribe();
    }

    private void OnDisable()
    {
        _cubeDisplay.Unsubscribe();
        _bombDisplay.Unsubscribe();
    }
}