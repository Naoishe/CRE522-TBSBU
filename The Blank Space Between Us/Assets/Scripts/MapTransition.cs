using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] GameObject targetWaypoint;
    [SerializeField] CinemachineCamera playerCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        player.transform.position = targetWaypoint.transform.position;
    }
}
