using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject frogModel; // The 3D model of the frog
    public float moveSpeed = 3.0f; // Speed of movement
    public float rotateSpeed = 5.0f;
    public float roamRadius = 10.0f; // Radius within which the frog can roam
    public float minStopDuration = 1.0f; // Minimum duration to stop
    public float maxStopDuration = 3.0f; // Maximum duration to stop
    public float stopInterval = 5.0f; // Interval between stops

    private Vector3 targetPosition;
    private Rigidbody rb;
    private float stopTimer;
    private bool isStopped = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewTargetPosition();
        stopTimer = stopInterval;
    }

    void Update()
    {
        if (PlayerMovementAdvanced.gamOver)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {

            HandleStop();
            if (!isStopped)
            {
                Roam();
                RotateTowardsMovementDirection();
            }
        }
    }

    void Roam()
    {
        Vector3 moveDirection = targetPosition - transform.position;
        if (moveDirection.magnitude > 0.1f)
        {
            rb.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            SetNewTargetPosition();
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

    void SetNewTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y; // Keep the frog on the same plane
        targetPosition = randomDirection;
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
