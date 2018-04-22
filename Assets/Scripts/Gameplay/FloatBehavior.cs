using UnityEngine;
using System;
using System.Collections;

public class FloatBehavior : MonoBehaviour
{
    float originalY;
    public float floatStrength = .25f;
    public float yOffset = .75f;
    public Vector3 newPosition;

    void Start()
    { 
        this.originalY = this.transform.position.y + yOffset;
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z); 
    }

    void Update()
    { 
        transform.position = new Vector3(transform.position.x, originalY + ((float)Math.Sin(Time.time * 1.5) * floatStrength), transform.position.z);
    }
}