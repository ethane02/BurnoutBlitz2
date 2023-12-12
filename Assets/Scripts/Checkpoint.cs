using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // HashSet to store cars that have passed through this checkpoint
    private HashSet<Transform> passedCars = new HashSet<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider's tag is "Car"
        if (other.CompareTag("Car") || other.CompareTag("Player"))
        {
            Transform carTransform = other.transform;

            // Check if the car has already passed through this checkpoint
            if (!passedCars.Contains(carTransform))
            {
                // Add the car to the set and log the checkpoint passage
                passedCars.Add(carTransform);
                Debug.Log("Car passed checkpoint: " + carTransform.name);
            }
        }
    }

    // You may want to reset the passedCars set at the start of each lap
    public void ResetCheckpoint()
    {
        passedCars.Clear();
    }
}
