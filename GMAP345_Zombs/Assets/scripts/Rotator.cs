using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Vanessa Cunha
* Purpose: Rotates object attached
* Class: GMAP 231
* Assignment 6: NavMesh
* Due Date: August 22nd, 2023
*/

public class Rotator : MonoBehaviour
{
    //speed variable to control the rotation speed initialized
    public float speed = 0;
    // Update is called once per frame
    void Update()
    {
        /*rotates the game object this script is attached to by 15 in the x axis
         * 30 in the Y axis and 45 in the z axis, multiplies by deltaTime in order
         * to make it per second rather than per frame
         */
        transform.Rotate (new Vector3(15, 30, 45) * ((Time.deltaTime) * speed));
    }
}
