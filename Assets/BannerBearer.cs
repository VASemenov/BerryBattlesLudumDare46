using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerBearer : Soldier
{

    // Update is called once per frame
    void Update()
    {
        if (!isBuilt) return;
        
        if (target != null && (target.transform.position.x - transform.position.x) > 10)
        {
            FlipSpriteRight();
        } 
        else if (target != null && (transform.position.x - target.transform.position.x) > 10)
        {
            FlipSpriteLeft();
        }
        
        if (battalion.soldiers.Count < 2)
        {
            // battalion.army.RemoveBattalion(battalion);
            immortal = false;
        }
        
        if (currentOrder != "charge")
        {
            if (Vector3.Distance(_positionToHold, transform.position) > 0.6f)
            {
                RunToTarget();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
            
            
            // transform.position = _positionToHold;
        }
        else
        {
            RunInCharge();
        }
    }
    
    override protected void Kill()
    {
        Instantiate(blood, transform.position, Quaternion.Euler(0, 0, 0));
        battalion.army.RemoveBattalion(battalion);
        Destroy(gameObject);
    }

    
}
