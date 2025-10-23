using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    /* Assign this script to any object in the Scene to display frames per second */

    public float updateInterval = 0.5f; // How often should the number update
    public TextMeshProUGUI fpsText; // Reference to the TextMeshPro object

    private float accum = 0.0f;
    private int frames = 0;
    private float timeleft;
    private float fps;

    // Use this for initialization
    void Start()
    {
        timeleft = updateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = (accum / frames);
            fpsText.text = fps.ToString("F2") + "";
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
