using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] Material damagedMaterial;
    [SerializeField] float shootingDistance, shootingForce, bulletForce;
    [SerializeField] bool drawDebugLine = true;
    [SerializeField] AudioSource shootSound;

    void Update()
    {
        Shooting();

        // Dibuja Raycast
        if (drawDebugLine)
        {
            if (playerCamera != null)
            {
                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray ray = playerCamera.ScreenPointToRay(screenCenter);
                Debug.DrawRay(ray.origin, ray.direction * shootingDistance, Color.red);
            }
        }
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weaponAnimator.SetTrigger("Shoot");
            if (shootSound != null) shootSound.enabled = true;
            shootSound.Play();
            if (playerCamera != null)
            {
                RaycastHit impact;

                Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray ray = playerCamera.ScreenPointToRay(screenCenter);

                if (Physics.Raycast(ray, out impact, shootingDistance))
                {
                    if (impact.rigidbody != null)
                    {
                        impact.rigidbody.AddForce(-impact.normal * shootingForce, ForceMode.Impulse);
                    }

                    if (bulletPrefab != null && shootingPoint != null)
                    {
                        GameObject bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
                        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();

                        if (bulletRigidbody != null)
                        {
                            bulletRigidbody.AddForce(shootingPoint.forward * bulletForce, ForceMode.Impulse);
                        }
                        Destroy(bulletInstance, 0.05f);
                    }

                    if (impact.collider.CompareTag("EasyBarrel") || impact.collider.CompareTag("MediumBarrel") || impact.collider.CompareTag("HardBarrel"))
                    {
                        Renderer barrelRenderer = impact.collider.GetComponent<Renderer>();
                        barrelRenderer.material = damagedMaterial;
                        // Agrega puntos según el tipo de barril impactado
                        if (impact.collider.CompareTag("EasyBarrel"))
                        {
                            GameManager.Instance.AddPoints(100);
                            GameManager.Instance.numBarrels++;
                        }
                        else if (impact.collider.CompareTag("MediumBarrel"))
                        {
                            GameManager.Instance.AddPoints(200);
                            GameManager.Instance.numBarrels++;
                        }
                        else if (impact.collider.CompareTag("HardBarrel"))
                        {
                            GameManager.Instance.AddPoints(400);
                            GameManager.Instance.numBarrels++;
                        }
                        impact.collider.tag = "Untagged";
                        Destroy(impact.collider.gameObject, 2f);
                    }
                }
            }
        }
    }
}
