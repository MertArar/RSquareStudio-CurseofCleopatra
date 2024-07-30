using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioClip coinFX;

    void OnTriggerEnter(Collider other)
    {
        
        this.gameObject.SetActive(false);
    }
}
