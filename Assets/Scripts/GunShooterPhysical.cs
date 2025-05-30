using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShooterPhysical : MonoBehaviour
{
    public InputActionProperty shootAction;
    public Transform muzzleTransform;
    public GameObject bulletPrefab;
    public float bulletForce = 500f;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip shootClip;

    private void OnEnable()
    {
        shootAction.action.Enable();
    }

    private void Update()
    {
        if (shootAction.action.WasPressedThisFrame())
        {
            // Som de disparo
            if (audioSource != null && shootClip != null)
                audioSource.PlayOneShot(shootClip);

            GameObject bullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(muzzleTransform.forward * bulletForce);
            }
        }
    }
}