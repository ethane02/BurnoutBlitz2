using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCpManager : MonoBehaviour
{
    public int CarNumber;
    public int cpCrossed = 0;
    private bool hasCrossedCheckpoint = false;
    public int CarPosition;

    public int currentLap = 0;

    public RacingManager racemanager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint") && !hasCrossedCheckpoint)
        {
            cpCrossed += 1;
            racemanager.CarCollectedCp(CarNumber, cpCrossed);
            hasCrossedCheckpoint = true; // Set the flag to true to indicate the checkpoint has been crossed in this frame
        }
        else
        {
            Debug.LogError("Not a checkpoint or already crossed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            hasCrossedCheckpoint = false; // Reset the flag when the car exits the checkpoint
        }
    }

    public void updateLap(){
        currentLap += 1;
    }
}
