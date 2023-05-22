using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
  
    public Camera cam;
    private Vector2 movement, mousePos;
    private float angle;
    private Vector2 lookDir;
    private GameObject tankMain, tankTower;
    private Rigidbody2D tankTowerBody, tankMainBody;

    private void Awake() {
        //this can also be done via unity drag and drop. 
        tankMain = transform.Find("SmallTankA").gameObject; 
        tankTower = transform.Find("Tower").gameObject;
        tankMainBody = tankMain.GetComponent<Rigidbody2D>();
        tankTowerBody = tankTower.GetComponent<Rigidbody2D>();

        //can be replaced with tags possibly to avoid hardcoding the name... https://answers.unity.com/questions/893966/how-to-find-child-with-tag.html
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

    private void FixedUpdate() { //for physics stuff
        Vector2 newPosition = tankMainBody.position + movement * moveSpeed * Time.deltaTime;
        tankMainBody.MovePosition(newPosition);
        tankTowerBody.MovePosition(newPosition);
        lookDir = mousePos - tankMainBody.position; //vector subtraction
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90; //arctangent. we minus 90 because of the 4 quadrants trigonometry.
        tankTowerBody.rotation = angle;    
    }

    //TODO: https://www.youtube.com/watch?v=LNLVOjbrQj4 implement the shooting and rotation in a neater way
    
}
