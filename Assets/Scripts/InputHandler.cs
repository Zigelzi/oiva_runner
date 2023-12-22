using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float _movementTouchAreaHeight = 0.25f;
    private OivaActions _actions;
    private Camera _mainCamera;
    private Movement _movement;
    private Throwing _throwing;

    private void Awake()
    {
        _actions = new OivaActions();
        _mainCamera = Camera.main;
        _movement = GetComponent<Movement>();
        _throwing = GetComponent<Throwing>();

        _actions.Player.Touch.performed += OnTouch;
        Debug.Log($"Screen height: {Screen.height}");
        Debug.Log($"Screen wifth: {Screen.width}");
    }

    private void OnEnable()
    {
        _actions.Player.Enable();
    }

    private void OnDisable()
    {
        _actions.Player.Disable();
    }

    private void OnTouch(InputAction.CallbackContext context)
    {
        if (!_mainCamera) return;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        float movementBoundary = Screen.height * _movementTouchAreaHeight;
        if (touchPosition.y >= movementBoundary)
        {
            HandleMovement(touchPosition);
        }
        else
        {
            _throwing.Interact();
        }
    }

    private void HandleMovement(Vector2 touchPosition)
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
