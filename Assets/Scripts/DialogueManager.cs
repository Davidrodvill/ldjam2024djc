using System.Collections;

using UnityEngine;
using UnityEngine.UI;


public class DiddyKongDialogue : MonoBehaviour
{


    public static Text text;
    string wordsToPrint;
    public static bool beginText = false;
    public GameObject spiritSprite, textBox;
    
    

    // Use this for initialization
    void Start()
    {

        text = GameObject.Find("DialogueText").GetComponent<Text>();
        spiritSprite.SetActive(false);
        textBox.SetActive(false);
        
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

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "DialogueCollider1") //beginning
        {
            StopAllCoroutines();
            spiritSprite.SetActive(true);
            textBox.SetActive(true);
            StartCoroutine(TypeText("AAAAAAAAAAHHHHHHHHHH FUCK THIS GAME JAM")); //PLEASE DO NOT LEAVE THIS IN THE GAME
            StartCoroutine(SpeechTime());

        }

        

    }

    IEnumerator SpeechTime()
    {

        yield return new WaitForSeconds(10);
        StartCoroutine(TypeText(""));
        //diddySprite.SetActive(false); //REPLACE WITH SPIRIT SPRITE
        textBox.SetActive(false);
    }

}
