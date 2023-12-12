using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float turnSpeed = 5f;
    public float driftFactor = 0.9f;
    public float waypointRange = 1f;
    public float reductionForce = 100f;
    public float reductionDuration = 3f;

    public WayPointContainer waypointContainer; // Create an empty GameObject and attach WayPointContainer script to it
    private List<Transform> waypoints;
    private int currentWaypointIndex = 0;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Define waypoints
        waypoints = waypointContainer.waypoints;

        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints found.");
        }
    }

    private void Update()
    {
        FollowWaypoints();
    }

    private void FixedUpdate()
    {
        Accelerate();
        Steer();
        LimitSpeed();
        ApplyDrift();
    }

    private void FollowWaypoints()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);

        if (distanceToWaypoint < waypointRange)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex == waypoints.Count)
                currentWaypointIndex = 0;
        }

        Debug.DrawRay(transform.position, waypoints[currentWaypointIndex].position - transform.position, Color.yellow);

    }


    private void Accelerate()
    {
        float accelerationForce = acceleration;
        rb.AddForce(transform.forward * accelerationForce);
    }

    private void Steer()
    {
        Vector3 targetDir = waypoints[currentWaypointIndex].position - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        float horizontalInput = Mathf.Sign(angle);
        float turn = horizontalInput * turnSpeed * Time.fixedDeltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void LimitSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void ApplyDrift()
    {
        Vector3 lateralVelocity = Vector3.Dot(rb.velocity, transform.right) * transform.right;
        Vector3 dragForce = -lateralVelocity * driftFactor;
        rb.AddForce(dragForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Missile")
        {
            Destroy(collision.gameObject);
            StartCoroutine(ReduceSpeedOverTime());
        }
    }

    IEnumerator ReduceSpeedOverTime()
    {
        // Check if the Rigidbody component is available
        if (rb != null)
        {
            // Calculate the reduction force based on the current velocity
            Vector3 oppositeForce = -rb.velocity.normalized * reductionForce;

            // Calculate the force to be applied each frame
            Vector3 forcePerFrame = oppositeForce / (reductionDuration / Time.fixedDeltaTime);

            // Apply the reduction force over the specified duration
            float elapsedTime = 0f;

            while (elapsedTime < reductionDuration)
            {
                rb.AddForce(forcePerFrame, ForceMode.VelocityChange);
                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
