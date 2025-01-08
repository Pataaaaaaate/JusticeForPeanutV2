using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Chrono : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("TimerSettings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;


    public GameObject endMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (endMenu != null)
        {
            endMenu.SetActive(false); // Assurez-vous que le menu est désactivé au départ
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (hasLimit && ((countDown && currentTime <= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
            OpenEndMenu();   // Ouvre le menu de fin

            
        }

        SetTimerText();
    }
    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0.0");
    }

    private void OpenEndMenu()
    {
        if (endMenu != null)
        {
            endMenu.SetActive(true); // Active le menu de fin
            Time.timeScale = 0;      // Met le jeu en pause
        }
        else
        {
            Debug.LogWarning("End Menu n'est pas assigné dans l'inspecteur.");
        }
    }



}
