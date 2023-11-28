using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private OivaActions _actions;
    private Throwing _throwing;

    private void Awake()
    {
        _actions = new OivaActions();
        _throwing = GetComponent<Throwing>();

        _actions.Player.Throw.performed += OnThrow;
    }

    private void OnEnable()
    {
        _actions.Player.Enable();
    }

    private void OnDisable()
    {
        _actions.Player.Disable();
    }

    private void OnThrow(InputAction.CallbackContext context)
    {
        _throwing.Interact();
    }
}
