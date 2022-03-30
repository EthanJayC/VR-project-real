using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsUI : MonoBehaviour
{
    public void ChangeToForest()
    {
        SceneManager.LoadScene("Forest");
    }

    public void ChangeToRoom()
    {
        SceneManager.LoadScene("Living Room Environment");
    }

    public void ChangeToOffice()
    {
        SceneManager.LoadScene("Office Scene");
    }


}
