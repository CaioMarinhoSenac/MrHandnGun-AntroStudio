using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShooterXR : MonoBehaviour
{
    [Header("Input")]
    public InputActionProperty shootAction;

    [Header("Transformações")]
    public Transform muzzleTransform;
    public Transform crosshair;

    [Header("Raycast")]
    public float range = 100f;
    public LayerMask hitLayers;

    [Header("Efeitos Visuais")]
    public GameObject hitEffect;

    [Header("Áudio")]
    public AudioSource shootAudioSource;
    public AudioClip shootClip;

    private void OnEnable()
    {
        shootAction.action.Enable();
    }

    private void Update()
    {
        if (shootAction.action.WasPressedThisFrame())
        {
            // Feedback visual na Scene View
            Debug.DrawRay(muzzleTransform.position, muzzleTransform.forward * range, Color.red, 0.1f);

            // Som do tiro
            if (shootAudioSource != null && shootClip != null)
                shootAudioSource.PlayOneShot(shootClip);

            // Raycast para detectar impacto
            RaycastHit hit;
            bool acerto = false;

            if (Physics.Raycast(muzzleTransform.position, muzzleTransform.forward, out hit, range, hitLayers))
            {
                // Efeito no ponto do impacto
                if (hitEffect != null)
                    Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));

                // Se for inimigo
                if (hit.collider.CompareTag("Enemy"))
                {
                    acerto = true;
                    ScoreManager.Instance.AddPoint(1);
                    Destroy(hit.collider.gameObject);
                }

                // Posiciona mira no impacto
                if (crosshair != null)
                {
                    crosshair.position = hit.point;
                    crosshair.rotation = Quaternion.LookRotation(hit.normal);
                }
            }
            else
            {
                // Mira avança para a frente se nada for atingido
                if (crosshair != null)
                {
                    crosshair.position = muzzleTransform.position + muzzleTransform.forward * 10f;
                    crosshair.rotation = Quaternion.identity;
                }
            }

            // Verifica batida com música (ritmo)
            if (BeatManagerComAudio.Instance != null)
            {
                float bpm = BeatManagerComAudio.Instance.bpm;
                float beatInterval = 60f / bpm;
                float songPos = BeatManagerComAudio.Instance.songPosition;
                float nearestBeat = Mathf.Round(songPos / beatInterval) * beatInterval;

                if (Mathf.Abs(songPos - nearestBeat) > 0.15f)
                {
                    if (!acerto)
                    {
                        ScoreManager.Instance.AddPoint(-1);
                        Debug.Log("Atirou fora do ritmo!");
                    }
                }
            }
        }
    }
}