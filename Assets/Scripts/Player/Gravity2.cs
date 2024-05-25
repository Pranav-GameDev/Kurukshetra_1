using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity2 : MonoBehaviour
{

    public float additionalGravityForce;
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * additionalGravityForce, ForceMode.Acceleration);
    }

}
