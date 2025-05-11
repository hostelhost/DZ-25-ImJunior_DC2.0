using TMPro;
using UnityEngine;

public class GameStatsAggregator : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [SerializeField] private TextMeshPro _cubeTotalSpawnedText;
    [SerializeField] private TextMeshPro _cubeTotalCreatedText;
    [SerializeField] private TextMeshPro _cubeCurrentActiveText;

    [SerializeField] private TextMeshPro _bombTotalSpawnedText;
    [SerializeField] private TextMeshPro _bombTotalCreatedText;
    [SerializeField] private TextMeshPro _bombCurrentActiveText;

    private SpawnStatisticsDisplay _cubesDisplay;
    private SpawnStatisticsDisplay _bombsDisplay;

    private void Awake()
    {
        _cubesDisplay = new();
        _cubesDisplay.Initialize(_cubeTotalSpawnedText, _cubeTotalCreatedText, _cubeCurrentActiveText);
        _bombsDisplay = new();
        _bombsDisplay.Initialize(_bombTotalSpawnedText, _bombTotalCreatedText, _bombCurrentActiveText);
    }




}
