using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("Prefabs")]
    public GameObject coinPrefab;
    public GameObject enemyPrefab;

    [Header("Pool Size")]
    public int coinPoolSize = 30;
    public int enemyPoolSize = 6;

    private Queue<GameObject> coinPool = new Queue<GameObject>(); // fifo
    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        InitPool(coinPrefab, coinPoolSize, coinPool);
        InitPool(enemyPrefab, enemyPoolSize, enemyPool);
    }

    void InitPool(GameObject prefab, int size, Queue<GameObject> pool) // Tạo sẵn ob và disable all, cất vào Queue
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetCoin(Vector3 pos)
    {
        GameObject coin = coinPool.Dequeue();
        coin.transform.position = pos;
        coin.SetActive(true);
        //coinPool.Enqueue(coin);
        return coin;
    }
    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        coinPool.Enqueue(coin);
    }
    public GameObject GetEnemy(Vector3 pos)
    {
        if (enemyPool.Count == 0)
        {
            Debug.LogError("Enemy pool empty");
            return null;
        }

        GameObject enemy = enemyPool.Dequeue();
        enemy.transform.position = pos;
        enemy.SetActive(true);
        //enemyPool.Enqueue(enemy);
        return enemy;
    }
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }

}
