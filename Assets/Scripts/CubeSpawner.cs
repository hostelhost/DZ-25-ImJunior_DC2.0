using UnityEngine;

public class CubeSpawner : Spawner<Cube> 
{
    [SerializeField] private Spawner<Bomb> _bombSpawner;

    private void Awake()
    {
        GetAction(position =>
        {
            _bombSpawner.SpawnInPosition(position);
        });
    }
}