
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour {

    public Light flashlight;
    public GameObject Flashlight;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!flashlight.enabled)
            {
                Flashlight.transform.position = new Vector3(Flashlight.transform.position.x, Flashlight.transform.position.y + 1, Flashlight.transform.position.z);
                flashlight.enabled = true;
            }else
            {
                Flashlight.transform.position = new Vector3(Flashlight.transform.position.x, Flashlight.transform.position.y - 1, Flashlight.transform.position.z);
                flashlight.enabled = false;
            }

          // flashlight.enabled = !flashlight.enabled;
        }
	}

    
}
