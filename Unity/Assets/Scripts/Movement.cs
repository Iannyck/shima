using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float rotateSpeed = 6f;
    private bool init = false;

    Vector3 movement;
    Rigidbody playerRigidbody;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

       //  if (Input.GetButtonDown("w") || Input.GetButtonDown("a") || Input.GetButtonDown("s") || Input.GetButtonDown("d"))
       if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        Move(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);

        Turning();
    }

    void Turning()
    {
        float faceDirection = Input.GetAxisRaw("Horizontal") * -1;
        float faceOrientation = Input.GetAxisRaw("Vertical");
        transform.forward = new Vector3(faceOrientation, 0, faceDirection);
        
    }
}

 

