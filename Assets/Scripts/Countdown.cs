using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this line for TextMeshPro


public class Countdown : MonoBehaviour
{

    public TextMeshProUGUI CountDown;
    public GameObject LapTimer;
    public CarController3 CarControls;
    public AudioSource CountdownMusic;

    public AICarController[] aiCars; // Reference to your AI car scripts
                                     // Start is called before the first frame update
    void Start()
    {
        // Find the CarController3 component on the GameObject with tag "Player"
        GameObject playerCar = GameObject.FindGameObjectWithTag("Player");
        if (playerCar != null)
        {
            CarControls = playerCar.GetComponent<CarController3>();
        }

        // Ensure that CarControls is not null before disabling it
        if (CarControls != null)
        {
            CarControls.enabled = false;
        }

        foreach (var aiCar in aiCars)
        {
            aiCar.GetComponent<AICarController>().enabled = false;
        }

        LapTimer.GetComponent<TimeScript>().enabled = false;
        StartCoroutine(CountStart());
    }


    IEnumerator CountStart(){
        yield return new WaitForSeconds(0.5f);
        CountDown.text = "3";
        CountdownMusic.Play();
        CountDown.enabled = true;
        yield return new WaitForSeconds(1);
        CountDown.enabled = false;

        CountDown.text = "2";
        CountDown.enabled = true;
        yield return new WaitForSeconds(1);
        CountDown.enabled = false;

        CountDown.text = "1";
        CountDown.enabled = true;
        yield return new WaitForSeconds(1);
        CountDown.enabled = false;
        CountDown.text = "GO!";
        CountDown.enabled = true;
        yield return new WaitForSeconds(1);
        CountDown.enabled = false;
        CountDown.text = "";
        LapTimer.SetActive(true);
        CarControls.enabled = true;

         foreach(var aiCar in aiCars)
            aiCar.GetComponent<AICarController>().enabled = true;

        LapTimer.GetComponent<TimeScript>().enabled = true;
    } 
}
