
using System.Collections.Generic;
using Structures;
using UnityEngine;

public class Battalion: MonoBehaviour
{
    public int capacity;
    public  Soldier soldierPrefab;
    public  Soldier bannerBearerPrefab;
    public Vector2 center = new Vector2(0, 0);
    private float max_center_deviation = 5f;
    public Soldier bannerBearer;
    public Army army;
    public List<Soldier> soldiers;
    public bool isRanged = false;
    public bool isRoyal;
    public bool chosen = false;

    public void Generate()
    { 
        soldiers = new List<Soldier>();
        AddBannerBearer();
        GenerateSoldiers(capacity);
        
    }
    
    private void GenerateSoldiers(int amount)
    {
        for (var i = 0; i < capacity; i++)
        {
            AddSoldier();
        }
    }

    public void AddBannerBearer()
    {
        soldiers.Add(CreateNewSoldier(true));
    }

    public void AddSoldier(Soldier soldier = null)
    {
        if (soldier == null)
            soldiers.Add(CreateNewSoldier());
        else
        {
            soldiers.Add(AddExisting(soldier));
        }
    }

    private Soldier AddExisting(Soldier soldierPrefab)
    {
        var position = new Vector2(
            bannerBearer.transform.position.x + Random.Range(-max_center_deviation, max_center_deviation),
            bannerBearer.transform.position.y + Random.Range(-max_center_deviation, max_center_deviation)
        ); 
        var soldier = Instantiate(
            soldierPrefab,
            position,
            Quaternion.Euler(0, 0, 0),
            transform
        );
        soldier.battalion = this;
        
        soldier.isBannerBearer = false;
        
        soldier.Build();
        
        return soldier;
    }

    private Soldier CreateNewSoldier(bool isBannerBearer = false)
    {
        Vector2 position;
        if (isBannerBearer)
        {
            position = center;
        }
        else
        {
            position = new Vector2(
                center.x + Random.Range(-max_center_deviation, max_center_deviation),
                center.y + Random.Range(-max_center_deviation, max_center_deviation)
            ); 
        }
        
        
        var soldier = Instantiate(
            isBannerBearer? bannerBearerPrefab : soldierPrefab,
            position,
            Quaternion.Euler(0, 0, 0),
            transform
        );
        soldier.battalion = this;

        if (isBannerBearer)
        {
            bannerBearer = soldier;
        }
        soldier.isBannerBearer = isBannerBearer;
        
        soldier.Build();
        
        return soldier;
    }

    public void RemoveSoldier(Soldier soldier)
    {
        soldiers.Remove(soldier);
    }


    public bool Chosen
    {
        get => chosen;
        set
        {
            chosen = value;
            foreach (var soldier in soldiers)
            {
                if (value)
                    soldier.Choose();
                else
                    soldier.Unhoose();
            }
        }
    }


    public void Charge()
    {
        // Debug.Log(_soldiers.GetAll());
        if (!army.isEnemy)
            Debug.Log(army.enemyArmy.battalions.Count);
        foreach (var soldier in soldiers)
        {
            soldier.GiveOrder("charge");
        }
    }

    public void Hold()
    {
        foreach (var soldier in soldiers)
        {
            soldier.GiveOrder("hold");
        } 
    }
    
}
