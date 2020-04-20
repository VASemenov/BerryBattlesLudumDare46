using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject screenTransition;
    private bool isBlocked = false;
    
    void Start()
    {
        var rect = GetComponent<RectTransform>();
        LeanTween.moveY(rect, -65, 2).setEaseOutBounce().setOnComplete(
            () => LeanTween.moveY(rect, -75, 0.5f).setEaseSpring().setLoopPingPong());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isBlocked)
        {
            isBlocked = true;
            screenTransition.LeanScale(Vector3.one, 0.5f)
                .setOnComplete(() => SceneManager.LoadScene("Scenes/SampleScene"));
        }
    }
}
