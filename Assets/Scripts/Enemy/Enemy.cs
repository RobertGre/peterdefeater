using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy attributes
    protected int health;
    protected float movementSpeed;
    protected float rotationSpeed = 5f;
    protected float detectionRangeSpotted = 12f;
    protected float detectionRangeDetected = 7f;
    protected Quaternion startRotation;
    public int damageAmount = 10;


    // Enemy components
    protected Rigidbody2D rb;

    // Reference to the player
    protected Transform player;

    // Enemy states
    protected enum EnemyState { Idle, Spotted, Detected }
    protected EnemyState currentState;

    // Idle movement variables
    protected bool isMovingUp = true;
    [SerializeField] protected float idleMovementSpeed = 5f;

    public KeyDrop keyDrop;
    public GameObject[] dropItems;

    // Initialize enemy attributes
    protected virtual void Start()
    {
        health = 100;
        movementSpeed = 10f;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = EnemyState.Idle;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Check player distance and update enemy state
        CheckPlayerDistance();

        // Perform actions based on the current state
        switch (currentState)
        {
            case EnemyState.Idle:
                MoveIdle();
                break;
            case EnemyState.Spotted:
                MoveIdle();
                RotateTowardsPlayer();
                break;
            case EnemyState.Detected:
                RotateTowardsPlayer();
                MoveTowardsPlayer();
                break;
        }
    }

    // Method to check player distance and update enemy state
    protected virtual void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRangeDetected)
        {
            // Check for line of sight to the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, distanceToPlayer, LayerMask.GetMask("Wall"));
            if (hit.collider == null)
            {
                currentState = EnemyState.Detected;
            }
            else
            {
                currentState = EnemyState.Spotted;
            }
        }
        else if (distanceToPlayer <= detectionRangeSpotted)
        {
            currentState = EnemyState.Spotted;
        }
        else
        {
            currentState = EnemyState.Idle;
        }

        // If a raycast hits a wall, sets the state back to Idle to prevent spotting the player through obstacles
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, player.position - transform.position, distanceToPlayer, LayerMask.GetMask("Wall"));
        if (wallHit.collider != null)
        {
            currentState = EnemyState.Idle;
        }
    }


    // Method for idle movement (up and down)
    protected virtual void MoveIdle()
    {
        Vector2 movement = isMovingUp ? Vector2.up : Vector2.down;

        // Check for collisions with a obstacle
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, 0.5f, LayerMask.GetMask("Wall"));
        if (hit.collider != null)
        {
            // Change direction if colliding with an obstacle
            isMovingUp = !isMovingUp;
        }
        else
        {
            rb.MovePosition(rb.position + movement * idleMovementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Method to rotate enemy towards the player
    protected virtual void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    // Method to move enemy towards player
    protected virtual void MoveTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        direction.Normalize();

        // Check for obstacles in the player's path
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, movementSpeed * Time.deltaTime, LayerMask.GetMask("Wall"));
        if (hit.collider == null)
        {
            rb.MovePosition((Vector2)transform.position + direction * movementSpeed * Time.deltaTime);
        }
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

    protected virtual void Die()
    {
        // Generate a random index to choose a drop item
        int randomIndex = Random.Range(0, dropItems.Length);

        // Instantiate the randomly chosen drop item
        Instantiate(dropItems[randomIndex], transform.position, Quaternion.identity);

        if (keyDrop != null)
        {
            // Trigger key drop
            keyDrop.DropKey();
        }


        // Destroy the enemy game object
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Draw Gizmos for detected range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRangeDetected);

        // Draw gizmos for spotted range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRangeSpotted);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player
            collision.gameObject.GetComponent<Player>().TakeDamage(damageAmount);
        }
    }
}


