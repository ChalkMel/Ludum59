using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private Actions _inputSystemActions;
    
    private void Awake()
    {
        _inputSystemActions = new Actions();
    }

    private void OnEnable()
    {
        Bind();
        _inputSystemActions.Enable();
    }

    private void Bind()
    {
        _inputSystemActions.Move.Jump.performed += OnJump;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = _inputSystemActions.Move.Move.ReadValue<Vector2>();
        playerMovement.Move(direction); 
    }
    

    private void OnJump(InputAction.CallbackContext obj)
    {
        playerMovement.Jump();
    }

    private void Expose()
    {
        _inputSystemActions.Move.Jump.performed -= OnJump;
    }

    private void OnDisable()
    {
        _inputSystemActions.Disable();
        Expose();
    }
}