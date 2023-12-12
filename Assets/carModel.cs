using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carModel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject[] carModels;

    private void Awake()
    {
        ChooseCarModel(SaveManager.instance.currentCar);
    }
    private void ChooseCarModel(int _index)
    {
        Instantiate(carModels[_index], transform.position, Quaternion.identity, transform);
    }
}
