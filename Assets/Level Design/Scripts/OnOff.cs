
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{

    public Light flashlight;

    public Vector3 UpPosition = new Vector3(0, -.3f, 0);
    public Vector3 DownPosition = new Vector3(0, -1.3f, 0);
    public float speed = 1.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IEnumerator routine;
            if (flashlight.enabled)
            {
                Debug.Log("should be down");
                routine = AnimateFlashlight(UpPosition, DownPosition);
            }
            else
            {
                Debug.Log("should be up");
                routine = AnimateFlashlight(DownPosition, UpPosition);
            }
            StartCoroutine(routine);
        }
    }
    IEnumerator AnimateFlashlight(Vector3 start, Vector3 end)
    {
        float startTime = Time.time;
        float progress = 0;
        while (progress < speed)
        {
            transform.localPosition = Vector3.Lerp(start, end, (Time.time - startTime) / speed);
            yield return null;
            progress = Time.time - startTime;
        }
        //transform.position = end;

        flashlight.enabled = !flashlight.enabled;
    }
}