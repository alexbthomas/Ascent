using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    AudioSource detectedSound;

    

void Start()
{
    GameObject audioObject = GameObject.Find("SpottedPlayer");
    if (audioObject != null)
    {
        detectedSound = audioObject.GetComponent<AudioSource>();
    }
    else
    {
        Debug.LogError("Audio source object not found!");
    }
}


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name); // To check if the trigger is working

        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<CloakAbility>().isCloaked == false){
                Debug.Log("Player detected"); // To confirm it's the player
                detectedSound.Play();
                other.GetComponent<PlayerController>().ResetToStartPosition();    
            }
            
        }
    }
}