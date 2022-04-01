using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource Speaker;
    public GameObject ToggleGroup;
    private Toggle[] toggles;

    public Dialogue[] questions;
    public static QuestionManager instance { get; private set; }

    private int clipCount;

    void Start()
    {
        Debug.Log("Start called!");

        clipCount = loadClipCount();
        questions = initializeClips();

        toggles = ToggleGroup.GetComponentsInChildren<Toggle>(true);
        Speaker = gameObject.GetComponent<AudioSource>();

        nextQuestion();
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void nextQuestion()
    {
        if (!Speaker.isPlaying)
        {
            if (clipCount <= clips.Length - 1)
            {
                questions[clipCount].playClip(Speaker, toggles);
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
                questions[clipCount].playClip(Speaker, toggles);
                Debug.Log("Previous question playing! clip count: " + clipCount);
            }
        }
        else
        {
            Debug.Log("Speaker Already in use!");
        }
    }

    public void submit()
    {
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                questions[clipCount - 1].setUserAnswer(toggles[i].GetComponentInChildren<Text>().text);
                
                ToggleGroup.GetComponent<ToggleGroup>().SetAllTogglesOff(true);
                
                foreach (Toggle t in toggles)
                {
                    t.gameObject.SetActive(false);
                }

                saveClipCount(clipCount);

                Speaker.Stop();
                nextQuestion();

                break;
            }
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

    private int loadClipCount()
    {
        int fileInt;

        TextReader reader = new StreamReader(@"G:\Computer Science\Year 2\SoftwareProjects\Unity Projects\VR-project-real\VR game\Assets\Sounds\clipCount.txt");

        fileInt = int.Parse(reader.ReadLine());
        reader.Close();
        return fileInt;
    }
    public void saveClipCount(int count)
    {
        string pathName = @"G:\Computer Science\Year 2\SoftwareProjects\Unity Projects\VR-project-real\VR game\Assets\Sounds\clipCount.txt";

        TextWriter writer = new StreamWriter(pathName);

        writer.WriteLine(count);

        writer.Close();
    }

    void OnApplicationQuit()
    {
        Debug.Log("Quitting!");

        saveClipCount(0);

        string pathName = @"G:\Computer Science\Year 2\SoftwareProjects\Unity Projects\VR-project-real\VR game\Assets\Sounds\UserAnswers.txt";

        TextWriter writer = new StreamWriter(pathName);
        string answer;

        for (int i = 0; i < questions.Length; i++)
        {
            if ((answer = questions[i].getUserAnswer()) != null)
            {
                writer.WriteLine("Question 1: " + answer);
            }
        }
        writer.Close();
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

    public virtual void playClip(AudioSource Speaker, Toggle[] toggles)
    {
        Speaker.clip = this.clip;
        Speaker.Play();
    }
    public virtual void setUserAnswer(string userAnswer) { }
    public virtual string getUserAnswer() { return null; }

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

    public override void playClip(AudioSource Speaker, Toggle[] toggles)
    {
        // play question
        // format panels onto tablet
        Speaker.clip = this.clip;
        Speaker.Play();

        for(int i = 0; i < answerOptions.Length; i++)
        {
            toggles[i].gameObject.SetActive(true);
            toggles[i].GetComponentInChildren<Text>().text = answerOptions[i];
        }
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

    public override void setUserAnswer(string userAnswer)
    {
        this.userAnswer = userAnswer;
    }
    public override string getUserAnswer()
    {
        return userAnswer;
    }
}
