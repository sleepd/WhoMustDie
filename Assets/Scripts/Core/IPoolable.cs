using UnityEngine;

public interface IPoolable
{
    void OnSpawnFromPool();
    void OnRecycleToPool();
}