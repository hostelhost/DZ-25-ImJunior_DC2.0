using System;
using TMPro;
using UnityEngine;

public class SpawnStatisticsDisplay : MonoBehaviour
{
    private TextMeshPro _totalSpawnedText;
    private TextMeshPro _totalCreatedText;
    private TextMeshPro _currentActiveText;

    private int _totalSpawned;
    private int _totalCreated;

    public void Initialize(TextMeshPro totalSpawnedText, TextMeshPro totalCreatedText, TextMeshPro currentActiveText)
    {
        _totalSpawnedText = totalSpawnedText;
        _totalCreatedText = totalCreatedText;
        _currentActiveText = currentActiveText;
    }

    public void OnTotalSpawned() =>
        Print(_totalSpawnedText, AddCount(_totalSpawned));

    public void OnTotalCreated() =>
        Print(_totalCreatedText, AddCount(_totalCreated));

    public void OnCurrentActive(int currentActive) =>
        Print(_currentActiveText, currentActive);

    private int AddCount(int count)
    {
        return count += 1;
    }

    private void Print(TextMeshPro changeable, int count) =>
        changeable.text = Convert.ToString(count);
}
