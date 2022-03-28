using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    int clipCount;
    bool isPressed;
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public GameObject presser;
    public AudioClip[] clips;
    public AudioSource Speaker;

    //canvas stuff
    public GameObject TextUI;
    public GameObject TE;
    public bool TextUp = false;


    void Start()
    {
        
        Speaker = gameObject.GetComponent<AudioSource>();
        isPressed = false;
        clipCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();

            if (!Speaker.isPlaying)
            {
                Speaker.clip = clips[clipCount];
                Speaker.Play();
                clipCount++;
            }
            isPressed = true;
            //sets the textUI to be visible on button press
            TextUp = true;
            TextUI.SetActive(TextUp);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }

    void Update()
    {
        //___________________displays relevant text to the clip count________________________________
        //NOTE to team: clipCount jumps 2 because each odd number clip is the "no answer" speech script
        // which is redundant until we implement that option on answering the questions. 

        //pre intro script
        if (clipCount == 1) TE.GetComponent<Text>().text = ("Hi, I’m Wathanga.\nThank you for taking the time to talk to me today.\nI will be your coach for today’s session.\nAs this is your first session with us we will focus on getting to know you better and will ask you some questions around your feelings and other things that might be impacting you.\nThis is a voluntary session.\nThis means you can choose not to answer any question,there’s no such thing as a wrong answer and we can take a break/end at any time and that’s perfectly OK with me.\n\n(Press Button again to continue)");
        //how is your mood script
        if (clipCount == 2) TE.GetComponent<Text>().text = ("How is your mood?\n\nChoose a response that is closest to how you’ve been feeling over the previous week.\n\n(Answer the question on your handheld slate)");
        //how is your sleep script
        if (clipCount == 4) TE.GetComponent<Text>().text = ("How well do you sleep?\n\nthink about how you’ve been sleeping over the previous week.\n\n(Answer the question on your handheld slate)");
        //axiety script
        if (clipCount == 6) TE.GetComponent<Text>().text = ("How anxious or on edge do you feel?\n\nChoose an answer that’s closest to how you’ve been feeling over the previous week.\n\n(Answer the question on your handheld slate)"); ;
        //stress script
        if (clipCount == 8) TE.GetComponent<Text>().text = ("How stressed do you feel?\n\nChoose an answer that’s closest to how you’ve been feeling over the previous week.\n\n(Answer the question on your handheld slate)"); ;
        //worry script
        if (clipCount == 10) TE.GetComponent<Text>().text = ("Have you been worrying about anything?\n\n(Answer the question on your handheld slate)"); ;
        //first part of the session script (oh boy that's alot of script)
        if (clipCount == 12) TE.GetComponent<Text>().text = ("Feeling stress is a part of life but it doesn't have to be unmanageable.\nSometimes, it can feel like waves that just keep coming, higher and higher – or like a volcano about to erupt.\n\nLife will always include some stress – that's normal.\n\nStress can affect how you’re feeling or react to things.\nYou might feel worried, nervous or tense.\bYour body might feel different; you might get headaches or feel dizzy or you might get pains in your stomach.\nWe might not be able to make stress go away but we all have the power to manage it, and to take more control.");





    }
}
