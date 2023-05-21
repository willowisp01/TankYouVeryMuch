using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private Rigidbody2D myBody;
    public Camera cam;
    private Vector2 movement;
    public Vector2 mousePos;

    public float angle;

    public Vector2 lookDir;



    private void Awake() {
        myBody = GetComponent<Rigidbody2D>();
        //this can also be done via unity drag and drop. 
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //mouse position (in world coordinates)
    }

    private void FixedUpdate() {
        myBody.MovePosition(myBody.position + movement * moveSpeed * Time.deltaTime);
        lookDir = mousePos - myBody.position; //vector subtraction
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90; //arctangent. we minus 90 because of the 4 quadrants trigonometry.
        myBody.rotation = angle;    
    }

    void PlayerMove() {
        

        //TODO: https://www.youtube.com/watch?v=LNLVOjbrQj4 implement the shooting and rotation in a neater way
    }
}
