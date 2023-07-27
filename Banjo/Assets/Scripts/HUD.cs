using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class HUD : MonoBehaviour
{

    [SerializeField] private TMP_Text timer;
    [SerializeField] private TMP_Text vehicleSpeedText;

    private float maxSpeedAngle = -120;
    private float minSpeedAngle = 120;


    public bool isInGameplay;
    private float timeElapsed;
    public double secondsElapsedRounded;
    public int minutesElapsed;
    private float vehicleSpeed;
    private float speedMax = 200;
    public float boostTimer;
    public int rechargeTime;
    public int maxBoosts;
    public Transform speedNeedleTransform;
    public bool canBoost;

    private Image boostDisplayImage;
    private float boostDisplayTimer;
    private bool isBoosting = false;
    private bool boostRechargeWhileBoosting = false;
    private bool currentlyBoosting = false;
    private bool newBoostChargeAdded = false;
    private bool newBoostScaleCompleted = false;
    private Transform newRechargedBoost;
    private float newRechargedBoostTimer;

    [SerializeField] public int boostChargesRemaining;
    [SerializeField] private GameObject[] boostDisplay;
    [SerializeField] private Rigidbody vehicle;
    public float speedoMultiplier;

    public void Start()
    {
        maxBoosts = 3;
        boostChargesRemaining = 2;
        boostDisplayImage = boostDisplay[boostChargesRemaining - 1].GetComponent<Image>();
    }

    //Updates timer to show time elapsed
    void Update()
    {
        if (isInGameplay)
        {
            if (currentlyBoosting == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && boostChargesRemaining > 0)
                {
                    canBoost = true;
                    BoostCharges();
                    boostDisplayTimer = 0;
                }
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
        }


        boostDisplayTimer += Time.deltaTime;

        if (newBoostChargeAdded == true)
        {
            if ((newRechargedBoostTimer <= 0.2f) && (!newBoostScaleCompleted))
            {
                newRechargedBoostTimer += Time.deltaTime;
                
                if (newRechargedBoostTimer >= 0.2f)
                {
                    newBoostScaleCompleted = true;
                }
            }
            
            else
            {
                newRechargedBoostTimer -= Time.deltaTime;
            }


            if (newRechargedBoostTimer < 0)
            {
                newRechargedBoostTimer = 0;
                newBoostScaleCompleted = false;
                newBoostChargeAdded = false;
            }
      
            newRechargedBoost.transform.localScale = new Vector3(1 + newRechargedBoostTimer, 1 + newRechargedBoostTimer, 1);        
        }

        if (isBoosting)
        {
            if (boostDisplayTimer < 2)
            {
                if (boostRechargeWhileBoosting == true)
                {
                    boostDisplayImage = boostDisplay[boostChargesRemaining].GetComponent<Image>();
                    boostDisplay[boostChargesRemaining - 1].SetActive(true);
                }
                boostDisplayImage.fillAmount = 1 - (boostDisplayTimer / 2);
            }
            if (boostDisplayTimer > 2)
            {
                if (boostChargesRemaining > 0)
                {
                    boostDisplay[boostChargesRemaining - 1].SetActive(false);
                    isBoosting = false;
                    boostRechargeWhileBoosting = false;
                }
                currentlyBoosting = false;
            }

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
        boostDisplayImage = boostDisplay[boostChargesRemaining-1].GetComponent<Image>();
        isBoosting = true;
        currentlyBoosting = true;
        //boostDisplay[boostChagesRemaining-1].SetActive(false);
        boostChargesRemaining--;
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
            boostChargesRemaining++;
            newRechargedBoost = boostDisplay[boostChargesRemaining - 1].GetComponent<Transform>();
            newBoostChargeAdded = true;
            newRechargedBoostTimer = 0;
            boostDisplayImage.fillAmount = 1;
            canBoost = false;
            boostTimer = 0f;
            if (isBoosting == true)
            {
                boostRechargeWhileBoosting = true;
            }
        }

        if(!canBoost && boostChargesRemaining < maxBoosts)
        {
            canBoost = true;
        }
    }

    public void DisplayRefresh()
    {
        if ((boostChargesRemaining <= maxBoosts) && (boostChargesRemaining > 0))
        {
            boostDisplay[boostChargesRemaining - 1].SetActive(true);
        }
    }

}
