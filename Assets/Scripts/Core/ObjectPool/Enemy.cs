using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 3f;
    private Vector3 startPos;
    private bool movingRight = true;
    private int hp = 1;

    public EnemySpawner spawner;

    void OnEnable()
    {
        hp = 1;
        startPos = transform.position;
        movingRight = true;
    }

    void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
                Flip();
            }
        }
    }
    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    public int coinDrop = 3;
    public void TakeDamage()
    {
        hp--;
        if (hp <= 0)
        {
            for (int i = 0; i < coinDrop; i++)
            {
                Vector3 offset = new Vector3(
                    Random.Range(-0.5f, 1.2f),
                    Random.Range(1.5f, 2.2f),
                    0
                );
                ObjectPool.Instance.GetCoin(transform.position + offset);
            }

            gameObject.SetActive(false);

            if (spawner != null)
            {
                Debug.Log("Calling Respawn()");
                spawner.Respawn(1f);
            }
        }
    }
}
