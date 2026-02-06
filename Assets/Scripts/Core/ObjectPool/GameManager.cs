using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        UpdateScore();
        //for (int i = 0; i < 10; i++)
        //{
        //    Vector3 pos = new Vector3(i * 5, -1, 0);
        //    ObjectPool.Instance.GetEnemy(pos);
        //}
    }

    void Update() { }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }
    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}
