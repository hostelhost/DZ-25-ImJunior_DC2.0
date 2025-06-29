using System;
using TMPro;
using UnityEngine;

public class StatisticsDisplay<T> where T : MonoBehaviour, IAppearing
{
    private Pool<T> _pool;

    private TextMeshProUGUI _totalSpawnedText;
    private TextMeshProUGUI _totalCreatedText;
    private TextMeshProUGUI _currentActiveText;

    private int _totalSpawned = 0;

    public StatisticsDisplay(Pool<T> pool, TextMeshProUGUI totalSpawnedText, TextMeshProUGUI totalCreatedText, TextMeshProUGUI currentActiveText)
    {
        _pool = pool;
        _totalSpawnedText = totalSpawnedText;
        _totalCreatedText = totalCreatedText;
        _currentActiveText = currentActiveText;
    }

    private void OnDisable()
    {
        _pool.Spawned -= OnTotalSpawned;
        _pool.Created -= OnTotalCreated;
        _pool.Deactivated -= OnCurrentActive;
        _pool.Spawned -= OnCurrentActive;
    }

    public void Subscribe()
    {
        _pool.Spawned += OnTotalSpawned;
        _pool.Created += OnTotalCreated;
        _pool.Deactivated += OnCurrentActive;
        _pool.Spawned += OnCurrentActive;
    }

    private void OnTotalSpawned() =>
        Print(_totalSpawnedText, ++_totalSpawned);

    private void OnTotalCreated(int totalCreated) =>
        Print(_totalCreatedText, totalCreated);

    private void OnCurrentActive()=>      
        Print(_currentActiveText, _pool.CountActive);
    
    private void Print(TextMeshProUGUI changeable, int count) =>
        changeable.text = Convert.ToString(count);
}
