using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WorldPlayer : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        private Vector2 movementInput;
        private Rigidbody2D rb;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            movementInput = new Vector2(moveX, moveY).normalized;
        }

        void FixedUpdate()
        {
            rb.linearVelocity = movementInput * moveSpeed; 
        }
    }
}

