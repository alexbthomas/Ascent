using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Player movement properties
    public float movementSpeed = 5.0f;
    public float sprintSpeed = 10.0f; // Speed while sprinting
    public float crouchSpeed = 2.5f; // Reduced speed while crouching
    public float jumpForce = 5.0f;
    public float standHeight = 2.0f; // Regular height
    public float crouchHeight = 1.0f; // Height when crouching
    
    // Takedown properties
    public LayerMask enemyLayer;
    public Transform checkPosition;
    public float checkRadius = 2f;
    
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private bool isCrouching = false;
    private bool isSprinting = false;
    private Vector3 startPosition;
    private PlayerResources playerResources; // Script managing player's resources
    private PlayerAbilityManager abilityManager;

    private float xRotation = 0f;
    private float yRotation = 0f;
    public Transform playerBody;

    // Cooldown management
    public float takedownCooldown = 5.0f; // 5-second cooldown
    private bool isTakedownOnCooldown = false; // Cooldown flag

    AudioSource takedownSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerResources = GetComponent<PlayerResources>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        startPosition = transform.position; // Store the initial position

        GameObject audioObject = GameObject.Find("TakedownPlayer");
        if (audioObject != null)
        {
            takedownSound = audioObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Audio source object not found!");
        }
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumping();
        HandleCrouching();

        if (abilityManager.silentTakedownUnlocked && isCrouching && Input.GetKeyDown(KeyCode.F))
        {
            PerformSilentTakedown();
        }
    }

    public void ResetToStartPosition()
    {
        transform.position = startPosition; // Reset position to the start
        Cursor.lockState = CursorLockMode.Locked;
        
        xRotation = 0f;
        yRotation = 0f;

        // Apply rotations and output to console
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        Debug.Log("Initial xRotation: " + xRotation);
        Debug.Log("Initial yRotation: " + yRotation);
    }

    void HandleMovementInput()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isCrouching ? crouchSpeed : (isSprinting ? sprintSpeed : movementSpeed);
        float x = Input.GetAxis("Horizontal") * currentSpeed;
        float z = Input.GetAxis("Vertical") * currentSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * Time.deltaTime;
    }

    void HandleJumping()
    {
        if(Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleCrouching()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            capsuleCollider.height = crouchHeight;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            capsuleCollider.height = standHeight;
        }
    }

    void PerformSilentTakedown()
    {
        if(!isTakedownOnCooldown){
            isTakedownOnCooldown = true; // Start the cooldown
            StartCoroutine(TakedownCooldownRoutine()); // Cooldown timer

            Collider[] hitEnemies = Physics.OverlapSphere(checkPosition.position, checkRadius, enemyLayer);
            foreach (var enemy in hitEnemies)
            {
                Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
                float dotProduct = Vector3.Dot(transform.forward, directionToEnemy);
                if (dotProduct > 0.8) // Only perform takedown if behind the enemy
                {
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        enemyController.DisableEnemy(5); // Disable the enemy for 5 seconds
                        playerResources.AddSpark(10); // Award sparks
                        takedownSound.Play();
                        break; // One takedown at a time
                    }
                }
            }
        }
    }

    IEnumerator TakedownCooldownRoutine()
    {
        yield return new WaitForSeconds(takedownCooldown); // 5-second cooldown
        isTakedownOnCooldown = false; // Cooldown over
    }
}
