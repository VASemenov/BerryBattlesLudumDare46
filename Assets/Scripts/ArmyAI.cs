using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAI : Army
{
    private string currentOrder;
    private void Update()
    {
        if (!isBuilt)
        {
            return;
        }
        if (PlayerArmyInRange())
        {
            Charge();
            currentOrder = "charge";
        }
    }

    private bool PlayerArmyInRange()
    {
        return true;
    }
    
    
    private void Charge()
    {
        if (currentOrder == "charge")
        {
            return;
        }
        foreach (var chosenBattalion in battalions)
        {
            chosenBattalion.Charge();
        }
    }
    
    private void Hold()
    {
        foreach (var chosenBattalion in battalions)
        {
            chosenBattalion.Hold();
        }
    }
}
