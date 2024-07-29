using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPower : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float maxPowerTime;
    private float T = -1;
    public static int magnetEnable = -1;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (magnetEnable == 1)
        {
            transform.position = Player.transform.position;
            T += Time.deltaTime / maxPowerTime;

            if (T >= 1)
            {
                magnetEnable = -1;
            }
        }
    }
}
