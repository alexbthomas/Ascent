using UnityEngine;
using System.Collections;

public class GuardController : MonoBehaviour
{
    public bool isMoving = true; // Flag to determine if the guard moves
    public float speed = 2.0f;
    public Animator animator; // Animation controller for the guard
    public Collider visionCollider;  // Vision cone's collider
    public Renderer visionRenderer;  // Vision cone's renderer
    private bool isActive = true;

    // Reference to the boss controller
    public BossController boss; 

    private Vector3 offset; // The original offset relative to the boss

    void Start()
    {
        if (boss != null)
        {
            offset = transform.position - boss.transform.position; // Calculate the initial offset
        }
    }

    void Update()
    {
        if (!isActive)
        {
            animator.SetBool("isActive", false);
            return; // If the guard is inactive, exit early
        }

        animator.SetBool("isActive", true);

        if (isMoving && boss != null)
        {
            FollowBoss(); // Ensure guards follow the boss's direction and maintain offset
            animator.SetBool("isWalking", true); // Trigger walking animation
        }
        else
        {
            animator.SetBool("isWalking", false); // No walking animation if stationary
        }
    }

    private void FollowBoss()
    {
        // Ensure the guard faces the same direction as the boss
        transform.rotation = Quaternion.Slerp(transform.rotation, boss.transform.rotation, 0.1f);

        // Maintain the relative position based on the stored offset
        Vector3 targetPosition = boss.transform.position + offset; // Maintain the offset
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // Move towards the offset position
    }

    public void DisableEnemy(float duration)
    {
        isActive = false;
        visionCollider.enabled = false; // Disable vision
        visionRenderer.enabled = false; // Also disable the renderer

        StartCoroutine(ReEnableAfterDelay(duration)); // Start coroutine to re-enable after a delay
    }

    IEnumerator ReEnableAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the given duration
        isActive = true;
        visionCollider.enabled = true; // Re-enable vision
        visionRenderer.enabled = true; // Re-enable renderer
    }
}
