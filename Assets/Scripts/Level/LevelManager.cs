
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _finishPrefab;
    [SerializeField] private GameObject _enemyTemplate;
    [SerializeField] private Transform _target;
    [SerializeField] private EventsLisiner _eventsLisiner;
    [SerializeField] private int _enemyCount;

    private GameObject[] _enemies;

    private void Awake()
    {
        _enemies = new GameObject[_enemyCount];
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemies[i] = Instantiate(_enemyTemplate);
            _enemies[i].GetComponent<ChaseState>().InitTarget(_target);
            _eventsLisiner.GetTriggersEvents(_enemies[i].GetComponentInChildren<EnemyTrigger>());
            _eventsLisiner.GetCollisionEvents(_enemies[i].GetComponent<EnemyCollision>());
            _eventsLisiner.GetStateEvents(_enemies[i].GetComponent<Enemy>());
        }
        
    }

    private void Start()
    {
        LevelGeneration generations = new LevelGeneration();
        WallGeneration[,] level = generations.GenerateLevel();

        for (int x = 0; x < level.GetLength(0); x++)
        {
            for (int y = 0; y < level.GetLength(1); y++)
            {
                GameObject wall = Instantiate(_wallPrefab, new Vector2(x, y), Quaternion.identity, transform);

                wall.SetActive(level[x, y]._wallActive);
            }
        }

        Instantiate(_finishPrefab, new Vector2(level.GetLength(0)-1, level.GetLength(1)-1), Quaternion.identity, transform);
        _enemies[1].transform.position = new Vector2(level.GetLength(0) - 1, level.GetLength(1) - 1);
        AstarPath.active.Scan();
    }
}
