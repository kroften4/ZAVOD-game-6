using UnityEngine;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    public Button _leftButton;
    public Button _rightButton;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);
            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    CheckButton(_touch.position);
                    break;

                case TouchPhase.Moved:
                    CheckButton(_touch.position);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    ResetButtons();
                    break;
            }
        }
    }

    private void CheckButton(Vector2 touchPosition)
    {
        Vector2 localPoint;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, touchPosition, null, out localPoint);

        if (RectTransformUtility.RectangleContainsScreenPoint(_leftButton.GetComponent<RectTransform>(), touchPosition))
        {
            SetButtonState(_leftButton, true);
            SetButtonState(_rightButton, false);
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(_rightButton.GetComponent<RectTransform>(), touchPosition))
        {
            SetButtonState(_rightButton, true);
            SetButtonState(_leftButton, false);
        }
    }

    private void SetButtonState(Button button, bool isPressed)
    {
        if (isPressed)
        {
            button.onClick.Invoke();
            ColorBlock colors = button.colors;
            colors.normalColor = Color.gray;
            button.colors = colors;
        }
        else
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            button.colors = colors;
        }
    }

    private void ResetButtons()
    {
        SetButtonState(_leftButton, false);
        SetButtonState(_rightButton, false);
    }
}
