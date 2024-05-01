using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public bool isMoving = true; // Flag to determine if the enemy moves
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;
    private Vector3[] globalWaypoints;
    private int waypointIndex = 0;
    public Animator animator; // Reference to the Animator component
    public Collider visionCollider;  // Reference to the vision cone's collider
    public Renderer visionRenderer;  // Reference to the vision cone's renderer
    private bool isActive = true;

    void Start()
    {
        Transform[] waypoints = new Transform[transform.childCount];
        globalWaypoints = new Vector3[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
            globalWaypoints[i] = waypoints[i].position;
        }
    }

    void Update()
    {
        if (!isActive){
            animator.SetBool("isActive", false);
        }
        else{
            animator.SetBool("isActive", true);
        }

        if (isMoving && isActive)
        {
            Patrol(); // Only patrol if the enemy should move
            animator.SetBool("isWalking", true); // Trigger walking animation
        }
        else
        {
            animator.SetBool("isWalking", false); // No animation if stationary
        }
    }

    private void Patrol()
    {
        Vector3 targetWaypoint = globalWaypoints[waypointIndex];
        Vector3 directionToTarget = targetWaypoint - transform.position;
        directionToTarget.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 10.0f)
        {
            Vector3 moveTarget = new Vector3(targetWaypoint.x, transform.position.y, targetWaypoint.z);
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetWaypoint.x, 0, targetWaypoint.z)) < 1.0f)
        {
            waypointIndex = (waypointIndex + 1) % globalWaypoints.Length;
        }
    }

    public void DisableEnemy(float duration)
    {
        isActive = false;
        visionCollider.enabled = false; // Disable vision collider
        visionRenderer.enabled = false; // Also disable the renderer to make the cone disappear
        StartCoroutine(ReEnableAfterDelay(duration));
    }

    IEnumerator ReEnableAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        isActive = true;
        visionCollider.enabled = true; // Re-enable vision collider
        visionRenderer.enabled = true; // Re-enable the renderer to make the cone visible again
    }
}
