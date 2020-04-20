using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Arrow : MonoBehaviour
{
    public float force = 0.2f;
    private Vector3 forceVector;
    public Transform target;
    private int framesTillDissapear = 60;
    private int framesIn = 0;
    private SpriteRenderer _sprite;
    private Vector3 targetPosition;
    public float speed = 0.02f;
    public GameObject trail;
    private int targetDirection = 1;
    private int rotationTweaker = 0;
    private Soldier targetSoldier;
    
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        targetPosition = target.transform.position + new Vector3(
                             Random.Range(-1f, 1f),
                             Random.Range(-1f, 1f),
                             0);;
        
        if (targetPosition.x < transform.position.x)
        {
            targetDirection = -1;
            rotationTweaker = 180;
        }

        targetSoldier = target.GetComponent<Soldier>();
        transform.eulerAngles = new Vector3(0,0,targetDirection * 80 + rotationTweaker);
    }

    public void Launch()
    {
        
    }

    private void Update()
    {
        if (framesIn < framesTillDissapear)
        {
            MoveUp();
        }
        else if (framesIn == framesTillDissapear)
        {
            Shift();
        }
        else if (framesIn > 200)
        {
            Destroy(gameObject);
        }
        else
        {
            MoveDown();
        }
        
        
        
    }

    public void MoveUp()
    {
        var multiplicator = (1 - framesIn / framesTillDissapear);
        transform.position += new Vector3(targetDirection * speed * 1.5f * multiplicator, speed * 6 * multiplicator, 0);
        framesIn++;
        var color = _sprite.color;
        
        _sprite.color = new Color(color.r, color.g, color.b, 1 - 0.01f * framesIn);

    }

    public void Shift()
    {
        transform.position = targetPosition + new Vector3(-speed * 1.5f * framesTillDissapear * targetDirection, speed * 6 * framesTillDissapear, 0);
        transform.eulerAngles = new Vector3(0,0,-targetDirection * 80 + rotationTweaker);
        var color = _sprite.color;
        _sprite.color = new Color(color.r, color.g, color.b, 0.5f);
        framesIn++;
    }

    public void MoveDown()
    {
        var multiplicator = (1- framesIn / framesTillDissapear);
        transform.position += new Vector3(-speed * 1.5f * multiplicator * targetDirection, speed * 6 * multiplicator, 0);
        
        var color = _sprite.color;
        _sprite.color = new Color(color.r, color.g, color.b,  0.01f * (framesIn - framesTillDissapear));
        
        if (Vector3.Distance(targetPosition, transform.position) < 0.09f)
        {
            if (target != null)
            {
                targetSoldier.ReduceHealth(25);
            }

            var effect = Instantiate(trail,
                targetPosition,
                Quaternion.Euler(0, rotationTweaker, 0));
            // effect.Play();
            Destroy(gameObject);
        }

        framesIn++;
    }

}
