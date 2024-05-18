using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    [SerializeField] List<GameObject> equipableWeapons;

    private bool colliderSmallWeaponActivated, colliderLargeWeaponActivated;
    public Action onEquipWeapon;

    void Update()
    {
        ChangeWeapon();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SmallWeapon"))
        {
            colliderSmallWeaponActivated = true;
        }
        else if (other.CompareTag("LargeWeapon"))
        {
            colliderLargeWeaponActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SmallWeapon"))
        {
            colliderSmallWeaponActivated = false;
        }
        else if (other.CompareTag("LargeWeapon"))
        {
            colliderLargeWeaponActivated = false;
        }
    }

    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (colliderSmallWeaponActivated && !equipableWeapons[0].activeSelf)
            {
                equipableWeapons[0].SetActive(true);
                equipableWeapons[1].SetActive(false);
                onEquipWeapon?.Invoke();
            }
            else if (colliderLargeWeaponActivated && !equipableWeapons[1].activeSelf)
            {
                equipableWeapons[0].SetActive(false);
                equipableWeapons[1].SetActive(true);
                onEquipWeapon?.Invoke();
            }
        }
    }
}
