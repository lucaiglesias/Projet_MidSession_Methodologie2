using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    GameObject targetGameObject;
    [SerializeField] float speed;

    Character targetCharacter;

    Rigidbody2D rgdbd2d;

    [SerializeField] int hp = 1;
    [SerializeField] int enemyAttack = 1;
    [SerializeField] int experience_points = 5;

    private void Awake()
    {
        rgdbd2d = GetComponent<Rigidbody2D>();
        targetGameObject = targetDestination.gameObject;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgdbd2d.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(enemyAttack);
        Debug.Log("Attacking the character! " +targetCharacter.currentHp);

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            this.gameObject.SetActive(false);
            targetCharacter.LevelUp(experience_points);
        }
    }
}
