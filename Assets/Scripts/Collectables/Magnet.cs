using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour
{
    public float magnetRange = 8f; // Mıknatısın etkili olduğu mesafe
    public float magnetDuration = 5f; // Mıknatısın etkili olduğu süre
    public float magnetForce = 10f; // Çekme kuvveti

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagnetStick"))
        {
            StartCoroutine(MagnetizeCoins());
        }
    }

    private IEnumerator MagnetizeCoins()
    {
        float startTime = Time.time;

        while (Time.time < startTime + magnetDuration)
        {
            Collider[] coins = Physics.OverlapSphere(transform.position, magnetRange);
            
            foreach (Collider coin in coins)
            {
                if (coin.CompareTag("Coin"))
                {
                    Vector3 direction = transform.position - coin.transform.position;
                    coin.GetComponent<Rigidbody>().AddForce(direction.normalized * magnetForce);
                }
            }

            yield return null;
        }
    }
}