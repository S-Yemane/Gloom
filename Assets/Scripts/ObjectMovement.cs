using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    { //called before performing any physics here. 

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);

        if (moveHorizontal == 3)
        {
            transform.position = new Vector3(0, 0, 0);
        }


    }
    //which ever objects that have trigger enabled will cause the scene to change
    void OnTriggerEnter(Collider other)
    {
        
        Application.LoadLevel(1);

    }
}
