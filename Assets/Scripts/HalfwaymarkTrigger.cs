using UnityEngine;

public class HalfwayTrigger : MonoBehaviour
{
    public LapTrigger lapTrigger; // Reference to the LapTrigger script

    void OnTriggerEnter(Collider other)
    {
         if (other.name == "BlueCar" || other.name == "RedCar" || other.name == "YellowCar" || other.name == "GreyCar" || other.name == "PurpleCar"  )
        {
            Debug.Log("Hit Lap Completed");

            lapTrigger.HalfwayMarkHit();
        }
    }
}
