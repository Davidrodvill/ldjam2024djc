﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSummon : MonoBehaviour
{
    public GameObject blockPrefab;                
    public GameObject blockOutlinePrefab;         
    public float despawnTime = 5.0f;              

    private GameObject currentOutline;            
    private Vector3 mousePosition;                
    private float rotationAngle = 0f;             

    void Update()
    {
        if (Input.GetMouseButtonDown(0))          // Left mouse button clicked
        {
            HandleSummonPress();
        }

        if (Input.GetMouseButtonDown(1))          // Right mouse button clicked
        {
            CancelPlacement();
        }

        if (currentOutline != null)
        {
            UpdateOutlinePositionAndRotation();
        }
    }

    private void HandleSummonPress()
    {
        if (currentOutline == null)
        {
            CreateOutline();
        }
        else
        {
            ConfirmPlacement();
        }
    }

    private void CancelPlacement()
    {
        if (currentOutline != null)
        {
            Destroy(currentOutline);
            currentOutline = null;
        }
    }

    private void CreateOutline()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        currentOutline = Instantiate(blockOutlinePrefab, mousePosition, Quaternion.Euler(0, 0, rotationAngle));
    }

    private void ConfirmPlacement()
    {
        GameObject blockInstance = Instantiate(blockPrefab, currentOutline.transform.position, currentOutline.transform.rotation);
        Destroy(currentOutline); // Remove the outline after placement
        currentOutline = null;
        StartCoroutine(DespawnAfterTime(blockInstance, despawnTime)); // Start the despawn coroutine
    }

    private IEnumerator DespawnAfterTime(GameObject spawnedObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(spawnedObject); // Destroy the object after the delay
    }

    private void UpdateOutlinePositionAndRotation()
    {
        // Update the outline position to follow the mouse
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        currentOutline.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        // Rotate outline with Q and E keys
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotationAngle -= 90; // Rotate left by 90 degrees
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotationAngle += 90; // Rotate right by 90 degrees
        }

        // Apply rotation
        currentOutline.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}
/*
 * 
───────────────────██
──────────────────█─██
──────────────────█───█
──────────────────█───█
──────────────────█───█
──────────────────█───█
──────────────────█───█▓
──────────────────█───▓█
──────────────────█───░█
──────────────────█───░█
──────────────────█░░░─█
───────────▓███──██▓▓███
───────────██──▓██▓────██
───────────█▓────█▓─────▓█
───────────█▓─────█──────░█
██████─────█▓─────█────────█
████████▓███░──────█──█▓────█
█░░░░░░█───────────█░███────█▓
▓██████─────────────█▓██────██
███████░────────────────────▓█
▓██████░────────────────────░█
▓██████░────────────────────▓█
▓██████░────────────────────█▓
▓██████░────────────────────█
▓██████░───────────────────██
▓███░██░──────────────────█
▓███──████████████████████
██████
*/
