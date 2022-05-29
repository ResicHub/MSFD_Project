using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float levelTimer;
    private bool gameOn;

    [SerializeField]
    private TextMeshPro timerText;
    private string timerTextCopy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameOn = true;
        levelTimer = 10;
    }

    private void FixedUpdate()
    {
        if (gameOn)
        {
            levelTimer -= Time.deltaTime;
            ResetTimerText();
            if (levelTimer <= 0)
            {
                gameOn = false;
                Debug.Log("Time!");
                timerText.text = "Time!";
            }
        }
    }

    private void ResetTimerText()
    {
        string newTime = $"0{(int)levelTimer / 60}:{TimePart((int)levelTimer % 60)}";
        if (newTime != timerTextCopy)
        {
            timerTextCopy = newTime;
            timerText.text = newTime;
        }
    }

    private string TimePart(int sec)
    {
        if (sec <= 9)
        {
            return $"0{sec}";
        }
        return sec.ToString();
    }
}
