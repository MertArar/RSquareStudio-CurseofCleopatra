using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    public GameObject countDown3;
    public GameObject countDown2;
    public GameObject countDown1;
    public GameObject countDownGo;
    public GameObject fadeIn;

    

    bool countingStarted = false;

    void Start()
    {
        countDown3.SetActive(false);
        countDown2.SetActive(false);
        countDown1.SetActive(false);
        countDownGo.SetActive(false);
        fadeIn.SetActive(false);
    }

    void Update()
    {
       
        if (Input.GetKeyUp(KeyCode.Space) && !countingStarted)
        {
            countingStarted = true;
            StartCoroutine(StartCountingAfterDelay(1f));
        }
    }

    IEnumerator StartCountingAfterDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        StartCoroutine(CountSequence());
    }

    IEnumerator CountSequence()
    {
       
        countDown3.SetActive(true);
        
        yield return new WaitForSeconds(1);

        
        countDown3.SetActive(false);
        countDown2.SetActive(true);
      
        yield return new WaitForSeconds(1);

        
        countDown2.SetActive(false);
        countDown1.SetActive(true);
     
        yield return new WaitForSeconds(1);

        
        countDown1.SetActive(false);
        countDownGo.SetActive(true);
    
        yield return new WaitForSeconds(1);

        PlayerMovement.currentlyMove = true;
    }
}