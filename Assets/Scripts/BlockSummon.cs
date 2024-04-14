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

      
    }

    private void HandleSummonPress()
    {
        ConfirmPlacement();
        // Recreate the outline immediately after placing a block
        CreateOutline();
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
        // Instantiate the block at the outline position and rotation
        GameObject blockInstance = Instantiate(blockPrefab, currentOutline.transform.position, currentOutline.transform.rotation);

        // Start the despawn coroutine for the placed block
        StartCoroutine(DespawnAfterTime(blockInstance, despawnTime));
    }

    private IEnumerator DespawnAfterTime(GameObject spawnedObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (spawnedObject != null)
        {
            Destroy(spawnedObject); // Destroy the object after the delay
        }
    }

    private void UpdateOutlinePositionAndRotation()
    {
        if (currentOutline == null)
        {
            return; // Exit if there's no outline to update
        }

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
