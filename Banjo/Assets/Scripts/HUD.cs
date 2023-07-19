using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;

public class HUD : MonoBehaviour
{

    [SerializeField] private TMP_Text timer;
    [SerializeField] private TMP_Text vehicleSpeedText;

    private float timeElapsed;
    private double secondsElapsedRounded;
    private int minutesElapsed;
    private float vehicleSpeed;

    [SerializeField] private int boostChagesRemaining;
    [SerializeField] private GameObject[] boostDisplay;
    [SerializeField] private Rigidbody vehicle;

    //Updates timer to show time elapsed
    void Update()
    {
        timeElapsed += Time.deltaTime;
        secondsElapsedRounded = System.Math.Round(timeElapsed, 2);
        if (secondsElapsedRounded >= 60)
        {
            timeElapsed = 0;
            minutesElapsed++;
        }
        timer.text = "Time: " + minutesElapsed + (".") + secondsElapsedRounded;

        //Test input to check boost charges works
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            BoostCharges();
        }

        vehicleSpeed = vehicle.velocity.magnitude;
        vehicleSpeedText.text = "Speed: " + Math.Round(vehicleSpeed) + "km/h";
    }

    //Displays Boosts left available
    public void BoostCharges()
    {
        boostDisplay[boostChagesRemaining-1].SetActive(false);
        boostChagesRemaining--;
    }

}
