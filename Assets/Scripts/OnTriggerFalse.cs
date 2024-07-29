using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerFalse : MonoBehaviour
{
    public float t = -1;
    private Transform NextPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (MagnetPower.magnetEnable == 1 && other.CompareTag("Magnet"))
        {
            NextPosition = other.transform;
            t = 0;
        }
        else
        {
            gameObject.SetActive(false);
            t = -1;
        }
    }

    private void Update()
    {
        if (t >= 0)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, NextPosition.position, t);
            if (t >= 1)
                t = -1;
        }
    }
}