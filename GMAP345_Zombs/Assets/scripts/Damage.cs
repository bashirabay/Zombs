using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int DMG;
    private Animator anim;
    private float cooldown = 0f;
    public FlashController flashController;  // Reference to FlashController

    private void Start()
    {
        GetReferences();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ensure this script only triggers on collision with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(DMG);
                if (flashController != null)
                {
                    flashController.Flash();  // Trigger the flash effect
                }

                // Trigger the attack animation
                anim.SetTrigger("AttackTrigger");
            }
        }
    }

    private void GetReferences()
    {
        anim = GetComponentInChildren<Animator>();
        // Optionally, find the FlashController if not assigned in the Inspector
        if (flashController == null)
        {
            flashController = FindObjectOfType<FlashController>();
        }
    }
}