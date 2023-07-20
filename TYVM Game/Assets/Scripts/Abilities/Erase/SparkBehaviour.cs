using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkBehaviour : MonoBehaviour
{
    private float activeTime = 0.583f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, activeTime);
    }

}
