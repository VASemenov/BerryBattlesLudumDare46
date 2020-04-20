using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bell : MonoBehaviour
{
    public Battalion[] availableBattalions;
    public Army playerArmy;
    private int charge = 1;
    public EButton button;


    private void Start()
    {
        button.gameObject.SetActive(false);
    }

    public void Ring()
    {
        if (charge <= 0) return;
        playerArmy.AddBattalion(availableBattalions[Random.Range(0, availableBattalions.Length)]);
        charge--;
        if (charge == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
            button.gameObject.SetActive(false);
        }
    }

    public void ShowButton()
    {
        button.gameObject.SetActive(true);
    }
    
    public void HideButton()
    {
        button.gameObject.SetActive(false);
    }
    
}
