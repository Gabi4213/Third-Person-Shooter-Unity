using System.Collections;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    public GameObject projectilePrefab; // Your projectile prefab
    public Transform shootingPoint; // The point from where the projectile will be shot
    public GameObject model; // The model to rotate
    public Animator anim;

    public ParticleSystem handParticle;
    public float projectileSpeed = 10f; // Speed of the projectile
    public float attackSpeed = 1f;

    public bool canShoot = true;

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(ShootEnumerator());
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the shooting point
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);

        // Get the camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;

        // Ensure the projectile only moves horizontally
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Rotate the model to face the shooting direction
        if (model != null)
        {
            model.transform.rotation = Quaternion.LookRotation(cameraForward);
        }

        // Add velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = cameraForward * projectileSpeed;
        }
    }

    IEnumerator ShootEnumerator()
    {
        handParticle.Play();
        anim.SetTrigger("Attack");
        canShoot = false;
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;
    }
}
