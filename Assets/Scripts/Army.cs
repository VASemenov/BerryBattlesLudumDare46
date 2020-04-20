
using System;
using System.Collections.Generic;
using UnityEngine;

public class Army: MonoBehaviour
{
    public int numberOfBattalions;
    public Battalion battalionPrefab;
    public Faction faction;
    public List<Battalion> battalionPrefabs;
    public Army enemyArmy;
    public Soldier commander;
    public List<Battalion> battalions;
    private Vector2 _center;
    protected bool isBuilt = false;
    public bool isEnemy = false;

    private void Start()
    {
        battalions = new List<Battalion>();

        for (var i = 0; i < battalionPrefabs.Count; i++)
        {
            battalions.Add(Instantiate(
                battalionPrefabs[i], 
                transform.position, 
                Quaternion.Euler(0,0,0),
                transform));

            battalions[i].center = new Vector2(transform.position.x, transform.position.y + 6*i);
            battalions[i].name = "Battalion " + i;
            battalions[i].army = this;
            battalions[i].Generate();
        }
        
        isBuilt = true;

        if (commander != null)
        {
            commander.battalion = battalions[0];
        }
    }

    public void RemoveBattalion(Battalion battalion)
    {
        battalions.Remove(battalion);
        if (battalions.Count == 0 && commander != null)
        {
            Destroy(gameObject);
        }
    }

    public void AddBattalion(Battalion battalion, bool isEnemy = false)
    {
        this.isEnemy = isEnemy;
        var added = Instantiate(
            battalion, 
            transform.position, 
            Quaternion.Euler(0,0,0),
            transform);

        var enemySpawnPosition = isEnemy? 30 : 0;
        if (isEnemy)
        {
            added.center = new Vector2(enemyArmy.commander.transform.position.x - 3 + enemySpawnPosition, enemyArmy.commander.transform.position.y);
            
        }
        else
        {
            added.center = new Vector2(commander.transform.position.x + 5 + enemySpawnPosition, commander.transform.position.y);
        }
        
        added.name = "Battalion " + battalions.Count;
        added.army = this;
        added.Generate();
        
        battalions.Add(added);
        
        if (isEnemy)
            added.Charge();
        // Debug.Log("ADDED TO " + this.name + " " + battalions.Count);
        
    }
    
    // public Soldier GetClosestSoldier(Soldier soldier)
    // {
    //     
    // }
}