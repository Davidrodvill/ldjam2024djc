using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{

    //public int timer;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(FirePillarTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator FirePillarTimer()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //should set the animation to true
        anim.SetBool("isLava", true);

        //sets its box collider to true
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(0.8f);
        //sets anim bool to false
        anim.SetBool("isLava", false);
        //then disables box collider
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //waits a few seconds
        yield return new WaitForSeconds(3);
        //calls the coroutine again so it loops
        StartCoroutine(FirePillarTimer());


    }

}
