using System;
using TMPro;

public class SpawnStatisticsDisplay
{
    private TextMeshProUGUI _totalSpawnedText;
    private TextMeshProUGUI _totalCreatedText;
    private TextMeshProUGUI _currentActiveText;

    private int _totalSpawned = 0;

    public SpawnStatisticsDisplay(TextMeshProUGUI totalSpawnedText, TextMeshProUGUI totalCreatedText, TextMeshProUGUI currentActiveText)
    {
        _totalSpawnedText = totalSpawnedText;
        _totalCreatedText = totalCreatedText;
        _currentActiveText = currentActiveText;
    }

    public void OnTotalSpawned() =>
        Print(_totalSpawnedText, ++_totalSpawned);

    public void OnTotalCreated(int totalCreated) =>
        Print(_totalCreatedText, totalCreated);

    public void OnCurrentActive(int currentActive) =>
        Print(_currentActiveText, currentActive);

    private void Print(TextMeshProUGUI changeable, int count) =>
        changeable.text = Convert.ToString(count);
}
