using System.Collections.Generic;
using UnityEngine;

public class ActivatePanels : MonoBehaviour
{
    public List<GameObject> panels;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var panel in panels)
            {
                panel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var panel in panels)
            {
                panel.SetActive(false);
            }
        }
    }
}
