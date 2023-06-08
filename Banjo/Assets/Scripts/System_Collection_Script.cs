using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class System_Collection_Script : MonoBehaviour
{
    public System_Coin_Counter coinCounter;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinCounter.coinCount++;
            Destroy(gameObject);
        }
        
        
    }
}
