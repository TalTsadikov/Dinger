using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public bool _gotHit = false;
    //
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == "Bat")
    //    {
    //        _gotHit = true;
    //        Debug.Log(_gotHit);
    //        Bat bat = other.GetComponent<Bat>();
    //        Vector3 hitDirrection = this.transform.position - bat.transform.position;
    //        transform.position -= hitDirrection.normalized * bat._currentForce * Time.deltaTime;
    //    }
    //}

    public bool _gotHit { get; private set; } = false;
    private Vector3 _hitDirection;
    private float _hitForce;

    private Rigidbody _ballRigidbody;

    private void Start()
    {
        _ballRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_gotHit)
        {
            // Apply hit force to the ball's rigidbody
            _ballRigidbody.velocity = _hitDirection.normalized * _hitForce;
        }
    }

    public void ApplyHitForce(Vector3 direction, float force)
    {
        _hitDirection = direction;
        _hitForce = force;
        _gotHit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bat"))
        {
            // Ball collided with the bat, apply hit force
            Bat bat = other.GetComponent<Bat>();
            Vector3 hitDirection = transform.position - bat.transform.position;
            ApplyHitForce(hitDirection, bat._currentForce);
        }
    }

    public bool IsHit()
    {
        return _gotHit;
    }

    public void ResetHit()
    {
        _gotHit = false;
    }

}
