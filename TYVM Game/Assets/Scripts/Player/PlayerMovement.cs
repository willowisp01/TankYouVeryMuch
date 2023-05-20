using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private Rigidbody2D myBody;

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
    }
}
