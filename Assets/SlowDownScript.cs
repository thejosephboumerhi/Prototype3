using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownScript : MonoBehaviour
{
    //Code source in wiki
    //How slow the slow motion is
    public float slowMotionTimeScale;

    //Floats for the normal and slowed time
    private float startTimeScale;
    private float startFixedDeltaTime;
    // Start is called before the first frame update
    void Start()
    {
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    //Command for slow down
    void Update()
    {
        //Starts the slow down
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartSlowMotion();
        }

        //Stops the slow down
        if (Input.GetKeyUp(KeyCode.E))
        {
            StopSlowMotion();
        }
    }

    //Time gets effected by the public float
    private void StartSlowMotion()
    {
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
    }

    //Returns time back to normal
    private void StopSlowMotion()
    {
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }
}
