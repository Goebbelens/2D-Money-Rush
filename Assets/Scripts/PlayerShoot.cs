using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject _bulletPrefab;

    [Header("Values")]
    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private float _timeBetweenShots;
    [SerializeField]
    private float _lastFireTime;

    [Header("Firing status")]
    [SerializeField]
    private bool _fireContinuously;
    [SerializeField]
    private bool _fireSingle;

    void Update()
    {
        if(_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();

                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position + transform.up * 1f, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * transform.up;
    }

    private void OnFire(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed;

        if(inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }
}
