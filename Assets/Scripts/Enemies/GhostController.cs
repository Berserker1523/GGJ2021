using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private LayerMask ignoreCollision;
    [SerializeField] private Collider2D decorationCollider;
    
    private GhostMovement ghostMovement;
    private Vector3 currentMovementDirection;
    private int currentDirection;

    private bool intersectionTouched;
    private bool added;

    private SpawnManager spawnManager;

    private void OnEnable()
    {
        Physics2D.IgnoreCollision(decorationCollider, GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GameObject.Find("SolidObjects").GetComponent<CompositeCollider2D>(), GetComponent<Collider2D>());
    }

    private void Awake()
    {
        decorationCollider = GameObject.Find("deco").GetComponent<CompositeCollider2D>();
        ghostMovement = GetComponent<GhostMovement>();
        currentMovementDirection = EnumUtils.ConvertToVector(movementSide[currentDirection]);
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void FixedUpdate()
    {
        if (CanMove())
        {
            Debug.Log("Hola");
            Debug.DrawRay(transform.position, currentMovementDirection, Color.red);
            ghostMovement.Move(currentMovementDirection);
        }
        else
        {
            if (!added)
            {
                NextMovementInstruction();
            }

            added = false;
            currentMovementDirection = EnumUtils.ConvertToVector(movementSide[currentDirection]);
            ghostMovement.Move(currentMovementDirection);
        }

        if (intersectionTouched)
        {
            currentMovementDirection = EnumUtils.ConvertToVector(movementSide[currentDirection]);
            intersectionTouched = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.bounds.Contains(GetComponent<Collider2D>().bounds.min) &&
            other.bounds.Contains(GetComponent<Collider2D>().bounds.max))
        {
            intersectionTouched = other.gameObject.CompareTag("Intersection");
            
            if (intersectionTouched && !added)
            {
                NextMovementInstruction();
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
        //Play cool animation
        spawnManager.KillGhost(killedGhost);
    }

    private bool CanMove()
    {
        var raycast = Physics2D.Raycast(transform.position, currentMovementDirection, 1,ignoreCollision);

        if (raycast.collider == null)
        {
            return true;
        }
        
        Debug.Log(raycast.collider.name);
        return !raycast.collider.CompareTag("Wall");
    }

    private void NextMovementInstruction()
    {
        added = true;

        currentDirection = Random.Range(0, movementSide.Length);

        if (++currentDirection >= movementSide.Length)
        {
            currentDirection = 0;
        }
    }
}