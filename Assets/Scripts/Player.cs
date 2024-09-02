using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player attributes
    private int health;
    [SerializeField]private float movementSpeed = 35f;
    private float rotationSpeed = 10f;
    private LayerMask obstacleLayer; // Layer mask for obstacles
    private Animator anim;
    [SerializeField] private Canvas deathCanvas;
    [SerializeField] private Canvas hudCanvas;

    // Player input variables
    private float horizontalInput;
    private float verticalInput;

    // Player component
    private Rigidbody2D rb;

    // Properties for accessing health and if the player has the key
    public int Health { get { return health; } }
    public bool HasKey { get; set; } = false;
    public GameObject KeyObject;

    private void Start()
    {
        health = 100;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        obstacleLayer = LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * movementSpeed;

        // Using raycast to detect obstacles (walls)
        float raycastLength = movement.magnitude * Time.deltaTime * 1.5f; // Adjust raycast length
        RaycastHit2D hit = Physics2D.Raycast(rb.position, movement, raycastLength, obstacleLayer);
        if (hit.collider == null)
        {
            // If no wall is detected then it moves the player
            rb.MovePosition(rb.position + movement * Time.deltaTime);
        }

        // Rotates the player towards cursor and updates the player animations
        RotateToCursor();
        ControlAnimation();

        if (KeyObject != null)
        {
            if (!HasKey)
            {
                HasKey = false;
                KeyObject.SetActive(false);
            }
            else
            {
                HasKey = true;
                KeyObject.SetActive(true);
            }
        }
    }


    private void ControlAnimation()
    {
        bool isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;

        // Updates animator parameters based on player state
        anim.SetBool("Walk", isMoving);
        anim.SetBool("Idle", !isMoving);

        bool isShooting = Input.GetButton("Fire1");

        // Updates animator parameter for shooting
        anim.SetBool("Shooting", isShooting);
    }

    // Method to rotate player to face cursor
    private void RotateToCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePos - playerPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    // Method for healing
    public void Heal(int amount)
    {
        health += amount;
    }

    // Method for taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Method for players death
    private void Die()
    {
        if(health <= 0)
        {
         Time.timeScale = 0f;
         deathCanvas.gameObject.SetActive(true);
        }
    }
}


