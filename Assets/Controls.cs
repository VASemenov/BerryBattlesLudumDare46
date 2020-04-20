using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class Controls : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float speed = 2f;
    public Army armyUnderControl;
    [Header("UI")]
    public Image holdButton;
    public Image onMeButton;
    public Image chargeButton;
    public Image selectionButton;
    public TextMeshProUGUI selectionText;
    private bool canPickUpBerry = false;
    private bool carryingBerry = false;
    private List<Battalion> _chosenBattalions;
    private Berry berry;
    private bool inQueenRange = false;
    private Queen queen;
    private bool canRing = false;
    private Bell bell;
    private bool followingMode = false;
    private bool selectedAll = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey("d"))
        {
            _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        }
        if (Input.GetKey("a"))
        {
            _rigidbody.velocity = new Vector2(-speed, _rigidbody.velocity.y);
        }
        if (Input.GetKey("w"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, speed);
        }
        if (Input.GetKey("s"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -speed);
        }
        

        if (Input.GetKeyDown("1"))
        {
            Hold();
            LeanTween.scale(holdButton.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setEasePunch();
            followingMode = false;
        }

        if (Input.GetKeyDown("3"))
        {
            Charge();
            LeanTween.scale(chargeButton.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setEasePunch();
            followingMode = false;
        }

        if (Input.GetKeyDown("2"))
        {
            OnMe();
            LeanTween.scale(onMeButton.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setEasePunch();
            followingMode = false;
        }
        
        
        if (Input.GetKeyDown("e"))
        {
            if (canPickUpBerry)
            {
                if (!berry.isAttached)
                {
                    berry.Attach(transform);
                    carryingBerry = true;
                }
                else if (berry.isAttached)
                {
                    carryingBerry = false;
                    if (inQueenRange)
                    {
                        queen.Feed();
                        Destroy(berry.gameObject);
                    }
                    else
                    {
                        berry.Detach();
                    }
                    
                }
                    
            } else if (canRing)
            {
                bell.Ring();
            }
        }

        if (Input.GetKeyDown("q"))
        {
            if (selectedAll)
            {
                _chosenBattalions = GetClosestBattalion();
            }
            else
            {
                _chosenBattalions = GetAllBattalions();
            }
            LeanTween.scale(selectionButton.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setEasePunch();
            HighlightChosenBattalions();
            
        }
        
    }


    private void HighlightChosenBattalions()
    {
        foreach (var battalion in armyUnderControl.battalions)
        {
            if (_chosenBattalions.Contains(battalion))
                battalion.Chosen = true;
            else
                battalion.Chosen = false;
        }
    }


    private void FollowMe()
    {
        followingMode = true;
    }
    

    private void OnMe()
    {
        int i = 0;
        foreach (var chosenBattalion in _chosenBattalions)
        {
            chosenBattalion.bannerBearer._positionToHold = GetFormation(i, chosenBattalion);
            chosenBattalion.Hold();
            i++;
        }
    }


    private Vector3 GetFormation(int id, Battalion battalion)
    {
        Vector3 result;
        if ((!battalion.isRanged && !battalion.isRoyal) || !selectedAll)
        {
            return new Vector3(transform.position.x - 1, transform.position.y + 3 * id - _chosenBattalions.Count % 2, 0);
        }
        else if (battalion.isRoyal)
        {
            return new Vector3(transform.position.x - 20, transform.position.y + 0 * id - _chosenBattalions.Count % 2, 0);
        }
        else
        {
            return new Vector3(transform.position.x - 10, transform.position.y + 1 * id - _chosenBattalions.Count % 2, 0);
        }
    } 

    private List<Battalion> GetAllBattalions()
    {
        selectionText.text = "All";
        selectedAll = true;
        return armyUnderControl.battalions;
    }
    
    private List<Battalion> GetClosestBattalion()
    {
        selectionText.text = "Closest";
        selectedAll = false;
        
        var battalions = armyUnderControl.battalions;
        
        var closest = battalions[0];
        var min = Vector2.Distance(closest.transform.position, transform.position);

        foreach (var compareTo in battalions)
        {
            var distance = Vector2.Distance(compareTo.bannerBearer.transform.position, transform.position);
            if (distance < min)
            {
                min = distance;
                closest = compareTo;
            }
        }
        var result = new List<Battalion>();
        result.Add(closest);
        
        return result;
    }



    private void Charge()
    {
        foreach (var chosenBattalion in _chosenBattalions)
        {
            chosenBattalion.Charge();
        }
    }
    
    private void Hold()
    {
        foreach (var chosenBattalion in _chosenBattalions)
        {
            chosenBattalion.Hold();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Queen"))
        {
            queen = other.GetComponent<Queen>();
            inQueenRange = true;
            if (carryingBerry)
            {
                queen.ShowButton();
            }
        }
        else if (other.gameObject.CompareTag("Berry"))
        {
            berry = other.GetComponent<Berry>();
            berry.ShowButton();
            canPickUpBerry = true;
        } 
        else if (other.gameObject.CompareTag("Bell"))
        {
            canRing = true;
            bell = other.GetComponent<Bell>();
            bell.ShowButton();
        }
       
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Berry"))
        {
            berry.HideButton();
            canPickUpBerry = false;
        }
        else if (other.gameObject.CompareTag("Queen"))
        {
            inQueenRange = false;
            queen.HideButton();
        } else if (other.gameObject.CompareTag("Bell"))
        {
            canRing = false;
            bell = other.GetComponent<Bell>();
            bell.HideButton();
        }
    }
}
