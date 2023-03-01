using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on https://www.youtube.com/watch?v=ixSN42SM3gQ&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=1
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [HideInInspector] public Vector3 movementVector;

    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;

    [SerializeField] float speed = 3f;

    Animate animate;
        
    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        if(movementVector.x != 0)
        {
            lastHorizontalVector = movementVector.x;
        }
        if(movementVector.y != 0)
        {
            lastVerticalVector = movementVector.y;
        }

        movementVector *= speed * CountDown.Instance.pause;

        animate.horizontal = movementVector.x;
        animate.vertical = movementVector.y;


        rgbd2d.velocity = movementVector;
    }
}
