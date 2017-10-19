using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewDistance : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Camera.main.farClipPlane = 500;
            Debug.Log("Entered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Camera.main.farClipPlane = 5000;
            Debug.Log("Exited");
        }
    }
}
