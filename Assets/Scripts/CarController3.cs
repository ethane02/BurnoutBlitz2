using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class CarController3 : MonoBehaviour
{

    public float acceleration = 10f;

    public float maxSpeed = 10f;
    public float turnSpeed = 5f;
    public float driftFactor = 0.9f;  // Adjust for drifting effect
    public float BoostForce = 100f;
    public float launchForce = 100f;
    public Transform centerOfMass;   // Set this to the car's center of mass

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    public GameObject missilePrefab;
    public Transform missileSpawn;
    public float powerCounter = 0;
    public string enemyTag = "Enemy";
    public MissileCounter missileManager;

    public GameObject speedometerNeedle; 

    private SpeedometerScript speedScript;


   void Start()
    {
        // Find the Rigidbody component in the children of car_parent
        rb = GetComponentInChildren<Rigidbody>();
        if (rb == null)
            {
                Debug.LogError("Rigidbody not found in children. Make sure it's attached to car_collider or adjust accordingly.");
                return;
        }

        // Find the SpeedometerScript component
      speedScript = speedometerNeedle.GetComponent<SpeedometerScript>();

        //Check if the SpeedometerScript component is found
        if (speedScript == null)
        {
            Debug.LogError("SpeedometerScript component not found on the speedometer needle object.");

        }
}



    void Update()
    {
        // Get user input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(powerCounter > 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
                powerCounter = powerCounter - 1;
            }
        }
        
    }

    void FixedUpdate()
    {
        // Apply acceleration and steering
        Accelerate();
        Steer();

        // Limit speed
        LimitSpeed();

        // Apply drag for drifting effect
        ApplyDrift();
    }

    public void Accelerate()
    {
        float accelerationForce = verticalInput * acceleration;
        rb.AddForce(transform.forward * accelerationForce);
        speedScript.UpdateNeedle(accelerationForce);
       // Debug.Log("Acceleration force:" + accelerationForce);
    }

    void Steer()
    {
        float turn = horizontalInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void LimitSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void ApplyDrift()
    {
        // Simulate drifting by applying a force perpendicular to the velocity
        Vector3 lateralVelocity = Vector3.Dot(rb.velocity, transform.right) * transform.right;
        Vector3 dragForce = -lateralVelocity * driftFactor;
        rb.AddForce(dragForce);
    }

    /*
        void UpdateWheel(WheelCollider coll, GameObject wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "SpeedBoost":
                maxSpeed = 60f;
                ApplyBoost(rb);
                break;
            case "SlowPad":
                ApplySlow(rb);
                break;
            case "PowerUp":
                missileManager.UpdateScore(1);
                powerCounter = powerCounter + 1;
                break;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "SpeedBoost":
                maxSpeed = 10f;
                break;
        }
    }

    void ApplyBoost(Rigidbody carRb)
    {
        carRb.AddForce(transform.forward * BoostForce);
    }

    void ApplySlow(Rigidbody carRb)
    {
        carRb.AddForce(-carRb.velocity.normalized * acceleration);
    }

    void Fire()
    {
        //var missile = (GameObject)Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);
        //missile.GetComponent<Rigidbody>().velocity = missile.transform.forward * 10;
        GameObject missile = Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);

        // Get the SmartMissile3D script from the instantiated missile
        GameObject enemy = FindClosestEnemy();
        if(enemy != null)
        {
            missile.transform.LookAt(enemy.transform);
        }

        // Apply an impulse force to the missile in the forward direction
        Rigidbody missileRb = missile.GetComponent<Rigidbody>();
        missileRb.velocity = missile.transform.forward * launchForce;
        missileManager.UpdateScore(-1);



    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if(enemies.Length == 0)
        {
            return null;
        }
        Transform closestEnemy = enemies[0].transform;
        float closestDistance = Vector3.Distance(transform.position, closestEnemy.position);

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < closestDistance) 
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy.gameObject;
    }
    

}

 