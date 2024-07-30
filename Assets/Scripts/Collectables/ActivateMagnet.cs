using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMagnet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MagnetPower.magnetEnable = 1;
        gameObject.SetActive(false);
    }
}
