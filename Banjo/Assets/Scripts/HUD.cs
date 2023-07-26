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

    private float maxSpeedAngle = -120;
    private float minSpeedAngle = 120;

    private float timeElapsed;
    private double secondsElapsedRounded;
    private int minutesElapsed;
    private float vehicleSpeed;
    private float speedMax = 200;
    public float boostTimer;
    public int rechargeTime;
    public int maxBoosts;
    public Transform speedNeedleTransform;
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
        secondsElapsedRounded = System.Math.Round(timeElapsed, 3);
        if (secondsElapsedRounded >= 60)
        {
            timeElapsed = 0;
            minutesElapsed++;
        }
        
        if (secondsElapsedRounded >= 10)
        {
            timer.text = "Time: " + minutesElapsed + (":") + secondsElapsedRounded;
        }
        else
        {
            timer.text = "Time: " + minutesElapsed + (":0") + secondsElapsedRounded;
        }
        

        //Test input to check boost charges works


        BoostRecharge();
        DisplayRefresh();

        speedNeedleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());

        //Gets vehicle speed
        vehicleSpeed = vehicle.velocity.magnitude;
    }

    //Displays Boosts left available
    public void BoostCharges()
    {
        boostDisplay[boostChagesRemaining-1].SetActive(false);
        boostChagesRemaining--;
    }

    private float GetSpeedRotation()
    {
        float totalAngleSize = minSpeedAngle - maxSpeedAngle;

        float speedNormalized = (vehicleSpeed / speedMax);

        return minSpeedAngle - speedNormalized * totalAngleSize;
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
