using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private RectTransform joystickRectTransform;
    [SerializeField] private GameObject joystickComponents;
    [SerializeField] private Canvas canvas;
    [SerializeField] private OnScreenStick onScreenStick;

    private void OnEnable()
    {
        GameActions.GameFinished += DisableJoystick;
        InputHandler.OnInputDown += HandleInputDown;
        InputHandler.OnInputDrag += HandleInputDrag;
        InputHandler.OnInputUp += HandleInputUp;
    }

    private void OnDisable()
    {
        GameActions.GameFinished -= DisableJoystick;
        InputHandler.OnInputDown -= HandleInputDown;
        InputHandler.OnInputDrag -= HandleInputDrag;
        InputHandler.OnInputUp -= HandleInputUp;
    }

    private void HandleInputDown(Vector2 pointerPosition)
    {
        if (joystickComponents.activeInHierarchy) return;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            pointerPosition,
            canvas.worldCamera,
            out localPoint
        );

        joystickRectTransform.anchoredPosition = localPoint;
        joystickComponents.SetActive(true);
        
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = pointerPosition
        };
        onScreenStick.OnPointerDown(pointerEventData);
    }

    private void HandleInputDrag(Vector2 pointerPosition)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = pointerPosition
        };
        onScreenStick.OnDrag(pointerEventData);
    }

    private void HandleInputUp(Vector2 pointerPosition)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        onScreenStick.OnPointerUp(pointerEventData);

        joystickComponents.SetActive(false);
    }

    private void DisableJoystick()
    {
        gameObject.SetActive(false);
    }
}