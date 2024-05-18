using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool isOnStartPosition;
    public Action onStartGame;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnStartPosition)
        {
            onStartGame?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            isOnStartPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            isOnStartPosition = false;
        }
    }
}
