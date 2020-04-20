using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetRandomizer : MonoBehaviour
{
    public Sprite[] possibleModels;
    // Start is called before the first frame update
    void Start()
    {
        int choice = Random.Range(0, possibleModels.Length);
        GetComponent<SpriteRenderer>().sprite = possibleModels[choice];
    }
    
}
