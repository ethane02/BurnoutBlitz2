using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRace : MonoBehaviour
{
public void LoadLevel(int _index)
    {
        SceneManager.LoadScene(_index);
    }
}
