using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For using List

public class BossController : MonoBehaviour
{
    public bool isMoving = true; // Flag to determine if the boss moves
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;
    public int waypointIndex = 0;
    public Animator animator; // Reference to the Animator component
    
    private Vector3[] globalWaypoints; // Store waypoint positions
    private bool isActive = true; // Flag for active status

    void Start()
    {
        List<Transform> waypointList = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Point")) // Find waypoints tagged "Point"
            {
                waypointList.Add(child);
            }
        }

        // Convert to a Vector3 array
        globalWaypoints = new Vector3[waypointList.Count];
        for (int i = 0; i < waypointList.Count; i++)
        {
            globalWaypoints[i] = waypointList[i].position;
        }
    }

    void Update()
    {
        if (!isActive) return; // Only patrol if active

        Patrol(); // Handle patrolling between waypoints
    }

    private void Patrol()
    {
        if (globalWaypoints == null || globalWaypoints.Length == 0) return; // Ensure waypoints exist

        Vector3 targetWaypoint = globalWaypoints[waypointIndex];
        Vector3 directionToTarget = targetWaypoint - transform.position;
        directionToTarget.y = 0; // Keep rotation on the horizontal plane

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 10.0f) // If facing the waypoint
        {
            Vector3 moveTarget = new Vector3(targetWaypoint.x, transform.position.y, targetWaypoint.z);
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime); // Move towards the waypoint
        }

        if (Vector3.Distance(transform.position, targetWaypoint) < 0.1f) // If reached waypoint
        {
            waypointIndex = (waypointIndex + 1) % globalWaypoints.Length; // Loop through waypoints
        }
    }

    public void DisableBoss(float duration)
    {
        isActive = false; // Mark as inactive
        StartCoroutine(ReEnableAfterDelay(duration)); // Re-enable after a delay
    }

    IEnumerator ReEnableAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the given duration
        isActive = true; // Re-enable the boss after delay
    }
}
