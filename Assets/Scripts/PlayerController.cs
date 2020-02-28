using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    float speedTimer = 0.0f;
    bool SpeedEffect = false;




    void Start() {
        rb = GetComponent<Rigidbody>();
       
            }
    void Update() {
        if (SpeedEffect) {
            speedTimer += Time.deltaTime;
            if (speedTimer > 5.0f) {
                speed = 10;
                SpeedEffect = false;
                speedTimer = 0.0f;
            }
        }
    }
    void FixedUpdate() { //called before performing any physics here. 

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);


    }

    void OnTriggerEnter(Collider other)
    {
        //create a tag to the pickup prefab & the string has to match up to the tag we create
        if (other.gameObject.CompareTag("SpeedPerk"))
        {
            other.gameObject.SetActive(false);
            speed = 40;
            SpeedEffect = true;
        }
        if (other.gameObject.CompareTag("ColorPerk"))
        {
            other.gameObject.SetActive(false);
            GetComponent<Renderer>().material.color = Color.black;



        }
    }
   
}
