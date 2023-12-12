using UnityEngine;

public class LapCompleteTrigger : MonoBehaviour
{
    public LapTrigger lapTrigger; // Reference to the LapTrigger script
    private bool lapCompletedThisFrame = false;

    void OnTriggerEnter(Collider other)
    {
        if (!lapCompletedThisFrame)
        {
            if (other.name == "BlueCar" || other.name == "RedCar" || other.name == "YellowCar" || other.name == "GreyCar" || other.name == "PurpleCar")
            {
                Debug.Log("Hit Lap Completed");
                lapTrigger.CompleteLap();
                other.GetComponent<CarCpManager>().updateLap();
            }
            else
            {
                Debug.Log("Hit by A.I. Car");
                other.GetComponent<CarCpManager>().updateLap();
            }

            lapCompletedThisFrame = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (lapCompletedThisFrame)
        {
            lapCompletedThisFrame = false;
        }
    }
}
