using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation
    public Animator anim;
    public CapsuleCollider capsuleCollider;
    public ParticleSystem dustTrail;

    void Update()
    {
        // Check if the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            // Rotate the player model along the X-axis
            anim.SetBool("Rolling", true);

            //particle
            dustTrail.Play();

            //collider stuff
            capsuleCollider.center.Set(0, 0.13f, 0);
            capsuleCollider.radius = 0.5f;
            capsuleCollider.height = 1.14f;
        }
        else
        {
            anim.SetBool("Rolling", false);

            //collider stuff
            capsuleCollider.center.Set(0, 0, 0);
            capsuleCollider.radius = 0.5f;
            capsuleCollider.height = 1.9f;
        }
    }
}
