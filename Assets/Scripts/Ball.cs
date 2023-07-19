using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool _gotHit = false;

    [Header("Gravity")]
    [SerializeField] float _gravity=-10;
    private float _powerY;

    //Power Z
    float _throwPower;
    private float _powerZ;
    float horizonDirrection;
    bool isMoving;

    //Vectors
    Vector3 moveTo;
    Vector3 ballDirrection;
    float _ballAngle;

    Bat bat;

    private void Start()
    {
        horizonDirrection = -1;
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            Move();
        }
    }

    void CalculateMove(float x, float y, float z)
    {
        _powerY = y;
        _powerZ = z * Time.deltaTime*horizonDirrection;
        ballDirrection = new Vector3(0, _powerY, _powerZ);
    }

    void CalculateMove(Vector3 d)
    {
        ballDirrection = d;
        ballDirrection.z *= -1;
    }

    private void Move()
    {
        ballDirrection.y += _gravity * Time.deltaTime;
        transform.position += ballDirrection;
    }

    public void GetThrowPower(float power)
    {
        _throwPower = power;
        CalculateMove(0,0, _throwPower);
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bat")
        {
            _gotHit = true;
            Debug.Log(_gotHit);
            bat = other.GetComponent<Bat>();
            Vector3 hitDirrection =  other.ClosestPoint(transform.position)- this.transform.position;
            CalculateMove(hitDirrection*bat._currentForce*Time.deltaTime);
            _ballAngle = Vector3.Angle(hitDirrection, ballDirrection);
            //isMoving = false;
            //_ballAngle = Vector3.Angle(hitDirrection, transform.forward);
            //transform.position = hitDirrection.normalized * bat._currentForce * Time.deltaTime;
        }
    }

}
