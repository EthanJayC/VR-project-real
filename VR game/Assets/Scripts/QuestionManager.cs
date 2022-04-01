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
    public Canvas textUI;
    private Toggle[] toggles;
    public string pathOrigin;

    private string[] subtitiles = { "Hi, I’m Wathanga.\nThank you for taking the time to talk to me today.\nI will be your coach for today’s session.\nAs this is your first session with us we will focus on getting to know you better and will ask you some questions around your feelings and other things that might be impacting you.\nThis is a voluntary session.\nThis means you can choose not to answer any question,there’s no such thing as a wrong answer and we can take a break/end at any time and that’s perfectly OK with me.\n\n(Press Button again to continue)", "How is your mood?\n\nChoose a response that is closest to how you’ve been feeling over the previous week.\n\n(Answer the question on your handheld slate)", "Explain more", "How well do you sleep?\n\nthink about how you’ve been sleeping over the previous week.\n\n(Answer the question on your handheld slate)", "Explain more", "How anxious or on edge do you feel?\n\nChoose an answer that’s closest to how you’ve been feeling over the previous week.\n\n(Answer the question on your handheld slate)", "Explain more", "How stressed do you feel?\n\nChoose an answer that’s closest to how you’ve been feeling over the previous week.\n\n(Answer the question on your handheld slate)", "Explain more", "Have you been worrying about anything?\n\n(Answer the question on your handheld slate)", "Explain more", "Feeling stress is a part of life but it doesn't have to be unmanageable.\nSometimes, it can feel like waves that just keep coming, higher and higher – or like a volcano about to erupt.\n\nLife will always include some stress – that's normal.\n\nStress can affect how you’re feeling or react to things.\nYou might feel worried, nervous or tense.\bYour body might feel different; you might get headaches or feel dizzy or you might get pains in your stomach.\nWe might not be able to make stress go away but we all have the power to manage it, and to take more control." };

    public Dialogue[] questions;
    public static QuestionManager instance { get; private set; }

    private int clipCount;

    void Start()
    {
        Debug.Log("Start called!");

        pathOrigin = Directory.GetCurrentDirectory();

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
                textUI.GetComponentInChildren<Text>(true).text = subtitiles[clipCount];
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
        string filepath = pathOrigin + @"\Assets\Sounds\clipCount.txt";
        TextReader reader = new StreamReader(filepath);

        fileInt = int.Parse(reader.ReadLine());
        reader.Close();
        return fileInt;
    }
    public void saveClipCount(int count)
    {
        string filepath = pathOrigin + @"\Assets\Sounds\clipCount.txt";
        TextWriter writer = new StreamWriter(filepath);

        writer.WriteLine(count);

        writer.Close();
    }

    void OnApplicationQuit()
    {
        Debug.Log("Quitting!");

        saveClipCount(0);

        string filepath = pathOrigin + @"\Assets\Sounds\UserAnswers.txt";

        TextWriter writer = new StreamWriter(filepath);
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
    private string filepath = Directory.GetCurrentDirectory();

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
        pathname = filepath + @"\Assets\Sounds\" + pathname + ".txt";
        TextReader reader = new StreamReader(pathname);

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
