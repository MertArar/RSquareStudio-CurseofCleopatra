using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerFalse : MonoBehaviour
{
    public AudioClip coinSound;
    public float t = -1;
    private Transform NextPosition;

    private void OnCollisionEnter(Collision other)
    {
        // MagnetPower etkinken coinleri çek
        if (MagnetPower.magnetEnable == 1 && other.gameObject.CompareTag("Player"))
        {
            NextPosition = other.transform;
            t = 0;  // Mıknatıs etkisini başlat
        }
        // MagnetPower kapalıyken normal coin toplama
        else if (other.gameObject.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void Update()
    {
        // Mıknatıs etkisiyle coin çekme
        if (t >= 0)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, NextPosition.position, t);
            
            // Coin oyuncuya ulaştığında toplama işlemi
            if (t >= 1)
            {
                CollectCoin();
            }
        }
    }

    // Coin toplama işlemi
    private void CollectCoin()
    {
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        //CollectableControl.coinCount += 1; // Coin sayısını artır
        gameObject.SetActive(false);  // Coini devre dışı bırak
        t = -1;  // Mıknatıs etkisini sıfırla
    }
}