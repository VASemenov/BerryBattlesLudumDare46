using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.05f, 0.5f).setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
