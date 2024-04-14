using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSummon : MonoBehaviour
{
    public List<GameObject> blockPrefabs;          // List to hold multiple block prefabs
    public List<GameObject> blockOutlinePrefabs;   // List to hold corresponding outline prefabs
    public float despawnTime = 5.0f;

    private GameObject currentOutline;
    private Vector3 mousePosition;
    private float rotationAngle = 0f;
    private int currentPrefabIndex = 0;  // Index to keep track of the currently selected block prefab

    void Start()
    {
        CreateOutline();
        Cursor.visible = false;
    }

    void Update()
    {
        UpdateOutlinePositionAndRotation();

        if (Input.GetMouseButtonDown(1))  // Right mouse button clicked
        {
            CycleBlockPrefab();
        }

        if (Input.GetMouseButtonDown(0))  // Left mouse button clicked
        {
            HandleSummonPress();
        }
    }

    private void CycleBlockPrefab()
    {
        currentPrefabIndex = (currentPrefabIndex + 1) % blockPrefabs.Count;  // Cycle through the prefabs
        if (currentOutline != null)
        {
            Destroy(currentOutline);  // Destroy the current outline
        }
        CreateOutline();  // Create a new outline with the next block prefab
    }

    private void HandleSummonPress()
    {
        ConfirmPlacement();
    }

    private void CreateOutline()
    {
        if (currentOutline != null) return;  // Return if there's already an outline

        // Make sure we have the corresponding outline for the current block prefab
        if (blockOutlinePrefabs.Count > currentPrefabIndex)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            // Use the corresponding outline prefab for the current block prefab
            GameObject outlinePrefab = blockOutlinePrefabs[currentPrefabIndex];
            currentOutline = Instantiate(outlinePrefab, mousePosition, Quaternion.Euler(0, 0, rotationAngle));
        }
        else
        {
            Debug.LogError("The list of outline prefabs does not match the list of block prefabs.");
        }
    }

    private void ConfirmPlacement()
    {
        if (blockPrefabs.Count > currentPrefabIndex)
        {
            // Instantiate the block at the outline position and rotation
            GameObject blockInstance = Instantiate(blockPrefabs[currentPrefabIndex], currentOutline.transform.position, currentOutline.transform.rotation);
            // Start the despawn coroutine for the placed block
            StartCoroutine(DespawnAfterTime(blockInstance, despawnTime));
        }
        else
        {
            Debug.LogError("The list of block prefabs does not match the list of outline prefabs.");
        }
    }

    private IEnumerator DespawnAfterTime(GameObject spawnedObject, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Ensure the object still exists
        if (spawnedObject != null)
        {
            // Get the renderer component of the object
            Renderer renderer = spawnedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Get the current color of the material
                Color initialColor = renderer.material.color;
                float fadeDuration = 2.0f; // Duration of the fade effect in seconds
                float fadeSpeed = 1.0f / fadeDuration;

                // Gradually fade out the object
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * fadeSpeed)
                {
                    if (spawnedObject == null) yield break; // Exit if the object was destroyed elsewhere

                    Color newColor = initialColor;
                    newColor.a = Mathf.Lerp(initialColor.a, 0, t); // Lerp the alpha value to 0
                    renderer.material.color = newColor;
                    yield return null; // Wait until the next frame
                }

                // Once fully transparent, destroy the object
                Destroy(spawnedObject);
            }
        }
    }

    private void UpdateOutlinePositionAndRotation()
    {
        if (currentOutline == null)
        {
            CreateOutline();  // Ensure there is always an outline
            return;
        }

        // Update the outline position to follow the mouse
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        currentOutline.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        // Rotate outline with Q and E keys
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotationAngle -= 90;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotationAngle += 90;
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
