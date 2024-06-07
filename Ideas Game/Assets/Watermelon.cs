using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : MonoBehaviour
{
    public GameObject projectilePrefab; // Your projectile prefab
    public Transform shootingPoint; // The point from where the projectile will be shot
    public GameObject model; // The model to rotate
    public Animator anim;

    public float projectileSpeed = 10f; // Speed of the projectile
    public float attackSpeed = 1f;
    public float throwAngle = 45f; // Angle at which the projectile is thrown

    public bool canShoot = true;

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(ShootEnumerator());
        }
    }

    IEnumerator ShootEnumerator()
    {
        anim.SetTrigger("Attack");
        canShoot = false;
        Shoot();
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;
    }

    void Shoot()
    {
        // Instantiate the projectile at the shooting point
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);

        // Calculate the direction to throw the projectile
        Vector3 throwDirection = CalculateThrowDirection();

        // Rotate the model to face the shooting direction
        if (model != null)
        {
            model.transform.rotation = Quaternion.LookRotation(new Vector3(throwDirection.x, 0.0f, throwDirection.z));
        }

        // Add velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = throwDirection * projectileSpeed;
        }
    }

    Vector3 CalculateThrowDirection()
    {
        // Get the camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;

        // Calculate the horizontal direction
        Vector3 horizontalDirection = cameraForward;
        horizontalDirection.y = 0f;
        horizontalDirection.Normalize();

        // Calculate the vertical direction
        Vector3 verticalDirection = Vector3.up;

        // Combine horizontal and vertical directions with the throw angle
        Vector3 throwDirection = Quaternion.AngleAxis(throwAngle, Vector3.Cross(horizontalDirection, Vector3.up)) * horizontalDirection;
        throwDirection += verticalDirection * Mathf.Tan(throwAngle * Mathf.Deg2Rad);

        return throwDirection.normalized;
    }
}
