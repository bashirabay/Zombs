using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnContact : MonoBehaviour

/*
* Author: Vanessa Cunha
* Purpose: Destroys objects when shot
* Class: GMAP 231
* Assignment 6: NavMesh
* Due Date: August 22nd, 2023
*/

{
    //declare our list of strings we'll use if we want our projectile to destroy an object it hits
    public List<string> destroyableObjects = new List<string>();
    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        //run through our list of tags we want to destroy
        for (int i = 0; i < destroyableObjects.Count; i++)
        {
            //check if the hit object has one of our tags
            if(collision.gameObject.CompareTag(destroyableObjects[i]))
            {
                //destroy the hit object and the projectile
                Destroy(gameObject);
                Destroy(collision.gameObject);
                GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
                Destroy(explosion, 5);
            }
        }
    }
}
