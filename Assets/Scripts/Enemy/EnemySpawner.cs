using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float randomRange = 0f;
    [SerializeField] private float spawnDistance = 8f;
    [SerializeField] private Vector3 spawnOffsetLocal = Vector3.zero;
    [SerializeField] private bool autoStart = true;
    [SerializeField] private EnemyTarget target;
    private ObjectPool<Enemy> pool;
    private float _timer;
    private bool _running;

    void Awake()
    {
        pool = new ObjectPool<Enemy>(
            createFunc: () =>
            {
                var go = Instantiate(enemyPrefab, transform);
                go.GetComponent<Enemy>().Init(pool);
                return go;
            },
            actionOnGet: (go) =>
            {
                go.gameObject.SetActive(true);
            },
            actionOnRelease: (go) =>
            {
                go.gameObject.SetActive(false);
                go.transform.position = Vector3.zero;
            },
            actionOnDestroy: (go) => Destroy(go),
            defaultCapacity: 10,
            maxSize: 50
        );
    }

    void OnEnable()
    {
        _running = autoStart;
        _timer = 0f;
    }

    public void StartSpawning() => _running = true;
    public void StopSpawning() => _running = false;

    void Update()
    {
        if (!_running) return;

        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            _timer = 0f + Random.Range(-randomRange, randomRange);
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        Enemy enemy = pool.Get();
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
        enemy.target = target;
        enemy.OnSpawnFromPool();
    }
}
