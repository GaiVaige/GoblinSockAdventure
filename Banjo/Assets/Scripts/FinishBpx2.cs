using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBpx2 : MonoBehaviour
{
    public TimeSaver stopwatch;
    public HUD timeGrabber;


    // Start is called before the first frame update
    void Start()
    {
        stopwatch = FindObjectOfType<TimeSaver>();
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        stopwatch.Time2 = "Time: " + timeGrabber.minutesElapsed + (":") + timeGrabber.secondsElapsedRounded;
        StartCoroutine(ReloadStart());
    }

    public IEnumerator ReloadStart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
