using System.Collections;
using System.Collections.Generic;
using References;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public IntReference health;

    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.sizeDelta = new Vector2((float)health.Value/600 * 400, 10);
    }
}
