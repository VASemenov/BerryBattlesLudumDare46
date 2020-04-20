using System;
using References;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Soldier : MonoBehaviour
{
    public ParticleSystem trail;
    public ParticleSystem fightFog;
    public GameObject blood;
    public int id = 0;
    public int row = 0;
    public WeaponObject weapon;
    public Weapon weaponReference;
    public Battalion battalion;
    public bool inFormation = true;
    public bool inZone = false;
    public float speed = 1.2f;
    public float spreadFromBanner = 1;
    public bool isBannerBearer = false;
    public Faction forceFaction;
    public string currentOrder;
    public int orderDelay = 100;
    public int constantOrderDelay;
    public Soldier target;
    public int maxHealth = 100;
    public int health = 100;
    private Vector3 standardWeaponPosition;
    protected bool immortal = false;
    protected bool inRangeOfPlayer = false;
    private bool inDefencePosition = false;
    public GameObject highlight;
    public IntReference healthReference;
    public bool isCommander = false;
    public GameObject gameoverBanner;
    [Header("Sounds")] 
    private AudioSource _audioSource;
    public AudioClip[] battlecries;
    public AudioClip defence;
    public AudioClip swordFight;
    private bool gameOver = false;
    public WaveSpawner waves;
    public TextMeshProUGUI gameOverText;

    public Vector3 _positionToHold;
    public Rigidbody2D rb;

    private int timeBetweenAttacks = 3;
    private float startTime;

    protected bool isBuilt = false;

    private void Start()
    {
        
        if (name == "Commander")
        {
            Build();
        }
    }

    public void Build()
    {
        rb = GetComponent<Rigidbody2D>();
        if (highlight != null)
            highlight.SetActive(false);
        speed += Random.Range(-0.2f, 0.2f);
        constantOrderDelay = Random.Range(0, 100);
        orderDelay = constantOrderDelay;
        health = maxHealth;
        if (fightFog != null) 
            fightFog.Stop();
        
        Color color;
        if (battalion == null && forceFaction != null)
        {
            color = forceFaction.color;
        }
        else
        {
            color = battalion.army.faction.color;
        }
        
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.material.color = color;
        }

        weaponReference.SetWeapon(weapon);
        if (isBannerBearer)
        {
            immortal = true;
            weaponReference.GetComponent<SpriteRenderer>().material.color = battalion.army.faction.bannerColor;
        }
        
        _positionToHold = transform.position;
        standardWeaponPosition = weaponReference.transform.position;
        _audioSource = GetComponent<AudioSource>();
        isBuilt = true;
        
        
    }

    private void Update()
    {
        if (isCommander && gameOver)
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        if (isCommander && (Mathf.Abs(rb.velocity.x) > 0.5f || Mathf.Abs(rb.velocity.y) > 0.5f))
        {
            trail.Play();
        }
        else
        {
            trail.Stop();
        }
        if (rb.velocity.x > speed/2)
        {
            FlipSpriteRight();
        } 
        else if (rb.velocity.x < -speed/2)
        {
            FlipSpriteLeft();
        }
    }


    public void Choose()
    {
        if (highlight != null)
            highlight.SetActive(true);
    }
    
    public void Unhoose()
    {
        if (highlight != null)
            highlight.SetActive(false);
    }

    public void FlipSpriteRight()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void FlipSpriteLeft()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void GiveOrder(string order, bool muteSound = false)
    {

        switch (order)
        {
            case "hold":
                // WeaponsToHoldPosition();
                // _positionToHold = transform.position;
                target = battalion.bannerBearer;
                if (!muteSound && defence != null)
                {
                    _audioSource.clip = defence;
                    _audioSource.volume = 0.15f;
                    _audioSource.loop = false;
                    _audioSource.Play();
                }
                break;
                
            case "charge":
                // WeaponsToAttackPosition();
                target = ApproveTarget();
                
                if (!muteSound && battlecries.Length > 0)
                {
                    _audioSource.clip = battlecries[Random.Range(0, battlecries.Length)];
                    _audioSource.volume = Random.Range(0f, 0.6f);
                    _audioSource.loop = false;
                    _audioSource.PlayDelayed(Random.Range(0f, 2f));
                }
                
                
                break;
            case "shoot":
                if (!weapon.isRanged)
                {
                    GiveOrder("charge");
                }
                break;
                
            
        }

        currentOrder = order;
        orderDelay = constantOrderDelay;
        
    }

    protected void WeaponsToAttackPosition()
    {
        if (isBannerBearer) return;
        // weaponReference.transform.eulerAngles = new Vector3(0,0, 0);
        weaponReference.transform.position = gameObject.transform.position = new Vector2(
            weaponReference.parent.transform.position.x + 0.2f, 
            weaponReference.parent.transform.position.y + 0.5f);
    }
    
    protected void WeaponsToHoldPosition()
    {
        weaponReference.transform.eulerAngles = new Vector3(0,0, 0);
        // weaponReference.transform.position = standardWeaponPosition;
    }
    

    protected void RunToTarget()
    {
        if (target == null && !isBannerBearer) return;
        
        Vector3 position = isBannerBearer ? _positionToHold : target.transform.position;
        var position1 = transform.position;
        var directionX = Mathf.RoundToInt((position.x - position1.x) / Mathf.Abs(position.x - position1.x));
        var directionY = Mathf.RoundToInt((position.y - position1.y) / Mathf.Abs(position.y - position1.y));
        var distance = Vector2.Distance(position, position1);
        // var multiplierx = Mathf.Abs(position.x - position1.x) / distance;
        // var multipliery = Mathf.Abs(position.y - position1.y) / distance;

        rb.velocity = new Vector2(directionX * speed, directionY * speed);
    }


    protected Soldier ApproveTarget()
    {
        if (target == null)
        {
            target = GetNearestEnemy();
            if (target == null)
            {
                GiveOrder("hold", true);
            }
        }
            

        return target;
    }


    protected void ShootOnHold()
    {
        target = GetNearestEnemy();
        if (ApproveTarget() != battalion.bannerBearer && Vector3.Distance(transform.position, ApproveTarget().transform.position) <= weapon.reach)
        {
            GiveOrder("shoot");
        }
        else
        {
            GiveOrder("hold", true);
        }
        
    }
    
    protected void RunInCharge()
    {
        if (weapon.isRanged && Vector3.Distance(transform.position, ApproveTarget().transform.position) <= weapon.reach)
        {
            GiveOrder("shoot");
            return;
        }

        if (battalion.isRanged && isBannerBearer)
        {
            return;
        } 
        
        var position = ApproveTarget().transform.position + new Vector3(
            Random.Range(-spreadFromBanner, spreadFromBanner),
            Random.Range(-spreadFromBanner, spreadFromBanner),
            0);
        var position1 = transform.position;
        
        var directionX = Mathf.RoundToInt((position.x - position1.x) / Mathf.Abs(position.x - position1.x));
        var directionY = Mathf.RoundToInt((position.y - position1.y) / Mathf.Abs(position.y - position1.y));
        
        rb.velocity = new Vector2(directionX * speed, directionY * speed);
        // RunToTarget();
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherSoldier = other.gameObject.GetComponent<Soldier>();
        
        if (battalion == null || 
            other.gameObject.GetComponent<Soldier>() == null ||
            other.gameObject.GetComponent<Soldier>().battalion == null) return;

        if (battalion.army.faction != otherSoldier.battalion.army.faction && !isCommander)
        {
            if (!weapon.isRanged)
            {
                _audioSource.clip = swordFight;
                _audioSource.loop = true;
                _audioSource.Play();
                Fog();
                otherSoldier.ReduceHealth(weapon.damage);
            }
            
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        var otherSoldier = other.gameObject.GetComponent<Soldier>();
        if (battalion == null || 
            other.gameObject.GetComponent<Soldier>() == null ||
            other.gameObject.GetComponent<Soldier>().battalion == null) return;
        
        if (battalion.army.faction != otherSoldier.battalion.army.faction  && !isCommander)
        {
            if (Time.time - startTime > timeBetweenAttacks)
                startTime = Time.time;
            else
                return;

            if (!weapon.isRanged)
            {
                _audioSource.Pause();
                Fog();
                otherSoldier.ReduceHealth(weapon.damage);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.CompareTag("Player"))
         {
             inRangeOfPlayer = true;
         }

         if (battalion == null || 
             other.gameObject.GetComponent<Soldier>() == null ||
             other.gameObject.GetComponent<Soldier>().battalion == null) return;

         if (other.gameObject.GetComponent<Soldier>().battalion == battalion)
         {
             inZone = true;
         }
         
         

         
     }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRangeOfPlayer = false;
        }
        
        if (battalion == null || 
            other.gameObject.GetComponent<Soldier>() == null ||
            other.gameObject.GetComponent<Soldier>().battalion == null) return;
        if (other.gameObject.GetComponent<Soldier>().battalion == battalion)
        {
            inZone = false;
        }
        
        
    }

    public void ReduceHealth(int damage)
    {
        if (immortal) return;
        
        health -= inZone ? Mathf.RoundToInt(damage * 0.5f) : damage;

        if (isCommander)
        {
            healthReference.Value = health;
        }

        if (health <= 0)
        {
            if (isCommander)
            {
                GameOver();
                Kill();
            }
            else
            {
                Kill();
            }
            
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Waves survived: " + (waves.waveNumber - 1);
        gameoverBanner.GetComponent<GameOverListener>().listening = true;
        gameoverBanner.GetComponent<RectTransform>().LeanMoveY(111, 1f).setEaseOutBounce().setOnComplete(() =>
        {
            gameOver = true;
            
        });
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // if (battalion == null || 
        //     other.gameObject.GetComponent<Soldier>() == null ||
        //     other.gameObject.GetComponent<Soldier>().battalion == null) return;
        // var otherSoldier = other.gameObject.GetComponent<Soldier>();
        // if (otherSoldier.battalion == battalion && !otherSoldier.isBannerBearer)
        // {
        //     inFormation = true;
        // }
        // if (battalion.army.faction != otherSoldier.battalion.army.faction)
        // {
        //     if (!weapon.isRanged)
        //     {
        //         StopFog();
        //     }
        //     
        // }
    }

    protected virtual void Kill()
    {
        
        Instantiate(blood, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
        battalion.RemoveSoldier(this);
    }

    
    public Soldier GetNearestEnemy()
    {
        if (battalion.army.enemyArmy == null) return null;
        // Debug.Log("HERE");
        
        var enemyBattalions = battalion.army.enemyArmy.battalions;
        // Debug.Log(enemyBattalions.Count);
        if (enemyBattalions.Count == 0) return null;
        
        var closest = enemyBattalions[0];
        var min = Vector2.Distance(closest.transform.position, transform.position);

        foreach (var compareTo in enemyBattalions)
        {
            var distance = Vector2.Distance(compareTo.bannerBearer.transform.position, transform.position);
            if (distance < min)
            {
                min = distance;
                closest = compareTo;
            }
        }

        // Soldier final;
        // if (battalion.army.isEnemy && Random.Range(0, 50) == 1)
        //     final = battalion.army.enemyArmy.commander;
        // else
        //     final = ;

        return closest.soldiers[Random.Range(0, closest.soldiers.Count)];
    }

    public void Fog()
    {
        if (fightFog != null)
            fightFog.Play();
    }

    // public void StopFog()
    // {
    //     if (fightFog != null)
    //         fightFog.Stop();
    // }
}
