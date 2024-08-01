using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagnetPower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI magnetTimeText;
    [SerializeField] private GameObject magnetUI;
    [SerializeField] private GameObject Player;
    [SerializeField] private float maxPowerTime;
    private float remainingPowerTime = -1;
    public static int magnetEnable = -1;
    
    void Update()
    {
        if (magnetEnable == 1)
        {
            if (!magnetUI.activeInHierarchy)
            {
                magnetUI.SetActive(true);
                remainingPowerTime = maxPowerTime;
            }

            if (remainingPowerTime > 0)
            {
                transform.position = Player.transform.position;
                remainingPowerTime = remainingPowerTime - Time.deltaTime;
                magnetTimeText.text = ((int)remainingPowerTime).ToString();
            }
            

            else
            {
                magnetEnable = -1;
                magnetUI.SetActive(false);
            }

        }
    }
}