using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawner<Cube> _cubeSpawner;
    [SerializeField] private Spawner<Bomb> _bombSpawner;

    private void Awake()
    {
        _cubeSpawner.OnDead = 
            position =>
        {
            _bombSpawner.Spawn(position);
        };
    }
}

