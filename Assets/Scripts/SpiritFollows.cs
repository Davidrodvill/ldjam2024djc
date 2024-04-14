using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritFollows : MonoBehaviour
{
    public Transform player;          
    public float hoverHeight = 1.5f;  
    public float followSpeed = 2f;    
    public float rotationSpeed = 5f;  

    void Update()
    {
        if (player == null) return;  

        HoverOverPlayer();
    }

    private void HoverOverPlayer()
    {
        // calculations ;;;)PPP
        Vector3 targetPosition = player.position + Vector3.up * hoverHeight;

        // bot moves twards player
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        //rotation
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}

