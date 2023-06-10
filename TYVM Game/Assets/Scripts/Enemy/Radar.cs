using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    //[SerializeField]
    float rotationSpeed = 90; //degrees per second.
    //too high and will often miss. too low and tank will hardly find any suitable shots.

    [SerializeField]
    Vector2 lookDir;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 3.0f, Color.red, 0.01f);
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        lookDir = transform.up.normalized;
    }

    public Vector2 GetDirection() { //returns the direction the radar is pointing to. 
        return lookDir;
    }
}
