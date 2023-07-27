using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player_Movement_SCript_2 controlOverride;
    public HUD hudOverride;
    public TMP_Text countdownText;
    public GameObject countdownObject;
    public GameObject goObject;
    public float goTimer;


    public int countdownNumber;
    public float countdownTimer;



    // Start is called before the first frame update
    void Start()
    {
        controlOverride.moveSpeed = 0;
        controlOverride.camTurnLimiter = 0;
        controlOverride.camTurnSpeed = 0;
        controlOverride.boostSpeed = 0;
        controlOverride.strafeForce = 0;
        controlOverride.isInGameplay = false;
        hudOverride.isInGameplay = false;
        goObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!controlOverride.isInGameplay)
        {

            if (countdownNumber == 0)
            {
                countdownObject.SetActive(false) ;
            }
            else
            {
                countdownObject.SetActive(true);
            }

            countdownTimer += Time.deltaTime;
            countdownText.text = countdownNumber.ToString();


        }


        if(countdownTimer >= 3f && countdownTimer < 4f)
        {
            countdownNumber = 3;
        }

        if(countdownTimer >= 4f && countdownTimer < 5f)
        {
            countdownNumber = 2;
        }

        if(countdownTimer >= 5f && countdownTimer < 6f)
        {
            countdownNumber = 1;
        }

        if (countdownTimer >= 6.5f)
        {
            countdownNumber = 0;
            countdownTimer = 0f;

            controlOverride.moveSpeed = 135;
            controlOverride.camTurnLimiter = 200;
            controlOverride.camTurnSpeed = 2.2f;
            controlOverride.boostSpeed = 100;
            controlOverride.strafeForce = 53;
            controlOverride.isInGameplay = true;
            hudOverride.isInGameplay = true;
            countdownObject.SetActive(false);



            goObject.SetActive(true);



        }



        if (goObject.activeInHierarchy)
        {
            goTimer += Time.deltaTime;

            if(goTimer >= 1f)
            {
                goObject.SetActive(false);
            }

        }

    }
}
