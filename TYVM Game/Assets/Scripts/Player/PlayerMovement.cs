using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private Rigidbody2D myBody;
    public Vector3 mousePos, objectPos;

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
        PlayerMove();

    }

    void PlayerMove() {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 
        float verticalInput = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;

        mousePos = Input.mousePosition; //the mouse position (relative to your screen) 
        mousePos.z = 0; 
        objectPos = Camera.main.WorldToScreenPoint(transform.position); //the object position (relative to your screen) 

        float deltaX = mousePos.x - objectPos.x;
        float deltaY = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg; //arctangent.
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 270)); //what are euler angles...

        //TODO: https://www.youtube.com/watch?v=LNLVOjbrQj4 implement the shooting and rotation in a neater way
    }
}
