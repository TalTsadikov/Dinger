using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] float _swingProgress;
    [SerializeField] public float _currentForce;
    [SerializeField] float _maxForce;
    [SerializeField] float _chargeTime;
    [SerializeField] float _releaseTime;
    [SerializeField] float _copyPower;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            SetSwingPower();
        else
            SwingBat();

        _swingProgress = _copyPower / _maxForce;
        _animator.SetFloat("Swing", _swingProgress);
    }

    public void SetSwingPower()
    {
        if (_copyPower >= _maxForce)
            _copyPower = _maxForce;
        else
        {
            _copyPower += (_maxForce / _chargeTime) * Time.deltaTime;
            _currentForce = _copyPower;
        }
    }

    public void SwingBat()
    {
        if (_copyPower <= 0)
            _copyPower = 0;
        else
            _copyPower -= (_currentForce / _releaseTime) * Time.deltaTime;
    }
}
