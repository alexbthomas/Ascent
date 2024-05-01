using System.Collections;
using UnityEngine;

public class CloakAbility : MonoBehaviour
{
    public float cloakDuration = 10.0f;
    public int sparkCost = 20;
    public bool isCloaked = false;
    private PlayerResources playerResources;
    private PlayerAbilityManager abilityManager;
    AudioSource cloakSound;

    void Start()
    {
        playerResources = GetComponent<PlayerResources>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        if (playerResources == null)
        {
            Debug.LogError("PlayerResources component not found on the player!");
        }

        GameObject audioObject = GameObject.Find("CloakPlayer");
        if (audioObject != null)
        {
            cloakSound = audioObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Audio source object not found!");
        }
    }

    void Update()
    {
        if (abilityManager.cloakUnlocked && Input.GetKeyDown(KeyCode.C) && !isCloaked && playerResources != null)
        {
            if (playerResources.UseSpark(sparkCost))
            {
                StartCoroutine(ActivateCloak());
            }
        }
    }

    IEnumerator ActivateCloak()
    {
        isCloaked = true;
        cloakSound.Play();
        Debug.Log("Player is now cloaked.");
        yield return new WaitForSeconds(cloakDuration);
        Debug.Log("Player is no longer cloaked.");
        isCloaked = false;
    }
}
