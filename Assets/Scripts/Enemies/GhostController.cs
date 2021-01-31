using System;
using UnityEngine;

public enum MovementSide
{
    Down = -1,
    Up = 1,
    Left = -2,
    Right = 2,
    Player
}

[RequireComponent(typeof(GhostMovement))]
public class GhostController : MonoBehaviour
{
    /// <summary>
    /// Movement pattern that a ghost is going to follow 
    /// </summary>
    [SerializeField] private MovementSide[] movementSide;
    
    private GhostMovement ghostMovement;
    private Vector3 currentMovementDirection;
    private int currentDirection;
    
    private bool intersectionTouched;
    private bool added;

    private SpawnManager spawnManager;
    
    private void Awake()
    {
        ghostMovement = GetComponent<GhostMovement>();
        currentMovementDirection = EnumUtils.ConvertToVector(movementSide[currentDirection]);
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    
    private void FixedUpdate()
    {
        if (CanMove())
        {
            ghostMovement.Move(currentMovementDirection);
        }

        if (intersectionTouched)
        {
            currentMovementDirection = EnumUtils.ConvertToVector(movementSide[currentDirection]);
            intersectionTouched = false;
        }
    }

    private bool CanMove()
    {
        var raycast = Physics2D.Raycast(transform.position, transform.right, 1);

        return !raycast.collider.CompareTag("Wall");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.bounds.Contains(GetComponent<Collider2D>().bounds.min) && other.bounds.Contains(GetComponent<Collider2D>().bounds.max))
        {
            intersectionTouched = other.gameObject.CompareTag("Intersection");
            if (intersectionTouched && !added)
            {
                added = true;
                
                if (++currentDirection >= movementSide.Length)
                {
                    currentDirection = 0;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Intersection"))
        {
            added = false;    
        }
    }

    public void GhostKilled(GameObject killedGhost)
    {
        spawnManager.KillGhost(killedGhost);
    }
}