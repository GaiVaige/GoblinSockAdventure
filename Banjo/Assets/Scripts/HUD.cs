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

    public float boostTimer;
    public int rechargeTime;
    public int maxBoosts;
    public bool canBoost;

    [SerializeField] public int boostChagesRemaining;
    [SerializeField] private GameObject[] boostDisplay;
    [SerializeField] private Rigidbody vehicle;
    public float speedoMultiplier;

    public void Start()
    {
        maxBoosts = 3;
    }

    //Updates timer to show time elapsed
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && boostChagesRemaining > 0)
        {
            canBoost = true;
            BoostCharges();

        }


        timeElapsed += Time.deltaTime;
        secondsElapsedRounded = System.Math.Round(timeElapsed, 2);
        if (secondsElapsedRounded >= 60)
        {
            timeElapsed = 0;
            minutesElapsed++;
        }
        timer.text = "Time: " + minutesElapsed + (".") + secondsElapsedRounded;

        //Test input to check boost charges works


        BoostRecharge();
        DisplayRefresh();

        //Displays current vehicle speed
        vehicleSpeed = vehicle.velocity.magnitude;
        vehicleSpeedText.text = "Speed: " + Math.Round(vehicleSpeed * speedoMultiplier) + "km/h";
    }

    //Displays Boosts left available
    public void BoostCharges()
    {
        boostDisplay[boostChagesRemaining-1].SetActive(false);
        boostChagesRemaining--;
    }


    public void BoostRecharge()
    {

        if (canBoost)
        {
            boostTimer += Time.deltaTime;


        }

        if (boostTimer >= rechargeTime)
        {
            boostChagesRemaining++;
            canBoost = false;
            boostTimer = 0f;
        }

        if(!canBoost && boostChagesRemaining < maxBoosts)
        {
            canBoost = true;
        }
    }

    public void DisplayRefresh()
    {
        if (boostChagesRemaining <= maxBoosts)
        {
            boostDisplay[boostChagesRemaining - 1].SetActive(true);
        }
    }

}
