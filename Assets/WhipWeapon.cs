using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack = 4f;
    float timer;

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;
    //[SerializeField] GameObject upWhipObject;
    //[SerializeField] GameObject downWhipObject;

    PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();

        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
        timer = timeToAttack;

        if(playerMove.lastHorizontalVector > 0)
        {
            rightWhipObject.SetActive(true);
        }
        else
        {
            leftWhipObject.SetActive(true);
        }
        //else
        //{
        //    rightWhipObject.SetActive(false);
        //}

        //if(playerMove.movementVector.x < -1)
        //{
        //    leftWhipObject.SetActive(true);
        //}
        //else
        //{
        //    leftWhipObject.SetActive(false);
        //}

        //if(playerMove.movementVector.y > 1)
        //{
        //    upWhipObject.SetActive(true);
        //}
        //else
        //{
        //    upWhipObject.SetActive(false);
        //}

        //if(playerMove.movementVector.y < -1)
        //{
        //    downWhipObject.SetActive(true);
        //}
        //else
        //{
        //    downWhipObject.SetActive(false);
        //}
    }
}
