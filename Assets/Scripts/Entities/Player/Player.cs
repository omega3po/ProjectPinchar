using System.Grid;
using UnityEngine;
using Entities.Base;

namespace Entities.Player
{
    public class Player : Character
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private bool hasAutoFireItem = false;
        [SerializeField] private float attackCooldown = 0.25f;
        [SerializeField] private LayerMask obstacleLayer;
        private float lastAttackTime;
        private Vector2Int facingDirection = Vector2Int.right;
        
        protected override void Start()
        {
            base.Start();
            if (GridManager.Instance != null)
            {
                gridX = -GridManager.Instance.Width / 2;
                gridY = 0;
                transform.position = new Vector3(gridX, gridY, 0);
            }
        }
        
        void Update()
        {
            if (CurrentHealth <= 0) return;
            HandleMovementInput();
            HandleAttackInput();
        }

        private void HandleMovementInput()
        {
            int targetX = gridX;
            int targetY = gridY;
            Vector2Int inputDir = Vector2Int.zero;
            
            if (Input.GetKeyDown(KeyCode.W)) inputDir = Vector2Int.up;
            else if (Input.GetKeyDown(KeyCode.S)) inputDir = Vector2Int.down;
            else if (Input.GetKeyDown(KeyCode.A)) inputDir = Vector2Int.left;
            else if (Input.GetKeyDown(KeyCode.D)) inputDir = Vector2Int.right;
            
            if (inputDir == Vector2Int.zero) return;
            
            facingDirection = inputDir;
            
            targetX += inputDir.x;
            targetY += inputDir.y;
            
            if (GridManager.Instance != null && !GridManager.Instance.IsValidPosition(targetX, targetY))
            {
                return;
            }

            if (IsOccupied(targetX, targetY))
            {
                return;
            }
            
            MoveToGrid(targetX, targetY);
        }

        private void HandleAttackInput()
        {
            if (Time.time < lastAttackTime + attackCooldown)
            {
                return;
            }

            bool isAttacking = false;

            if (hasAutoFireItem)
            {
                isAttacking = Input.GetKey(KeyCode.Space);
            }
            else
            {
                isAttacking = Input.GetKeyDown(KeyCode.Space);
            }

            if (isAttacking)
            {
                PerformAttack();
                lastAttackTime = Time.time;
            }
        }

        private void MoveToGrid(int targetX, int targetY)
        {
            gridX = targetX;
            gridY = targetY;
            transform.position = new Vector3(targetX, targetY, 0);
        }

        private void PerformAttack()
        {
            int targetX = gridX + facingDirection.x;
            int targetY = gridY + facingDirection.y;

            Vector3 targetPos = new Vector3(targetX, targetY, 0);
            
            Debug.Log("dkd");
            
            Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.3f, obstacleLayer);
            if (hit != null)
            {
                IDamageable target = hit.GetComponent<IDamageable>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }

        private bool IsOccupied(int targetX, int targetY)
        {
            Vector3 targetPos = new Vector3(targetX, targetY, 0);
            Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.3f, obstacleLayer);
            
            return hit != null; 
        }
        
        protected override void Die()
        {
            Debug.Log("Player Died... Game Over");
            gameObject.SetActive(false); 
        }
    }
}

