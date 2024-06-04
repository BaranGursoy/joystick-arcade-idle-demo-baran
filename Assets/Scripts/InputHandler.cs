using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static UnityAction<Vector2> OnInputDown;
    public static UnityAction<Vector2> OnInputDrag;
    public static UnityAction<Vector2> OnInputUp;

    private bool isDragging = false;

    private void Update()
    {
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                OnInputDown?.Invoke(Mouse.current.position.ReadValue());
                isDragging = true;
            }
            if (Mouse.current.leftButton.isPressed && isDragging)
            {
                OnInputDrag?.Invoke(Mouse.current.position.ReadValue());
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                OnInputUp?.Invoke(Mouse.current.position.ReadValue());
                isDragging = false;
            }
        }
        
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                OnInputDown?.Invoke(Touchscreen.current.primaryTouch.position.ReadValue());
                isDragging = true;
            }
            if (Touchscreen.current.primaryTouch.press.isPressed && isDragging)
            {
                OnInputDrag?.Invoke(Touchscreen.current.primaryTouch.position.ReadValue());
            }
            if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
            {
                OnInputUp?.Invoke(Touchscreen.current.primaryTouch.position.ReadValue());
                isDragging = false;
            }
        }
    }
}