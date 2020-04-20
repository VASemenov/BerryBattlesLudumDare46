using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SoldierAI : Soldier
{
    private float timeBtwAttack = 3;
    public float startTimeBtwAttack;
    private Soldier commander;
    private bool imGoodGuy = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startTimeBtwAttack = Time.time;
        if (battalion.army.gameObject.name == "Player Army")
        {
            imGoodGuy = true;
        }
        else
        {
            commander = battalion.army.enemyArmy.commander;
        }
        
            
        
    }

    private void Update()
    {
        if (!isBuilt) return;

        if (inRangeOfPlayer && !imGoodGuy && Input.GetKeyDown("space"))
        {
            ReduceHealth(commander.weapon.damage);
            commander.Fog();
        }
        
        if (target != null && target.transform.position.x > transform.position.x)
        {
            FlipSpriteRight();
        } 
        else if (target != null && target.transform.position.x < transform.position.x)
        {
            FlipSpriteLeft();
        }
        
        if (orderDelay > 0)
        {
            orderDelay--;
            return;
        }
        
        if (currentOrder == "charge")
        {
            RunInCharge();
            
        } else if (currentOrder == "shoot")
        {
            if (Time.time - startTimeBtwAttack > timeBtwAttack)
            {
                ShootArrow();
            }
            
            if (ApproveTarget().transform.position.x > transform.position.x)
            {
                FlipSpriteLeft();
            }
        
            if (ApproveTarget().transform.position.x > transform.position.x)
            {
                FlipSpriteRight();
            }
        }
        else
        {

            if (!inZone)
            {
                target = battalion.bannerBearer;
                RunToTarget();
            }
            else
            {
                if (weapon.isRanged)
                    ShootOnHold();
            }
        }
        
        
        

    }
    
    
    public void ShootArrow()
    {
        if (target == null)
        {
            target = GetNearestEnemy();
            if (target == null) GiveOrder("hold");
            
            return;
        }
        
        startTimeBtwAttack = Time.time;
        
        var arrow = Instantiate(weapon.projectile,
            transform.position + Vector3.up * 0.6f,
            quaternion.Euler(0,0,0));
        
        arrow.target = target.transform;
        arrow.Launch();
    }

    public bool inRangeForAttack()
    {
        return true;
    }


}
