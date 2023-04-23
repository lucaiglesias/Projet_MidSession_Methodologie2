using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //based on https://www.youtube.com/watch?v=p7FnfMRQ6Ec&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=3

    [SerializeField] float speed;

    Rigidbody2D rgdbd2d;

    [SerializeField] int hp = 1;
    [SerializeField] int enemyAttack = 1;
    [SerializeField] int experience_points = 5;
    [SerializeField] Vector3 direction;

    //public static Enemy Instance { get; private set; }



    private void Awake()
    {
        //Instance = this;
        rgdbd2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        direction = (Character.Instance.transform.position - transform.position).normalized;
        rgdbd2d.velocity = direction * speed * CountDown.Instance.pause;
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Character>())
        {

            Attack();
        }

    }

    private void Attack()
    {

        Character.Instance.TakeDamage(enemyAttack);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        DamageReaction();


        if (hp <= 0)
        {
            gameObject.SetActive(false);
            Character.Instance.LevelUp(experience_points);
            Character.Instance.MonstersKilled++;
            
            //Observer
            //Subject.Instance.Notify(this);
        }
    }

    IEnumerator DamageReaction()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //speed *= -1;
        yield return new WaitForSeconds(.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        //speed *= 1;
    }
}
