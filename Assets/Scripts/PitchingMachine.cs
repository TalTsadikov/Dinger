using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchingMachine : MonoBehaviour
{
    [Header("Ball Variables")]
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform _spawnPoint;
    private Vector3 _initialPosition;

    [Header("Throw Variables")]
    [SerializeField] float _throwForce;
    Vector3 _throwDirection;
    bool _isThrowing = false;

    [Header("Slider Variables")]
    [SerializeField] Slider _throwForceSlider;
    [SerializeField] private float _minSliderValue = 1f;
    [SerializeField] private float _maxSliderValue = 100f;
    [SerializeField] private float growthSpeed = 20f;
    private float _currentThrowPower = 0f;
    private bool _isSliderIncreasing = true;

    [SerializeField] Ball _ball;

    void Start()
    {
        _ball = GetComponent<Ball>();

        _initialPosition = transform.position;
        _currentThrowPower = _minSliderValue;
        _throwForceSlider.value = _currentThrowPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isThrowing)
        {
            if (_ball._gotHit == false)
            {
                ThrowBall();
            }
            else
                Debug.Log("not");
        }

        UpdateSliderValue();
    }

    void ThrowBall()
    {
        _isThrowing = true;
        _ball.GetThrowPower(_currentThrowPower);
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
}
