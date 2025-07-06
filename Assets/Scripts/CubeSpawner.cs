using UnityEngine;

public class CubeSpawner : Spawner<Cube> 
{
    [SerializeField] private Spawner<Bomb> _bombSpawner;

    private void Awake()
    {
        SetDeathCallback(position =>
        {
            _bombSpawner.SpawnInPosition(position);
        });
    }
}