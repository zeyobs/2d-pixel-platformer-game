using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.1f;
    public DetectionZone attackZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;
    public enum WalkableDirection { Right, Left }


    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection walkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                // flip direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }

            if (value == WalkableDirection.Right)
            {
                walkDirectionVector = Vector2.right;
            }
            else if (value == WalkableDirection.Left)
            {
                walkDirectionVector = Vector2.left;
            }


            _walkDirection = value;
        }

    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove)
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
        
    }

    private void FlipDirection()
    {
        if (walkDirection == WalkableDirection.Right)
        {
            walkDirection = WalkableDirection.Left;
        }
        else if (walkDirection == WalkableDirection.Left)
        {
            walkDirection = WalkableDirection.Right;
        }
        else
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
    }
     public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}

