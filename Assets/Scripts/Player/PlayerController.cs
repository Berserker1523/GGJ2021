﻿using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementSpeed;
    [SerializeField] private LayerMask solidObjectsLayer;
    [SerializeField] private GameObject lantern;
    private Lantern lanternComponent;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isMoving;
    private bool isScared;
    private Vector2 input;

    private void Start()
    {
        movementSpeed = ConfigurationUtils.PlayerMovementSpeed;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lanternComponent = lantern.GetComponent<Lantern>();

        EventManager.AddListener(EventName.DamageReceived, PlayScaredAnimation);
    }

    private void Update()
    {
        if (isScared)
        {
            return;
        }

        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //remove diagonal movement
            if (input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                FlipSprite(input.x);
                FlipLantern(input.x, input.y);

                Vector3 targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                if(IsWalkable(targetPosition))
                    StartCoroutine(Move(targetPosition));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;

        while((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPosition)
    {
        if (Physics2D.OverlapCircle(targetPosition, 0.3f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

    private void FlipSprite(float inputX)
    {
        if((!spriteRenderer.flipX && inputX > 0)  || (spriteRenderer.flipX && inputX < 0))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private void FlipLantern(float inputX, float inputY)
    {
        if(inputY == 0)
        {
            if(inputX < 0)
            {
                lantern.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (inputX > 0)
            {
                lantern.transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
        else
        {
            if (inputY < 0)
            {
                lantern.transform.eulerAngles = new Vector3(0, 0, 90);
            }

            if (inputY > 0)
            {
                lantern.transform.eulerAngles = new Vector3(0, 0, 270);
            }
        }
    }

    private void PlayScaredAnimation(int unused)
    {
        isScared = true;
        animator.SetBool("receivedDamage", true);
        lanternComponent.PauseDueToDamage();
    }

    private void ResumeMovement()
    {
        animator.SetBool("receivedDamage", false);
        lanternComponent.UnPauseDueToDamage();
        isScared = false;
    }
}
