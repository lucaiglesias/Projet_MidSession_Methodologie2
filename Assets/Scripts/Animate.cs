using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on https://www.youtube.com/watch?v=ixSN42SM3gQ&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=1
public class Animate : MonoBehaviour
{
    Animator animator;

    public float horizontal;
    public float vertical;  

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }
}
