using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDestroy : MonoBehaviour {

    Transform deadReplacement;
    bool DeadBodyGuard = false;
    float PointValue = 100f;

    void Update()
    {
        transform.Find("BodyGuardHead");
        if (!transform.find("BodyGuardHead"))
        {
            DeadBodyGuard = true;
            GameObject.Find("ScoreDynamic").SendMessageUpwards("NewScore", PointValue, SendMessageOptions.DontRequireReciever);
            Kill();
        }
    }

    void Kill()
    {
        if (DeadBodyGuard)
        {
            Destroy(gameObject);
        }
        if (deadReplacement)
        {
            Transform dead = Instatiate(deadReplacement, transform.position, transform.rotation);
        }
    }
}
