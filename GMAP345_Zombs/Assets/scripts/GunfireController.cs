using System.Collections; 
using UnityEngine;

public class GunfireController : MonoBehaviour
{
    // --- Audio ---
    public AudioClip GunShotClip;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    // --- Muzzle ---
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;

    // --- Scope ---
    public GameObject scope;
    public bool scopeActive = true;
    private bool lastScopeState;

    // --- Projectile ---
    public GameObject projectilePrefab;
    public Transform muzzlePoint;
    public GameObject projectileParent;
    public float lifespan = 2f;
    public float projectileSpeed = 1000f;

    // --- Reference to PauseMenu script ---
    public PauseMenu pauseMenu;

    private bool doubleShotActive = false;

    private void Start()
    {
        if (source != null) source.clip = GunShotClip;
        lastScopeState = scopeActive;
    }

    private void Update()
    {
        if (PauseMenu.GamePaused)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }

        if (scope && lastScopeState != scopeActive)
        {
            lastScopeState = scopeActive;
            scope.SetActive(scopeActive);
        }
    }

    public void FireWeapon()
    {
        var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);

        if (projectilePrefab != null)
        {
            ShootProjectile();
            if (doubleShotActive)
            {
                ShootProjectile(Vector3.right * 0.2f); // Offset the second bullet
            }
        }

        if (source != null)
        {
            PlayAudio();
        }
    }

    private void ShootProjectile(Vector3 offset = default)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, muzzlePoint.position + offset, muzzlePoint.rotation);
        newProjectile.transform.SetParent(projectileParent.transform);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(muzzlePoint.up * projectileSpeed, ForceMode.Impulse);
        }
        Destroy(newProjectile, lifespan);
    }

    private void PlayAudio()
    {
        if (source.transform.IsChildOf(transform))
        {
            source.Play();
        }
        else
        {
            AudioSource newAS = Instantiate(source);
            if (newAS != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
            {
                newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);
                newAS.PlayOneShot(GunShotClip);
                Destroy(newAS.gameObject, 4);
            }
        }
    }

    public void ActivateDoubleShot(float duration)
    {
        StartCoroutine(DoubleShot(duration));
    }

    private IEnumerator DoubleShot(float duration)
    {
        doubleShotActive = true;
        yield return new WaitForSeconds(duration);
        doubleShotActive = false;
    }
}

