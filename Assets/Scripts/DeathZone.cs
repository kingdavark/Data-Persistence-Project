using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject GameoverText;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        MainManager.instance.GameOver();

        GameoverText.SetActive(true);
    }
}
