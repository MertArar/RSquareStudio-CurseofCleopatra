using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    public float magnetRadius = 10f;  // Mıknatısın etkili olacağı alanın yarıçapı
    public float magnetStrength = 5f;  // Mıknatısın coinleri çekme gücü
    private bool isMagnetActive = false;

    // MagnetStick objesine temas edildiğinde çalışacak
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagnetStick"))
        {
            isMagnetActive = true; // Mıknatıs etkisini başlat
            Destroy(other.gameObject);  // MagnetStick objesini yok et
            StartCoroutine(DisableMagnetAfterTime(5f));  // Mıknatıs etkisi 5 saniye sürecek
        }
    }

    // Mıknatıs etkisini süresi dolduktan sonra devre dışı bırak
    private IEnumerator DisableMagnetAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMagnetActive = false;
    }

    // Mıknatıs etkisi aktifse coinleri çek
    private void Update()
    {
        if (isMagnetActive)
        {
            AttractCoins();
        }
    }

    // Coin objelerini oyuncuya çekme işlemi
    private void AttractCoins()
    {
        Collider[] coins = Physics.OverlapSphere(transform.position, magnetRadius);  // Mıknatıs etkisi alanındaki objeleri bul
        foreach (Collider coin in coins)
        {
            if (coin.CompareTag("Coin"))
            {
                Vector3 direction = transform.position - coin.transform.position;  // Coin'den oyuncuya doğru yön
                coin.transform.position += direction.normalized * magnetStrength * Time.deltaTime;  // Coin'i çek
            }
        }
    }
}