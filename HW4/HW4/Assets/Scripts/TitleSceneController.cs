using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{

    public GameObject farmer1;
    public GameObject farmer2;
    public GameObject farmer3;

    public GameObject dog1;
    public GameObject dog2;

    // Start is called before the first frame update
    void Start()
    {
        // Set animations of game objects on title scene
        Animator farmer1Animator = farmer1.GetComponent<Animator>();
        farmer1Animator.SetInteger("Animation_int", 2);   

        Animator farmer2Animator = farmer2.GetComponent<Animator>();
        farmer2Animator.SetInteger("Animation_int", 1);  

        Animator farmer3Animator = farmer3.GetComponent<Animator>();
        farmer3Animator.SetInteger("Animation_int", 9);  

        Animator dog1Animator = dog1.GetComponent<Animator>();
        dog1Animator.SetBool("Bark_b", true);  

        Animator dog2Animator = dog2.GetComponent<Animator>();
        dog2Animator.SetBool("Sit_b", true);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
