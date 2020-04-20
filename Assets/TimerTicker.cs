using System.Collections;
using System.Collections.Generic;
using References;
using UnityEngine;

public class TimerTicker : MonoBehaviour
{
    public IntReference timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(ReduceTime), 0.1f, 0.9f);
    }

    private void ReduceTime()
    {
        timeLeft.value--;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
