using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CameraController camera;

    [SerializeField]
    private GameObject StatisticBoard;
    [SerializeField]
    private Vector3 statisticCameraPosition;
    [SerializeField]
    private Vector3 statisticCameraRotation;
    [SerializeField]
    private TextMeshPro statisticText;
    [SerializeField]
    private TextMeshPro gameResultText;

    [SerializeField]
    private TransportBeltMovung belt;

    [SerializeField]
    private Spawner spawner;

    public static GameManager Instance;

    private float levelTimer;
    private bool gameOn;

    private int caughtCount;
    private int missedCount;

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
        spawner.isSpawning = true;
        levelTimer = 5;
        caughtCount = 0;
        missedCount = 0;
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
                spawner.isSpawning = false;
                DeactivateTrash();
                belt.SetMovement(false);
                Debug.Log("Time!");
                timerText.text = "Time!";
                StartCoroutine(EndGameCoroutine());
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

    public void IncreaseCaught()
    {
        caughtCount++;
    }

    public void IncreaseMissed()
    {
        missedCount++;
    }

    private void ActivateTrash()
    {
        for (int i = 0; i < spawner.transform.childCount; i++)
        {
            GameObject child = spawner.transform.GetChild(i).gameObject;
            child.GetComponent<TrashObject>().canCatch = true;
        }
    }

    private void DeactivateTrash()
    {
        for (int i = 0; i < spawner.transform.childCount; i++)
        {
            GameObject child = spawner.transform.GetChild(i).gameObject;
            TrashObject childTO = child.GetComponent<TrashObject>();
            childTO.canCatch = false;
            childTO.MouseUp();
        }
    }

    private IEnumerator EndGameCoroutine()
    {
        float countAll = caughtCount + missedCount;
        float caugthPerCent = caughtCount / countAll * 100;
        float missedPerCent = missedCount / countAll * 100;
        string statisticString = $"Caught: {System.Math.Round(caugthPerCent, 2)}%{System.Environment.NewLine}" +
                                 $"Missed: {System.Math.Round(missedPerCent, 2)}%";
        bool isLevelCompleete = caugthPerCent >= 95f;
        yield return new WaitForSeconds(3);
        StatisticBoard.SetActive(true);
        statisticText.text = statisticString;
        if (isLevelCompleete)
        {
            gameResultText.text = "Level completed!";
            gameResultText.color = Color.green;
        }
        else
        {
            gameResultText.text = "Level failed!";
            gameResultText.color = Color.red;
        }
        camera.Move(statisticCameraPosition, statisticCameraRotation);
    }
}
