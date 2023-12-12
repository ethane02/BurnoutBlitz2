using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this line for TextMeshPro

public class TimeScript : MonoBehaviour
{
    public static int minuteCount;
    public static int secondCount;
    public static float milliCount;

    public static string milliDisplay;

    public TextMeshProUGUI milliBox;
    public TextMeshProUGUI minuteBox;
    public TextMeshProUGUI secondBox;
    public static bool timerActive = true; 

    void Start()
    {
        // Initialization code (if any)
    }

    void Update()
    {

        if (timerActive)
        {
            milliCount += Time.deltaTime * 10;
            milliDisplay = milliCount.ToString("F0");
            if (milliCount == 10)
            {
                milliDisplay = "";
                milliDisplay = "0";
            }
            milliBox.text = "" + milliDisplay;

            if (milliCount >= 10)
            {
                milliCount = 0;
                secondCount += 1;
            }

            if (secondCount <= 9)
            {
                secondBox.text = "0" + secondCount + ".";
            }
            else
            {
                secondBox.text = "" + secondCount + ".";
            }

            if (secondCount >= 60)
            {
                secondCount = 0;
                minuteCount += 1;
            }

            if (minuteCount <= 9)
            {
                minuteBox.text = "0" + minuteCount + ":";
            }
            else
            {
                minuteBox.text = "" + minuteCount + ":";
            }
        }
    }
    public static void StopTimer()
    {
        timerActive = false;
    }
}
