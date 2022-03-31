using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ChangeToForest()
    {
        SceneManager.LoadScene("Forest");
    }

    public void ChangeToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ChangeToLiving()
    {
        SceneManager.LoadScene("LivingRoom");
    }

    public void ChangeToOffice()
    {
        SceneManager.LoadScene("Office");
    }
}
