using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RacingManager : MonoBehaviour
{
    public GameObject Cp;
    public GameObject CheckpointHolder;

    public GameObject[] Cars;
    public Transform[] CheckpointPositions;
    public GameObject[] CheckpointForEachCar;

    public CarSelectionManager carSelectionManager;  // Reference to the car selection manager

    public int currentCar;


    private int totalCars;
    private int totalCheckpoints;

    public TextMeshProUGUI positionUI;

    public TextMeshProUGUI name1;

    public TextMeshProUGUI name2;


    public List<GameObject> activeCars = new List<GameObject>();  // Use a List instead of an array

    void Start(){
        totalCars = Cars.Length;
        totalCheckpoints = CheckpointHolder.transform.childCount;

        setCheckpoints(); 
        setCarPosition();

        currentCar = carSelectionManager.GetComponent<CarSelectionManager>().currentCar;

        // Assuming currentCar is already set by CarSelectionManager
    if (currentCar >= 0 && currentCar < totalCars)
    {
        Cars[0] = activeCars[currentCar];
    }
    }



    void setCheckpoints(){
        CheckpointPositions = new Transform[totalCheckpoints];

        for(int i = 0; i < totalCheckpoints; i++){
            CheckpointPositions[i] = CheckpointHolder.transform.GetChild(i).transform;
        }

        CheckpointForEachCar = new GameObject[totalCars];

        for(int i = 0; i < totalCars; i++){
            CheckpointForEachCar[i] = Instantiate(Cp, CheckpointPositions[0].position, CheckpointPositions[0].rotation);
            CheckpointForEachCar[i].name = "CP" + i;
            CheckpointForEachCar[i].layer = 11 + i;
        }
    }

    public void CarCollectedCp(int carNumber, int cpNumber){
        CheckpointForEachCar[carNumber].transform.position = CheckpointPositions[cpNumber].transform.position;
        CheckpointForEachCar[carNumber].transform.rotation = CheckpointPositions[cpNumber].transform.rotation;

        comparePositions(carNumber);
    }

void comparePositions(int carNumber)
{
    CarCpManager currentCar = Cars[carNumber].GetComponent<CarCpManager>();

    if (currentCar.CarPosition > 1)
    {
        int currentCarPos = currentCar.CarPosition;
        int currentCarCp = currentCar.cpCrossed;
        int currentCarLap = currentCar.currentLap;

        CarCpManager carInFront = null;
        int carInFrontPos = 0;
        int carInFrontCp = 0;
        int carInFrontLap = 0;

        for (int i = 0; i < totalCars; i++)
        {
            CarCpManager candidateCar = Cars[i].GetComponent<CarCpManager>();

            if (candidateCar.currentLap < currentCarLap)
            {
                // Candidate car is on a lower lap, so it is considered in front
                carInFront = candidateCar;
                carInFrontCp = carInFront.cpCrossed;
                carInFrontPos = carInFront.CarPosition;
                carInFrontLap = carInFront.currentLap;
                break;
            }
            else if (candidateCar.currentLap == currentCarLap && candidateCar.CarPosition == currentCarPos - 1)
            {
                // Candidate car is on the same lap and one position in front
                carInFront = candidateCar;
                carInFrontCp = carInFront.cpCrossed;
                carInFrontPos = carInFront.CarPosition;
                carInFrontLap = carInFront.currentLap;
                break;
            }
        }

        if (currentCarCp > carInFrontCp || (currentCarCp == carInFrontCp && currentCarLap > carInFrontLap))
        {
            currentCar.CarPosition = currentCarPos - 1;
            carInFront.CarPosition = carInFrontPos + 1;

            Debug.Log("Car" + carNumber + " has overtaken " + carInFront.CarNumber);
        }

         if (currentCar.tag == "Player")
        {
            positionUI.text = currentCar.CarPosition + "/" + totalCars;
        }
    }
}

 public void UpdateEndScreen()
    {
        Debug.Log("Game ended");

        CarCpManager playerCar = FindCarByTag("Player");
        CarCpManager aiCar1 = FindCarByTag("AI1");

        if(aiCar1.CarPosition == 1){
            name1.text = "AI BOT";
            name2.text = "Player";
            Debug.Log("Bot w");


        }
        else{
            name1.text = "Player";
            name2.text = "AI Bot";
            Debug.Log("Player w");

        }

    }


    void setCarPosition(){
        for(int i = 0 ; i < totalCars; i++)
        {
            Cars[i].GetComponent<CarCpManager>().CarPosition = i+1;
            Cars[i].GetComponent<CarCpManager>().CarNumber = i;
    
        }   
    }


     CarCpManager FindCarByTag(string tag)
    {
        foreach (GameObject car in Cars)
        {
            if (car.CompareTag(tag))
            {
                return car.GetComponent<CarCpManager>();
            }
        }
        return null;
    }

    public void RaceFinished()
    {
        UpdateEndScreen();
    }


}