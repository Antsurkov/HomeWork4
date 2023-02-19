using UnityEngine;
namespace HomeWork4
{
    public class InputManager : MonoBehaviour
    {
        private Vector2 playerInput;
        public Vector2 PlayerInput => playerInput;
        
        private bool jumpPressed;
        private float mouseDeltaX = 0f;
        private float mouseDeltaY = 0f;
        public bool JumpPressed => jumpPressed;
        public float MouseDeltaX => mouseDeltaX;
        public float MouseDeltaY => mouseDeltaY;
        
        private void Update()
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            jumpPressed = Input.GetButtonDown("Jump");
            mouseDeltaX = Input.GetAxis("Mouse X");
            mouseDeltaY = Input.GetAxis("Mouse Y");
        }
    }
}
