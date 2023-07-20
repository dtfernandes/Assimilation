using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private static GameTimer instance; // Singleton instance
    private float timer = 0f; // Elapsed time
    private static bool _start;

    public static GameTimer Instance
    {
        get { return instance; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        // Check if an instance already exists; if yes, destroy this object to maintain the singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Set the current instance to this object
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void StartTimer()
    {
        _start = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!_start) return;
       
        // Update the timer value every frame
        timer += Time.deltaTime;
    }

    public float GetElapsedSeconds()
    {
        return timer;
    }

    public string GetElapsedTime()
    {
        return FormatTime(timer);
    }

    public static string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Return the time formatted as "min:sec" with "00:00" format
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Stop()
    {
        _start = false;
    }

    public void Reset()
    {
       timer = 0f;
    }


}
 