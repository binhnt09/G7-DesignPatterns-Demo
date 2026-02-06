using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            gameManager.AddScore(1);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            collision.GetComponent<Enemy>().TakeDamage();
        }
    }
}
