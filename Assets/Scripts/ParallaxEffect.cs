using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam; // reference to the camera
    public Transform followTarget; // target to follow

    
    Vector2 startingPosition; //starting position for the parallax game object
    float startingZ; //start z value of the parallax game object


    // Start is called before the first frame update
    void Start()
    {
        startingPosition= transform.position; // store the starting position of the parallax game object
        startingZ = transform.position.z; //store the starting z value of the parallax game object
    }

    // Update is called once per frame
    void Update()
    {

        // calculate the movement of the camera since the start
        Vector2 camMoveSinceStart = (Vector2)cam.transform.position - startingPosition;

        // calculate the distance in z-axis between the parallax object and the follow target
        float zDistanceFromTarget = transform.position.z - followTarget.transform.position.z;

        // calculate the clipping plane based on the z-distance from the target
        float clippingPlane = (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

        // calculate the parallax factor based on the distance from the target and the clipping plane
        float parallaxFactor = Mathf.Abs(zDistanceFromTarget / clippingPlane);

        // calculate the new position of the parallax game object
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // set the new position of the parallax game object while maintaining the starting z value
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);

    }
}
