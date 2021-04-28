using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public GameManager gameManager;
    public TextMeshProUGUI timerText;
    float seconds;
    Stopwatch stopwatch;


    // Start is called before the first frame update
    void Start() {
        stopwatch = new Stopwatch();
    }


    // Update is called once per frame
    void Update() {
        if (gameManager.isCountDown || gameManager.GameOver) return;

        TakeTime();
    }

    void TakeTime() {
        seconds += Time.deltaTime;
        var timeSpan = new TimeSpan(0, 0, (int)seconds);
        timerText.SetText("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        if (seconds >= 60 * 60 * 60) gameManager.EndGame();
    }

    public int GetSeconds() {
        return (int)seconds;
    }
}
