using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{

    public GameObject TimeSaver;

    public TimeSaver stopwatch;

    public TMP_Text Time1T;
    public TMP_Text Time2T;
    public TMP_Text Time3T;

    private void Awake()
    {
        if (GameObject.Find("TimeSaver") == null)
        {
            if(GameObject.Find("TimeSaver(Clone)") == null)
            Instantiate(TimeSaver);
        }
        else
        {
            Debug.Log("there are two instances of TimeSaver, is this intentional?");
        }

    }
    void Start()
    {
        stopwatch = FindObjectOfType<TimeSaver>();
        
        if (stopwatch.Time1 != null)
        {
            Time1T.text = stopwatch.Time1;
        }


        if (stopwatch.Time2 != null)
        {
            Time2T.text = stopwatch.Time2;
        }


        if (stopwatch.Time3 != null)
        {
            Time3T.text = stopwatch.Time3;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
