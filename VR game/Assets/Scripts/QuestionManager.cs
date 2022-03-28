using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource Speaker;
    private Question[] questions;

    private int clipCount;

    // Start is called before the first frame update
    void Start()
    {
        Speaker = gameObject.GetComponent<AudioSource>();
        clipCount = 0;

        questions = initializeQuestions();
        nextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextQuestion()
    {
        if (!Speaker.isPlaying)
        {
            if (clipCount <= clips.Length - 1)
            {
                Speaker.clip = questions[clipCount].getClip();
                Speaker.Play();
                clipCount++;
                Debug.Log("Next question playing!");
            }
        } else {
            Debug.Log("Speaker Already in use!");
        }
    }

    public void previousQuestion()
    {
        if (!Speaker.isPlaying)
        {
            if (clipCount > 0)
            {
                clipCount--;
                Speaker.clip = questions[clipCount].getClip();
                Speaker.Play();
                Debug.Log("Previous question playing!");
            }
        }
        else
        {
            Debug.Log("Speaker Already in use!");
        }
    }

    private Question[] initializeQuestions()
    {
        Question[] tempQuestions = new Question[clips.Length];

        for(int i = 0; i < tempQuestions.Length; i++)
        {
            tempQuestions[i] = new Question(clips[i]);
            tempQuestions[i].setUserAnswer("option 7");
        }

        return tempQuestions;
    } 
}

public class Question
{

    private AudioClip questionSound;
    private string[] answerOptions;
    private string userAnswer;

    public Question (AudioClip questionSound, string[] answerOptions)
    {
        this.questionSound = questionSound;
        this.answerOptions = answerOptions;
    }
    public Question(AudioClip questionSound)
    {
        this.questionSound = questionSound;
    }

    public void setUserAnswer (string userAnswer)
    {
        this.userAnswer = userAnswer; 
    }
    public string getUserAnswer ()
    {
        return userAnswer;
    }
    public AudioClip getClip()
    {
        return questionSound;
    }
}
