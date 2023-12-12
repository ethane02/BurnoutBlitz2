using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] cars;
    public int currentCar;
    public bool inGamePlayScene = false;
    public float rotationSpeed = 20f; // Rotation speed in degrees per second

    public void Start()
    {
        int selectedCar = PlayerPrefs.GetInt("SelectedCarID");
        if (inGamePlayScene == true)
        {
            cars[selectedCar].SetActive(true);
            currentCar = selectedCar;
        }
    }

    public void Update()
    {
        if (cars[currentCar].activeSelf && inGamePlayScene==false)
        {
            // Rotate the current car around the y-axis
            cars[currentCar].transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    public void Right()
    {
        if (currentCar < cars.Length - 1)
        {
            currentCar += 1;
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].SetActive(false);
            }
            cars[currentCar].SetActive(true);
        }
    }

    public void Left()
    {
        if (currentCar > 0)
        {
            currentCar -= 1;
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].SetActive(false);
            }
            cars[currentCar].SetActive(true);
        }
    }

    public void Selected()
    {
        PlayerPrefs.SetInt("SelectedCarID", currentCar);
        SceneManager.LoadScene(1);
    }
}
