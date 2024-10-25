using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public Vector3 customGravity = new Vector3(0, -176.58f, 0);

    void Start()
    {
        Physics.gravity = customGravity;
    }
}
