using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{


    public static Text text;
    string wordsToPrint;
    public static bool beginText = false;
    public GameObject dialogueObject;
    //public TMP_Text text;
    

    // Use this for initialization
    void Start()
    {
        
        //text = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
        text = GameObject.Find("DialogueText").GetComponent<Text>();
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
            StartCoroutine(TypeText("Hello there, uh....cowboy! Press A and D to move around, and space to jump")); 
            StartCoroutine(SpeechTime());

        }

        if(other.tag == "DialogueCollider2")
        {
            StopAllCoroutines();
            dialogueObject.SetActive(true);
            StartCoroutine(TypeText("I'm sure you've noticed by now, but you can use your mouse to summon blocks. Use them to make it to the Soul Gate!"));
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
