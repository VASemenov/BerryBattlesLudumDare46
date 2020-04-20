using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyIt), 5f);
    }

    private void DestroyIt()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
