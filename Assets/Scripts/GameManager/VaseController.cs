using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseController : MonoBehaviour
{
    private Animator animator;

    private void OnEnable()
    {
        animator.SetBool("Drop", false);
    }
}
