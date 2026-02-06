using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    void Start()
    {
        Spawn();
    }
    void Spawn()
    {
        GameObject enemy = ObjectPool.Instance.GetEnemy(transform.position);
        if (enemy != null)
        {
            enemy.GetComponent<Enemy>().spawner = this;
        }
    }
    void Update() { }
    public void Respawn(float delay)
    {
        Invoke(nameof(Spawn), delay);
    }
}
