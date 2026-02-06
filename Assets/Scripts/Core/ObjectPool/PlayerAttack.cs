using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackHitBox;
    public float attackDuration = 0.2f;

    void Start()
    {
        attackHitBox.SetActive(false);
    }

    public void EnableHitBox()
    {
        attackHitBox.SetActive(true);
    }

    public void DisableHitBox()
    {
        attackHitBox.SetActive(false);
    }
}
