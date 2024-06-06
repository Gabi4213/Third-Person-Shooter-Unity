using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    public float attackTimer;
    public Animator anim;
    private bool canAttack = true;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");

        canAttack = false;
        yield return new WaitForSeconds(attackTimer);
        canAttack = true;
    }

}
