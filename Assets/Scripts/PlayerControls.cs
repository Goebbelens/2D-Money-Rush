using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Joystick _joystick;

    [SerializeField]
    private Camera _camera;

    [Header("Values")]
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _screenBorder;


    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;

        Vector2 direction = new Vector2(horizontal, vertical);
        float inputMagnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();
        direction *= _speed * inputMagnitude * Time.deltaTime;
        if (direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
        PreventPlayerGoingOffScreen(ref direction);
        transform.Translate(direction , Space.World);
    }

    private void PreventPlayerGoingOffScreen(ref Vector2 moveDirection)
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if((screenPosition.x < _screenBorder && moveDirection.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - _screenBorder && moveDirection.x > 0))
        {
            moveDirection.x = 0;
        }
        if ((screenPosition.y < _screenBorder && moveDirection.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - _screenBorder && moveDirection.y > 0))
        {
            moveDirection.y = 0;
        }
    }
}
