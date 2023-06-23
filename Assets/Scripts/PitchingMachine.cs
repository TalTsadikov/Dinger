using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchingMachine : MonoBehaviour
{
    [Header("Ball Variables")]
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform _spawnPoint;
    Rigidbody _ballRigidbody;
    private GameObject _ball;

    [Header("Throw Force Variables")]
    [SerializeField] float _throwForce;
    [SerializeField] Slider _throwForceSlider;
    float _fillTime = 0f;
    bool _thrown;

    void Start()
    {
        _ball = Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
        _ballRigidbody = _ball.GetComponent<Rigidbody>();
        _ballRigidbody.useGravity = false;
        _thrown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _thrown == false) 
        {
            _thrown = true;
            _throwForce = _throwForceSlider.value;
            _ballRigidbody.AddForce(transform.forward * _throwForce * 50);
            _ballRigidbody.useGravity = true;
        }

        if (_thrown == false)
        {
            _throwForceSlider.value = Mathf.Lerp(_throwForceSlider.minValue, _throwForceSlider.maxValue, _fillTime);
            _fillTime += 0.375f * Time.deltaTime;
        }
    }
}
