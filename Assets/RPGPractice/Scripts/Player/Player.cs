using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    public class Player : SingletonMonobehaviour<Player>
    {

        public AnimationOverrides animationOverrides;

        //Movement Parametors
        public float xInput;
        public float yInput;
        public bool isWalking;
        public bool isRunning;
        public bool isIdle;
        public bool isCarrying=false;
        public bool isUsingToolRight;
        public bool isUsingToolLeft;
        public bool isUsingToolUp;
        public bool isUsingToolDown;
        public bool isLiftingToolRight;
        public bool isLiftingToolLeft;
        public bool isLiftingToolUp;
        public bool isLiftingToolDown;
        public bool isPickingRight;
        public bool isPickingLeft;
        public bool isPickingUp;
        public bool isPickingDown;
        public bool isSwingingToolRight;
        public bool isSwingingToolLeft;
        public bool isSwingingToolUp;
        public bool isSwingingToolDown;
        public bool idleUp;
        public bool idleDown;
        public bool idleLeft;
        public bool idleRight;
        public ToolEffect toolEffect = ToolEffect.none;


        private Camera mainCamera;

        private Rigidbody2D rigidBody2D;
        private Direction playerDirection;


        private List<CharacterAttibute> characterAttibutesCustomisationList;

        private float movementSpeed;


        [Tooltip("should be populated in the prefab with the equipped item sprite renderer")]
        [SerializeField] private SpriteRenderer equippedItemSpriteRender = null;



        private bool _playerInputIsDisabled = false;
        public bool PlayerInputIsDisabled { get => _playerInputIsDisabled; set => _playerInputIsDisabled = value; }


        protected override void Awake()
        {
            base.Awake();
            rigidBody2D = GetComponent<Rigidbody2D>();

            mainCamera = Camera.main;
        }

        private void Update()
        {
            #region PlayerInput

            if (!PlayerInputIsDisabled)
            {
                ResetAnimationTriggers();

                PlayerMovementInput();
                PlayerWalkInput();

                //Send event to listeners  for player movement input
                EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying,
                toolEffect,
                    isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                    isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
                    isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
                    isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
                    false, false, false, false);
            }
           
            #endregion
        }


        private void FixedUpdate()
        {
            PlayerMovement();
        }

        private void PlayerMovement()
        {
            //Physics System movement
            Vector2 move = new Vector2(xInput * movementSpeed * Time.deltaTime, yInput * movementSpeed * Time.deltaTime);
            rigidBody2D.MovePosition(rigidBody2D.position + move);
        }

        private void ResetAnimationTriggers()
        {
            isPickingRight = false;
            isPickingLeft = false;
            isPickingUp = false;
            isPickingDown = false;

            isUsingToolRight = false;
            isUsingToolLeft = false;
            isUsingToolUp = false;
            isUsingToolDown = false;

            isLiftingToolRight = false;
            isLiftingToolLeft = false;
            isLiftingToolUp = false;
            isLiftingToolDown = false;
            
            isSwingingToolRight = false;
            isSwingingToolLeft = false;
            isSwingingToolUp = false;
            isSwingingToolDown = false;

            toolEffect = ToolEffect.none;
        }


        private void PlayerMovementInput()
        {
            yInput = UnityEngine.Input.GetAxisRaw("Vertical");

            xInput = UnityEngine.Input.GetAxisRaw("Horizontal");

            if (yInput != 0 && xInput != 0)
            {
                xInput *= 0.71f;
                yInput *= 0.71f;
            }

            if (xInput != 0 || yInput != 0)
            {
                isRunning = true;
                isWalking = false;
                isIdle = false;
                movementSpeed = Settings.runningSpeed;

                if (xInput < 0)
                {
                    playerDirection = Direction.left;
                }
                else if (xInput > 0)
                {
                    playerDirection = Direction.right;
                }
                else if (yInput < 0)
                {
                    playerDirection = Direction.down;
                }
                else if (yInput > 0)
                {
                    playerDirection = Direction.up;
                }
            }
            else if (xInput == 0 && yInput == 0)
            {
                isRunning = false;
                isWalking = false;
                isIdle = true;
            }

        }


        private void PlayerWalkInput()
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                isRunning = false;
                isWalking = true;
                isIdle = false;
                movementSpeed = Settings.walkingSpeed;
            }
            else
            {
                isRunning = true;
                isWalking = false;
                isIdle = false;
                movementSpeed = Settings.runningSpeed;
            }
        }


        public void DisablePlayerInputAndMovement()
        {
            DisablePlayerInput();
            ResetMovement();

            EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying,
               toolEffect,
                   isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                   isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
                   isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
                   isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
                   false, false, false, false);
        }

        private void ResetMovement()
        {
            xInput = 0f;
            yInput = 0f;
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }

        public void DisablePlayerInput()
        {
            PlayerInputIsDisabled = true;
        }



        public void EnablePlayerInputs()
        {
            PlayerInputIsDisabled = false;
        }


        //get player viewport position
        public Vector3 GetPlayerViewportPosition()
        {
            return mainCamera.WorldToViewportPoint(transform.position);
        }
    }
}
