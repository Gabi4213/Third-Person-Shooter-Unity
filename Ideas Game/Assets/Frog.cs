using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public GameObject frogModel; // The 3D model of the frog
    public float moveSpeed = 3.0f; // Speed of movement
    public float rotateSpeed = 5.0f;
    public float jumpForce = 5.0f; // Force of jump
    public float jumpInterval = 3.0f; // Interval between jumps
    public float roamRadius = 10.0f; // Radius within which the frog can roam
    public float minStopDuration = 1.0f; // Minimum duration to stop
    public float maxStopDuration = 3.0f; // Maximum duration to stop
    public float stopInterval = 5.0f; // Interval between stops

    private Vector3 targetPosition;
    private Rigidbody rb;
    private float jumpTimer;
    private float stopTimer;
    private bool isStopped = false;
    public bool grounded;
    public Animator anim;
    public GameObject laser;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewTargetPosition();
        jumpTimer = jumpInterval;
        stopTimer = stopInterval;
    }

    void Update()
    {
        HandleStop();
        if (!isStopped)
        {
            anim.SetBool("Attack", false);
            laser.SetActive(false);
            Roam();
            HandleJump();
            RotateTowardsMovementDirection();
        }


        if (isStopped)
        {
            anim.SetBool("Attack", true);
            laser.SetActive(true);
            RotateTowardsPlayer();
        }

        anim.SetFloat("Speed", rb.velocity.magnitude);
    }

    void Roam()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    void HandleJump()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0 && grounded)
        {
            Jump();
            jumpTimer = jumpInterval;
        }
    }

    void HandleStop()
    {
        stopTimer -= Time.deltaTime;

        if (stopTimer <= 0)
        {
            if (!isStopped)
            {
                isStopped = true;
                float stopDuration = Random.Range(minStopDuration, maxStopDuration);
                StartCoroutine(StopForDuration(stopDuration));
            }
            stopTimer = stopInterval + Random.Range(minStopDuration, maxStopDuration);
        }
    }

    IEnumerator StopForDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void SetNewTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y; // Keep the frog on the same plane
        targetPosition = randomDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    void RotateTowardsMovementDirection()
    {
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            Vector3 eulerRotation = rotation.eulerAngles;
            eulerRotation.x = 0; // Keep x rotation at 0
            eulerRotation.z = 0; // Keep z rotation at 0
            Quaternion fixedRotation = Quaternion.Euler(eulerRotation);
            frogModel.transform.rotation = Quaternion.Slerp(frogModel.transform.rotation, fixedRotation, Time.deltaTime * moveSpeed);
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            Vector3 eulerRotation = rotation.eulerAngles;
            eulerRotation.x = 0; // Keep x rotation at 0
            eulerRotation.z = 0; // Keep z rotation at 0
            Quaternion fixedRotation = Quaternion.Euler(eulerRotation);
            frogModel.transform.rotation = Quaternion.Slerp(frogModel.transform.rotation, fixedRotation, Time.deltaTime * rotateSpeed);
        }
    }
}
