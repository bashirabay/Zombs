using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour

/*
* Author: Vanessa Cunha
* Purpose: Player Shooting Script
* Class: GMAP 231
* Assignment 6: NavMesh
* Due Date: August 22nd, 2023
*/

{
    //Variables
    public GameObject projectile;
    public Transform muzzlePoint;
    public GameObject projectileParent;
    public float lifespan = 2f;
    public float projectileSpeed = 1000f;

    // Update is called once per frame
    void Update()
    {
        //check if player hit our shoot button
        if (Input.GetMouseButtonDown(0))
        {
            //instantiate our prefab projectile
            GameObject currentProjectile = Instantiate(projectile, muzzlePoint.position, muzzlePoint.rotation);

            //set the parent of the projectile to a null object so it is not impaced by our character movement
            currentProjectile.transform.SetParent(projectileParent.transform);

            //add force to the projectile
            currentProjectile.GetComponent<Rigidbody>().AddForce(muzzlePoint.up * projectileSpeed, ForceMode.Impulse);

            //destroy the projectile after time has passed
            Destroy(currentProjectile, lifespan);
        }


    }
}
