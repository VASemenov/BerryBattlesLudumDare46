using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    public GameObject rulesSheet;

    private bool isShown = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if (isShown)
            {
                isShown = false;
                rulesSheet.SetActive(false);
            }
            else
            {
                isShown = true;
                rulesSheet.SetActive(true);
            }
        }
    }
}
