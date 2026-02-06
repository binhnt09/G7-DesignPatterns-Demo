using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Disable), 20f); // coin tự biến mất sau 5s
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
