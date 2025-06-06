using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunModel : MonoBehaviour
{
    public InputActionProperty shootAction;
    public Transform muzzleTransform;
    public GameObject bulletPrefab;
    public float bulletForce = 500f;
    public float cadence;
    protected float cadenceControl;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip shootClip;

    protected void OnEnable()
    {
        shootAction.action.Enable();
        Console.WriteLine("OnEnable herdado");
    }

    protected void Update()
    {
        if (shootAction.action.WasPressedThisFrame() && CanShoot())
        {
            cadenceControl = Time.time + cadence;
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

    protected bool CanShoot()
    {
        return Time.time > cadenceControl;
    }
}
