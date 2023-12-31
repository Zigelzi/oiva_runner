using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class InputHandler : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float _movementTouchAreaHeight = 0.25f;

    private OivaActions _actions;
    private Camera _mainCamera;
    private Movement _movement;
    float _movementBoundary = -1f;
    private Throwing _throwing;

    private void Awake()
    {
        _actions = new OivaActions();
        _mainCamera = Camera.main;
        _movement = GetComponent<Movement>();
        _throwing = GetComponent<Throwing>();

        _actions.Player.Throw.performed += OnThrow;
        _movementBoundary = Screen.height * _movementTouchAreaHeight;

        Debug.Log($"Screen height: {Screen.height}");
        Debug.Log($"Screen wifth: {Screen.width}");
    }

    private void OnEnable()
    {
        _actions.Player.Enable();
        EnhancedTouchSupport.Enable();
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            Vector2 touchPosition = Touch.activeTouches[0].screenPosition;
            if (touchPosition.y < _movementBoundary) return;

            HandleMovement(touchPosition);
        }
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
