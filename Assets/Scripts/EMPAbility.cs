using System.Collections;
using UnityEngine;

public class EMPAbility : MonoBehaviour
{
    public float empDuration = 5f;
    public float cooldown = 20f;  
    public int sparkCost = 30;      
    private float cooldownTimer = 0;
    private PlayerResources playerResources;
    private PlayerAbilityManager abilityManager;
    AudioSource EMPSound;

    void Start()
    {
        playerResources = GetComponent<PlayerResources>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        if (playerResources == null)
        {
            Debug.LogError("PlayerResources component not found on the player!");
        }

        GameObject audioObject = GameObject.Find("EMPPlayer");
        if (audioObject != null)
        {
            EMPSound = audioObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Audio source object not found!");
        }
    }

    void Update()
    {
        // Update the cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Check for EMP activation
        if (abilityManager.empUnlocked && Input.GetKeyDown(KeyCode.E) && cooldownTimer <= 0 && playerResources != null)
        {
            if (playerResources.UseSpark(sparkCost))
            {
                StartCoroutine(ActivateEMP());
            }
        }
    }

    IEnumerator ActivateEMP()
    {
        Debug.Log("EMP activated!");
        EMPSound.Play();
        // Find all enemies and disable them
        foreach (var enemy in FindObjectsOfType<EnemyController>())
        {
            enemy.DisableEnemy(empDuration);
        }

        foreach (var laser in FindObjectsOfType<LaserController>())
        {
            laser.DisableLaser(empDuration);
        }

        // Start cooldown timer
        cooldownTimer = cooldown;

        yield return new WaitForSeconds(empDuration);

        // Optionally, re-enable enemies here if they automatically recover
        Debug.Log("EMP effect ended.");
    }
}
