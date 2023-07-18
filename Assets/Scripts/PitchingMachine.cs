using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchingMachine : MonoBehaviour
{
    // [Header("Ball Variables")]
    // [SerializeField] GameObject _prefab;
    // [SerializeField] Transform _spawnPoint;
    // Rigidbody _ballRigidbody;
    // private Vector3 _initialPosition;
    //
    // [Header("Throw Variables")]
    // [SerializeField] float _throwForce;
    // Vector3 _throwDirection;
    // bool _isThrowing = false;
    //
    // [Header("Slider Variables")]
    // [SerializeField] Slider _throwForceSlider;
    // [SerializeField] private float _minSliderValue = 1f;
    // [SerializeField] private float _maxSliderValue = 100f;
    // [SerializeField] private float growthSpeed = 20f;
    // private float _currentThrowPower = 0f;
    // private bool _isSliderIncreasing = true;
    //
    // [SerializeField] Ball _ball;
    //
    //void Start()
    //{
    //    _ballRigidbody = GetComponent<Rigidbody>();
    //    _ball = GetComponent<Ball>();
    //    _ballRigidbody.useGravity = false;
    //    _initialPosition = transform.position;
    //    _throwDirection = Vector3.back;
    //    _currentThrowPower = _minSliderValue;
    //    _throwForceSlider.value = _currentThrowPower;
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && !_isThrowing)
    //    {
    //        if (_ball._gotHit == false)
    //        {
    //            ThrowBall();
    //        }
    //        else
    //            Debug.Log("not");
    //    }
    //
    //    UpdateSliderValue();
    //}
    //
    //void FixedUpdate()
    //{
    //    if (_isThrowing)
    //    {
    //        if (_ballRigidbody.velocity.magnitude <= 0.01f)
    //        {
    //            _ballRigidbody.velocity = Vector3.zero;
    //            _ballRigidbody.angularVelocity = Vector3.zero;
    //            _isThrowing = false;
    //        }
    //    }
    //}
    //
    //void ThrowBall()
    //{
    //    _ballRigidbody.velocity = _throwDirection * _currentThrowPower;
    //    _ballRigidbody.useGravity = true;
    //    _isThrowing = true;
    //}
    //
    //void UpdateSliderValue()
    //{
    //    if (!_isThrowing)
    //    {
    //        if (_isSliderIncreasing)
    //        {
    //            _currentThrowPower += growthSpeed * Time.deltaTime;
    //            if (_currentThrowPower >= _maxSliderValue)
    //            {
    //                _currentThrowPower = _maxSliderValue;
    //                _isSliderIncreasing = false;
    //            }
    //        }
    //        else
    //        {
    //            _currentThrowPower -= growthSpeed * Time.deltaTime;
    //            if (_currentThrowPower <= _minSliderValue)
    //            {
    //                _currentThrowPower = _minSliderValue;
    //                _isSliderIncreasing = true;
    //            }
    //        }
    //    }
    //
    //    _throwForceSlider.value = _currentThrowPower;
    //}
    [Header("Ball Variables")]
    [SerializeField] Transform _ballTransform;
    private Ball _ball;
    private Rigidbody _ballRigidbody;
    private bool _isThrowing = false;

    [Header("Throw Variables")]
    [SerializeField] float _throwForce;
    Vector3 _throwDirection;

    [Header("Slider Variables")]
    [SerializeField] Slider _throwForceSlider;
    [SerializeField] private float _minSliderValue = 1f;
    [SerializeField] private float _maxSliderValue = 100f;
    [SerializeField] private float growthSpeed = 20f;
    private float _currentThrowPower = 0f;
    private bool _isSliderIncreasing = true;

    private Vector3 _initialPosition;
    private bool _gravityEnabled = false;

    void Start()
    {
        _throwDirection = Vector3.back;
        _currentThrowPower = _minSliderValue;
        _throwForceSlider.value = _currentThrowPower;

        _ball = _ballTransform.GetComponent<Ball>();
        _ballRigidbody = _ballTransform.GetComponent<Rigidbody>();

        _initialPosition = _ballTransform.position; // Store the initial position

        _ballRigidbody.useGravity = false; // Disable gravity initially
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isThrowing)
        {
            if (!_ball.IsHit())
            {
                ThrowBall();
                _gravityEnabled = true; // Enable gravity when the ball is thrown
            }
        }

        UpdateSliderValue();

        if (_isThrowing)
        {
            Vector3 movement = _throwDirection * _currentThrowPower * Time.deltaTime;
            _ballTransform.position += movement;

            if (_gravityEnabled)
            {
                // Apply gravity
                Vector3 gravityMovement = Physics.gravity * Time.deltaTime;
                _ballTransform.position += gravityMovement;
            }
        }
    }

    void ThrowBall()
    {
        _isThrowing = true;
        _ball.ApplyHitForce(_throwDirection, _currentThrowPower);
    }

    void UpdateSliderValue()
    {
        if (!_isThrowing)
        {
            if (_isSliderIncreasing)
            {
                _currentThrowPower += growthSpeed * Time.deltaTime;
                if (_currentThrowPower >= _maxSliderValue)
                {
                    _currentThrowPower = _maxSliderValue;
                    _isSliderIncreasing = false;
                }
            }
            else
            {
                _currentThrowPower -= growthSpeed * Time.deltaTime;
                if (_currentThrowPower <= _minSliderValue)
                {
                    _currentThrowPower = _minSliderValue;
                    _isSliderIncreasing = true;
                }
            }
        }

        _throwForceSlider.value = _currentThrowPower;
    }

    public void ResetBall()
    {
        _isThrowing = false;
        _gravityEnabled = false; // Disable gravity
        _ball.ResetHit();
        _ballTransform.position = _initialPosition; // Reset ball position
    }
}