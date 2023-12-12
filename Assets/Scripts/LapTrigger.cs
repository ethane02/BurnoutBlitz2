using TMPro;
using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public TextMeshProUGUI LapCounterText;

    public RacingManager racem;

    public TextMeshProUGUI LapText;
    private int lapCount = 0;
    private bool halfWayReached = false;
    private bool firstLapTriggered = false;

    //Trigger for halfway point
    public GameObject HalfwayTrigger;

    //Trigger for the first lap
    public GameObject firstLapTrigger;

    public TimeScript timescript;

    public Canvas racedfinishedUI;



    void Start()
    {
        UpdateLapCounter();
    }

    public void HalfwayMarkHit()
    {
        if (firstLapTriggered) // Only set halfway reached if the first lap is completed
        {
            Debug.Log("Halfway Mark Reached");
            halfWayReached = true;
        }
    }

    public void CompleteLap()
    {
        if (!firstLapTriggered || halfWayReached)
        {
            Debug.Log("Lap Complete Trigger Hit");
            firstLapTriggered = true;
            IncrementLap();
            halfWayReached = false; // Reset for the next lap
        }
    }

    private void IncrementLap()
    {
        lapCount++;

        UpdateLapCounter();
    }

    private void UpdateLapCounter()
    {
        if (lapCount <= 1)
        {
            LapCounterText.text = lapCount + "/1";
        }
        else
        {
            LapCounterText.text = " ";
            LapText.text = "RACE COMPLETE";
            racem.GetComponent<RacingManager>().RaceFinished();
            racedfinishedUI.GetComponent<Canvas>().enabled = true;

            timescript.enabled = false; // Call to stop the timer
        }
    }
}
