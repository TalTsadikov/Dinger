using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool _gotHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bat")
        {
            _gotHit = true;
            Debug.Log(_gotHit);
            Bat bat = other.GetComponent<Bat>();
            Vector3 hitDirrection = this.transform.position - bat.transform.position;
            transform.position -= hitDirrection.normalized * bat._currentForce * Time.deltaTime;
        }
    }
}
