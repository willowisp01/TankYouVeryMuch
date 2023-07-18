using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectileMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float impulse;
    [SerializeField]
    private float accel;
    [SerializeField]
    private float turn;
    private float magnitude;
    private float direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(rb.transform.up * impulse, ForceMode2D.Impulse);
        magnitude = rb.velocity.magnitude;
    }

    private void Update() {
        direction = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        magnitude += accel;
        if (direction > 0) {
            TurnRight();
        } else if (direction < 0) {
            TurnLeft();
        }
        rb.velocity = transform.up.normalized * magnitude;
    }

    private void TurnLeft() {
        transform.Rotate(new Vector3(0, 0, turn));
    }

    private void TurnRight() {
        transform.Rotate(new Vector3(0, 0, -turn));
    }
}

