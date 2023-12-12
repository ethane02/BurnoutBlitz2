using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MissileCounter : MonoBehaviour
{
    public TextMeshProUGUI missileText;
    private int missiles = 0;

    public void UpdateScore(int amount)
    {
        missiles += amount;
        missileText.text = "" + missiles;
    }
}
