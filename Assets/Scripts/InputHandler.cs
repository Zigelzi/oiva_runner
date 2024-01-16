using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
public class InputHandler : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float _movementTouchAreaHeight = 0.25f;

    private OivaActions _actions;
    private Camera _mainCamera;
    private Movement _movement;
    private InputAction _movePositionInput;
    float _movementBoundary = -1f;
    private Throwing _throwing;

    private void Awake()
    {
        _actions = new OivaActions();
        _mainCamera = Camera.main;
        _movement = GetComponent<Movement>();
        _throwing = GetComponent<Throwing>();
        _movePositionInput = _actions.Player.MovePosition;

        _actions.Player.Throw.performed += OnThrow;
        _actions.Player.Move.started += OnMoveStart;
        _movementBoundary = Screen.height * _movementTouchAreaHeight;

    }

    private void OnEnable()
    {
        _actions.Player.Enable();
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        _actions.Player.Disable();
        EnhancedTouchSupport.Disable();
    }

    private void OnThrow(InputAction.CallbackContext context)
    {
        if (!_mainCamera) return;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();


        if (touchPosition.y >= _movementBoundary) return;

        _throwing.Interact();
    }

    private void OnMoveStart(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = _movePositionInput.ReadValue<Vector2>();
        HandleSidewaysMovement(touchPosition);
    }

    private void HandleSidewaysMovement(Vector2 touchPosition)
    {
        if (!_movement) return;

        if (touchPosition.x >= Screen.width * 0.5f)
        {
            _movement.Move(true);
        }
        else
        {
            _movement.Move(false);
        }
    }
}
