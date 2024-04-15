using System.Collections;

using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{


    public static TextMesh text;
    string wordsToPrint;
    public static bool beginText = false;
    public GameObject dialogueObject;
    
    

    // Use this for initialization
    void Start()
    {
        TextMesh text = GameObject.Find("DialogueText").GetComponent<TextMesh>();
        //text = GameObject.Find("DialogueText").GetComponent<Text>();
        //spiritSprite.SetActive(false);
        //textBox.SetActive(false);
        dialogueObject.SetActive(true);
        
    }


    public static IEnumerator TypeText(string wordsToPrint)
    {
        text.text = "";
        foreach (char letter in wordsToPrint)
        {
            text.text += letter;
            if (Input.GetKey(KeyCode.LeftShift))
                yield return new WaitForSeconds(0.0001f);
            else
                yield return new WaitForSeconds(0.035f);

        }
        

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "DialogueCollider1") //beginning
        {
            StopAllCoroutines();
            //spiritSprite.SetActive(true);
            //textBox.SetActive(true);
            dialogueObject.SetActive(true);
            //text here
            StartCoroutine(TypeText("AAAAAAAAAAHHHHHHHHHH FUCK THIS GAME JAM")); //PLEASE DO NOT LEAVE THIS IN THE GAME
            StartCoroutine(SpeechTime());

        }

        

    }

    IEnumerator SpeechTime()
    {

        yield return new WaitForSeconds(10);
        StartCoroutine(TypeText(""));
        dialogueObject.SetActive(false);
        //diddySprite.SetActive(false); //REPLACE WITH SPIRIT SPRITE
        //textBox.SetActive(false);
    }

}
