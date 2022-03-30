using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
    }

    public void ChangeToForest()
    {
        Debug.Log("DING DONG");
        SceneManager.LoadScene("Forest");
    }

    public void ChangeToLiving()
    {
        Debug.Log("DING DONG");
        SceneManager.LoadScene("LivingRoom");
    }

    public void ChangeToOffice()
    {
        Debug.Log("DING DONG");
        SceneManager.LoadScene("Office");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
