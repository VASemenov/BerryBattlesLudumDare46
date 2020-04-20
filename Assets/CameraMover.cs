using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    public Army playerArmy;
    public GameObject objectToFollow;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y,
            transform.position.z);
        
    }
}
