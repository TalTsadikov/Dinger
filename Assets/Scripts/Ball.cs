using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool _gotHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bat")
        {
            Bat bat = other.GetComponent<Bat>();

            Vector3 hitDirrection = this.transform.position - bat.transform.position;
            this.transform.position -= hitDirrection.normalized * bat._currentForce * Time.deltaTime;
        }
    }
}
