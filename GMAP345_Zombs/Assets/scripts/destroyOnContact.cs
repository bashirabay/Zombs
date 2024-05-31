using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnContact : MonoBehaviour
{
    public List<string> destroyableObjects = new List<string>();
    public GameObject explosionEffect;
    public int scoreValue = 10; // Score value for destroying a zombie
    public AudioSource explosionSound; // Audio source for the explosion sound effect

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < destroyableObjects.Count; i++)
        {
            if (collision.gameObject.CompareTag(destroyableObjects[i]))
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
                Destroy(explosion, 5);

                // Play the explosion sound effect
                if (explosionSound != null)
                {
                    explosionSound.Play();
                }

                // Add score when a zombie is destroyed
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.AddScore(scoreValue);
                }
            }
        }
    }
}
