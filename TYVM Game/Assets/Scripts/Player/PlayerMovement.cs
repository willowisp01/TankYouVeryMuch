using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 10f;
  
    public Camera cam;
    private Vector2 movement, mousePos;
    private GameObject tankMain, tankTower;
    private Rigidbody2D tankTowerBody, tankMainBody;

    private void Awake() {
        // This can also be done via unity drag and drop. 
        tankMain = transform.Find("SmallTankA").gameObject; 
        tankTower = transform.Find("Tower").gameObject;
        tankMainBody = tankMain.GetComponent<Rigidbody2D>();
        tankTowerBody = tankTower.GetComponent<Rigidbody2D>();

        // Can be replaced with tags possibly to avoid hardcoding the name... https://answers.unity.com/questions/893966/how-to-find-child-with-tag.html
    }

    // Start is called before the first frame update
    void Start() {
        Vector2 cameraBottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 cameraTopRight = cam.ScreenToWorldPoint(new Vector3(cam.scaledPixelWidth, cam.scaledPixelHeight, 0));
        Debug.Log(cameraTopRight);
    }

    // Update is called once per frame
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Mouse position (in world coordinates)
    }

    // For physics stuff
    private void FixedUpdate() {
        Vector2 newPosition = tankMainBody.position + movement * moveSpeed * Time.deltaTime;
        tankMainBody.MovePosition(newPosition);
        tankTowerBody.MovePosition(newPosition);
        Vector2 lookDir = mousePos - tankMainBody.position; // Vector subtraction
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90; // Arctangent. We minus 90 because of the 4 quadrants trigonometry.
        tankTowerBody.rotation = angle;    
    }

    //TODO: https://www.youtube.com/watch?v=LNLVOjbrQj4 implement the shooting and rotation in a neater way
    
}