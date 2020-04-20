using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    public bool isAttached = false;
    private Transform attachmentobject;
    public EButton button;
    
    // Start is called before the first frame update
    void Start()
    {
        HideButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttached)
        {
            transform.position = attachmentobject.position + Vector3.up * 0.2f;
        }
    }

    public void Attach(Transform soldierTransform)
    {
        HideButton();
        isAttached = true;
        attachmentobject = soldierTransform;
    }
    
    public void Detach()
    {
        ShowButton();
        isAttached = false;
        transform.position -= Vector3.up * 0.2f;

    }
    
    public void ShowButton()
    {
        button.gameObject.SetActive(true);
    }
    
    public void HideButton()
    {
        button.gameObject.SetActive(false);
    }
}
