using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionManager : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource Speaker;
    private Dialogue[] questions;

    private int clipCount;

    // Start is called before the first frame update
    void Start()
    {
        //Speaker = gameObject.GetComponent<AudioSource>();
        //clipCount = 0;

        questions = initializeClips();
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
                questions[clipCount].playClip(Speaker);
                clipCount++;
                Debug.Log("Next question playing! clip count: " + clipCount);
            }
        } else {
            Debug.Log("Speaker Already in use!");
        }
    }

    public void previousQuestion()
    {
        if (!Speaker.isPlaying)
        {
            if (clipCount > 1)
            {
                clipCount--;
                questions[clipCount].playClip(Speaker);
                Debug.Log("Previous question playing! clip count: " + clipCount);
            }
        }
        else
        {
            Debug.Log("Speaker Already in use!");
        }
    }

    private Dialogue[] initializeClips()
    {
        Dialogue[] tempQuestions = new Dialogue[clips.Length];

        for(int i = 0; i < tempQuestions.Length; i++)
        {
            if (clips[i].name.Contains("question"))
            {
                tempQuestions[i] = new Question(clips[i], clips[i].name);
            } else
            {
                tempQuestions[i] = new Dialogue(clips[i]);
            }
        }

        return tempQuestions;
    }
    /*
    public void GetAnswersFile()
    {
        string pathorigin = @"G:\Computer Science\Year 2\SoftwareProjects\Unity Projects\VR-project-real\VR game\Assets\Sounds\";

        TextReader reader = new StreamReader(@"G:\Computer Science\Year 2\SoftwareProjects\Unity Projects\VR-project-real\VR game\Assets\Sounds\Answers.txt");

        int count = int.Parse(reader.ReadLine());
        for (int i = 0; i < count; i++)
        {
            string pathName = reader.ReadLine();

            TextWriter writer = new StreamWriter(pathorigin + pathName + ".txt");

            int bound = int.Parse(reader.ReadLine());
            string answer;
            for (int j = 0; j < bound; j++)
            {
                answer = reader.ReadLine();
                Debug.Log(answer);
                writer.WriteLine(answer);
            }

            writer.Close();
        }

        reader.Close();
        Debug.Log("Concluded!");
    }*/
}

public class Dialogue
{

    protected AudioClip clip;

    public Dialogue(AudioClip clip)
    {
        this.clip = clip;
    }
    public Dialogue() 
    {
        
    }

    public virtual void playClip(AudioSource Speaker)
    {
        Speaker.clip = this.clip;
        Speaker.Play();
    }

    public AudioClip getClip()
    {
        return clip;
    }
}

public class Question : Dialogue
{

    private string[] answerOptions;
    private string userAnswer;

    public Question(AudioClip clip, string pathname)
    {
        this.clip = clip;
        loadAnswers(pathname);
    }

    public override void playClip(AudioSource Speaker)
    {
        // play audio then spawn in potential answers onto answer UI
    }

    private void loadAnswers(string pathname)
    {
        TextReader reader = new StreamReader(@"G:\Computer Science\Year 2\SoftwareProjects\Unity Projects\VR-project-real\VR game\Assets\Sounds\" + pathname + ".txt");

        string currentLine;
        int count = int.Parse(reader.ReadLine());
        answerOptions = new string[count];

        count = 0;
        while ((currentLine = reader.ReadLine()) != null)
        {
            answerOptions[count] = currentLine;
            count++;
        }
    }

    public void setUserAnswer(string userAnswer)
    {
        this.userAnswer = userAnswer;
    }
    public string getUserAnswer()
    {
        return userAnswer;
    }
}
