using System.Collections;
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

    void Start()
    {
        // Create the outline as soon as the game starts
        CreateOutline();
    }

    void Update()
    {
        // Always update the outline's position and rotation
        UpdateOutlinePositionAndRotation();

        if (Input.GetMouseButtonDown(0))          // Left mouse button clicked
        {
            HandleSummonPress();
        }

        if (Input.GetMouseButtonDown(1))          // Right mouse button clicked
        {
            CancelPlacement();
        }
    }

    private void HandleSummonPress()
    {
        ConfirmPlacement();
        // Recreate the outline immediately after placing a block
        CreateOutline();
    }

    private void CancelPlacement()
    {
        // There's no need to destroy the outline anymore, as it's always visible
        // Any other cancel-related logic can go here if needed
    }

    private void CreateOutline()
    {
        if (currentOutline == null)  // Only create the outline if it doesn't already exist
        {
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            currentOutline = Instantiate(blockOutlinePrefab, mousePosition, Quaternion.Euler(0, 0, rotationAngle));
        }
    }

    private void ConfirmPlacement()
    {
        Instantiate(blockPrefab, currentOutline.transform.position, currentOutline.transform.rotation);
        // No need to destroy the outline as it will be moved to the next position

        // Start the despawn coroutine for the placed block
        StartCoroutine(DespawnAfterTime(currentOutline, despawnTime));
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
