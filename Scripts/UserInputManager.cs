using System;
using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    public event Action<DirectionVectorType> DirectionPressed;

    private void Update()
    {
        if (Application.isEditor)
        {
            ControlWithKeyboard();
        }
        else
        {
            ControlWithTouchScreen();
        }
    }

    private void Swipe()
    {
        Vector2 delta = Input.GetTouch(0).deltaPosition;


        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 40)
            {
                DirectionPressed?.Invoke(DirectionVectorType.Right);
            }

            else if (delta.x < -40)
            {
                DirectionPressed?.Invoke(DirectionVectorType.Left);
            }
        }
        else
        {
            if (delta.y > 30)
            {
                DirectionPressed?.Invoke(DirectionVectorType.Up);
            }
            else if (delta.y < -30)
            {
                DirectionPressed?.Invoke(DirectionVectorType.Down);
            }
        }
    }

    private void ControlWithTouchScreen()
    {
        if (Input.touchCount > 0)
        {
            Swipe();
        }
    }

    private void ControlWithKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DirectionPressed?.Invoke(DirectionVectorType.Right);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DirectionPressed?.Invoke(DirectionVectorType.Left);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DirectionPressed?.Invoke(DirectionVectorType.Up);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DirectionPressed?.Invoke(DirectionVectorType.Down);
        }
    }
}