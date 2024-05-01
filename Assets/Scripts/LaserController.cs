using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{

    public Collider visionCollider;  // Reference to the vision cone's collider
    public Renderer visionRenderer;  // Reference to the vision cone's renderer
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableLaser(float duration)
    {
        visionCollider.enabled = false; // Disable vision collider
        visionRenderer.enabled = false; // Also disable the renderer to make the cone disappear
        StartCoroutine(ReEnableAfterDelay(duration));
    }

    IEnumerator ReEnableAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        visionCollider.enabled = true; // Re-enable vision collider
        visionRenderer.enabled = true; // Re-enable the renderer to make the cone visible again
    }
}
