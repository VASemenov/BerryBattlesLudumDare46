using System.Collections;
using System.Collections.Generic;
using References;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public IntReference timeTillNextWave;

    private TextMeshProUGUI tmpGui;
    // Start is called before the first frame update
    void Start()
    {
        tmpGui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var minutes = Mathf.RoundToInt(timeTillNextWave.Value / 60);
        var seconds = timeTillNextWave.Value - minutes * 60;

        var minutesText = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        var secondsText = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
    
        tmpGui.text = "Next wave in: " + minutesText + ":" + secondsText;
    }

    private int GetMinutes()
    {
        return timeTillNextWave.value % 60;
    }

    private int GetSeconds()
    {
        return timeTillNextWave.value - GetMinutes() * 60;
    }
    
}
