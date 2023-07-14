using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    public float moveSpeed = 10f;

    [SerializeField]
    private float rotationSpeed = 10000f;
  
    private Camera cam;
    private Vector2 movement, smoothedMovement, smoothCurrentVelocity, mousePos, lookDir;
    private Rigidbody2D tankHull, tankTower;

    private void Awake() {
        tankHull = transform.Find("Hull").GetComponent<Rigidbody2D>();
        tankTower = transform.Find("Tower").GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>(); // Looks for the camera
    }

    // Start is called before the first frame update
    private void Start() {
        // Vector2 cameraBottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        // Vector2 cameraTopRight = cam.ScreenToWorldPoint(new Vector3(cam.scaledPixelWidth, cam.scaledPixelHeight, 0));
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
        smoothedMovement = Vector2.SmoothDamp(smoothedMovement, movement, ref smoothCurrentVelocity, 0.1f); // Smoothens movement
        Vector2 newPosition = tankHull.position + smoothedMovement * moveSpeed * Time.deltaTime;
        tankHull.MovePosition(newPosition);
        tankTower.MovePosition(newPosition);
    }

    // This method allows for turret rotation to be controlled by the mouse
    private void TowerRotation() {
        lookDir = mousePos - tankHull.position; // Vector subtraction
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90; // Arctangent; we minus 90 because of the 4 quadrants trigonometry
        tankTower.rotation = angle;
        // Can consider changing to Quaternion instead to avoid doing the math ourselves
    }

    // This method ensures that the hull faces the direction of movement
    private void BodyRotation() {
        if (smoothedMovement != Vector2.zero) {
            Quaternion toRotation = Quaternion.LookRotation(transform.forward, smoothedMovement); // 2D games are upwards and downwards (x and y-axis)
            Quaternion hullRotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            tankHull.MoveRotation(hullRotation);
            // TODO: abrupt rotation >90� results in simply flipping to the new direction which should be changed
        }
    }

    public Vector2 GetLookDir() {
        return lookDir;
    }

    public void UpdateSpeed(float speed) {
        moveSpeed = speed;
    }

    //TODO: https://www.youtube.com/watch?v=LNLVOjbrQj4 implement the shooting and rotation in a neater way
    
}