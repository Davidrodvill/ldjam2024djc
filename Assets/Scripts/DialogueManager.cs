using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{


    public static Text text;
    string wordsToPrint;
    public static bool beginText = false;
    public GameObject dialogueObject;
    //public TMP_Text text;
    public AudioSource aud;
    public AudioClip spiritJingle;
    

    // Use this for initialization
    void Start()
    {
        
        //text = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
        text = GameObject.Find("DialogueText").GetComponent<Text>();
        //spiritSprite.SetActive(false);
        //textBox.SetActive(false);
        dialogueObject.SetActive(false);

        
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
            StartCoroutine(TypeText("Hello there, uh....cowboy! Press A and D to move around, and space to jump.")); 
            StartCoroutine(SpeechTime());
            aud.PlayOneShot(spiritJingle);
        }

        if(other.tag == "DialogueCollider2")
        {
            StopAllCoroutines();
            dialogueObject.SetActive(true);
            StartCoroutine(TypeText("I'm sure you've noticed by now, but you can use your mouse to summon blocks. Select different ones by pressing the Right Mouse Button [RMB]!"));
            StartCoroutine(SpeechTime());
            aud.PlayOneShot(spiritJingle);
        }

        if (other.tag == "DialogueCollider3")
        {
            StopAllCoroutines();
            dialogueObject.SetActive(true);
            StartCoroutine(TypeText("You can also hit Q and E to rotate the selected block!"));
            StartCoroutine(SpeechTime());
            aud.PlayOneShot(spiritJingle);
        }

        if(other.tag == "DialogueCollider4")
        {
            StopAllCoroutines();
            dialogueObject.SetActive(true);
            StartCoroutine(TypeText("Uh oh! Looks like we'll have to make our own way up on this one!"));
            StartCoroutine(SpeechTime());
            aud.PlayOneShot(spiritJingle);
        }

        if (other.tag == "DialogueCollider5")
        {
            StopAllCoroutines();
            dialogueObject.SetActive(true);
            StartCoroutine(TypeText("So uh....you come here often? Sorry, just trying to lighten the mood."));
            StartCoroutine(SpeechTime());
            aud.PlayOneShot(spiritJingle);
        }

        if (other.tag == "DialogueCollider6")
        {
            StopAllCoroutines();
            dialogueObject.SetActive(true);
            StartCoroutine(TypeText("Look out for the fire and lava! Trust me, it hurts!"));
            StartCoroutine(SpeechTime());
            aud.PlayOneShot(spiritJingle);
        }

        if (other.tag == "DialogueCollider7")
        {
            StopAllCoroutines();
            dialogueObject.SetActive(true);
            StartCoroutine(TypeText("Oh no! Nothing good that way. Lets try going straight up instead!"));
            StartCoroutine(SpeechTime());
            aud.PlayOneShot(spiritJingle);
        }

        if(other.tag == "DialogueCollider8")
        {
            StopAllCoroutines();
            dialogueObject.SetActive(true);
            StartCoroutine(TypeText("Aha! Now...which way do we go?"));
            StartCoroutine(SpeechTime());
            aud.PlayOneShot(spiritJingle);
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
