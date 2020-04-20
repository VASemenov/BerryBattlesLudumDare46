using System;
using System.Collections;
using System.Collections.Generic;
using References;
using UnityEngine;
using Random = UnityEngine.Random;

public class Queen : MonoBehaviour
{
    public ParticleSystem chewingParticles;
    private AudioSource _source;
    public EButton button;
    private Soldier soldier;
    public Soldier queenGuard;
    public Battalion[] availableBattalions;
    public IntReference healthRefernce;

    private void Start()
    {
        soldier = GetComponent<Soldier>();
        HideButton();
        _source = GetComponent<AudioSource>();
        chewingParticles.Stop();
    }

    public void Feed()
    {
        chewingParticles.Play();
        _source.Play();
        HideButton();
        soldier.battalion.army.AddBattalion(availableBattalions[Random.Range(0, availableBattalions.Length)]);
        for (var i = 0; i < 3; i++)
        {
            soldier.battalion.AddSoldier(queenGuard);
        }

        healthRefernce.Value = healthRefernce.constantValue;
        soldier.battalion.army.commander.health = healthRefernce.Value;


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
