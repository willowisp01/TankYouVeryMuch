using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 50f;
  
    public Camera cam;
    private Vector2 movement, mousePos;
    private GameObject tankHull, tankTower;
    private Rigidbody2D tankHullBody, tankTowerBody;

    private void Awake() {
        // This can also be done via Unity drag and drop.
        tankHull = GameObject.FindWithTag("Hull"); 
        tankTower = GameObject.FindWithTag("Tower");
        tankHullBody = tankHull.GetComponent<Rigidbody2D>();
        tankTowerBody = tankTower.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start() {
        Vector2 cameraBottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 cameraTopRight = cam.ScreenToWorldPoint(new Vector3(cam.scaledPixelWidth, cam.scaledPixelHeight, 0));
        Debug.Log(cameraTopRight);
    }

    // Update is called once per frame
    private void Update() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Mouse position (in world coordinates)
    }

    // Best to use FixedUpdate() for physics to keep in sync with the physics engine as Update() is dependent on framerate which can vary 
    private void FixedUpdate() {
        Movement();
        TowerRotation();
        BodyRotation();
    }

    // This method allows for tank movement
    private void Movement() {
        Vector2 newPosition = tankHullBody.position + movement * moveSpeed * Time.deltaTime;
        tankHullBody.MovePosition(newPosition);
        tankTowerBody.MovePosition(newPosition);
        // Can consider using Vector2.SmoothDamp to smoothen movement, as well as potentially add some small acceleration and deceleration for more realistic movement
    }

    // This method allows for turret rotation to be controlled by the mouse
    private void TowerRotation() {
        Vector2 lookDir = mousePos - tankHullBody.position; // Vector subtraction
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90; // Arctangent; we minus 90 because of the 4 quadrants trigonometry
        tankTowerBody.rotation = angle;
        // Can consider changing to Quaternion instead to avoid doing the math ourselves
    }

    // This method ensures that the hull faces the direction of movement
    private void BodyRotation() {
        if (movement != Vector2.zero) {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement * moveSpeed); // 2D games are upwards and downwards (x and y-axis)
            Quaternion hullRotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);    
            tankHullBody.MoveRotation(hullRotation);
            // TODO: abrupt rotation >= 90° results in snapping to the new direction which should be changed
        }
    }

    //TODO: https://www.youtube.com/watch?v=LNLVOjbrQj4 implement the shooting and rotation in a neater way
    
}