using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSummon : MonoBehaviour
{
    public GameObject blockPrefab;        
    public GameObject blockOutlinePrefab; 
    public Transform spawnPoint;          

    private GameObject currentOutline;    
    private bool isPreviewActive = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleSummonPress();
        }
    }
    /*
     * 
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⣷⣶⡤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠠⣤⣤⣤⣤⣄⡀⣿⣿⣷⣳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⢻⣿⣿⣿⣼⣿⣿⣿⣇⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣿⣿⣾⣿⣿⣿⣿⣷⣦⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⢀⣤⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠉⠛⠿⣿⣿⣿⣿⣿⣿⣿⣻⣿⣿⣿⣿⣿⣿⣿⡿⠗⠂⠀⠀⠀
⠀⠀⠀⠀⠀⠀⣤⣴⣾⣟⣿⣿⣿⠟⢀⣿⠟⢁⣟⡿⢻⣿⣷⣤⠤⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠉⠙⠛⢟⢹⣽⠙⠦⣨⣋⡴⠚⢋⣇⣾⠟⠋⠉⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠚⠓⢷⡏⠍⣴⠀⠁⠀⡶⠚⠛⠓⠂⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⣀⡀⢀⡌⡿⡤⡘⠃⢠⡊⠁⠀⣀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⣠⣤⣾⡟⣿⣴⢏⡀⠓⠰⠼⣠⠑⠀⠀⢶⣼⡟⢲⣴⡀⠀⠀⠀⠀
⠀⠀⡖⣭⠓⣿⣾⡏⣿⣿⣦⣵⠀⠀⠀⡆⠀⢠⣶⣿⣿⢩⣼⣿⠘⣥⠒⡄⡄
⠀⣼⡇⣿⡰⡄⣿⡵⣛⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⢢⢜⢣⠱⢠⠒⣱⡅
⣼⣿⣿⣸⡗⣷⡘⣷⢋⢶⣛⢿⣿⣿⣿⣿⣿⠿⢋⡴⢋⡌⢂⠧⣘⡣⣡⣿⣿
*/
    private void HandleSummonPress()
    {
        if (!isPreviewActive)
        {
            ShowPreview();
        }
        else
        {
            ConfirmPlacement();
        }
    }

    private void ShowPreview()
    {
        if (currentOutline != null)
        {
            Destroy(currentOutline); // Ensure only one preview is active at a time
        }

        currentOutline = Instantiate(blockOutlinePrefab, spawnPoint.position, Quaternion.identity);
        isPreviewActive = true;
    }

    private void ConfirmPlacement()
    {
        if (currentOutline != null)
        {
            Destroy(currentOutline); // Remove the outline
        }

        Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity); // Summon the actual block
        isPreviewActive = false;
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
