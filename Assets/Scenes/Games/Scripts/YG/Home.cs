using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Home : MonoBehaviour
{
    public Button task1_btn; 
    public Button task2_btn; 
    public Button task3_btn; 
    // Start is called before the first frame update
    void Start()
    {   // Playing agent introduction 

        Button t1_btn= task1_btn.GetComponent<Button>();
        Button t2_btn= task2_btn.GetComponent<Button>();
        Button t3_btn= task3_btn.GetComponent<Button>();
        t1_btn.onClick.AddListener(Task1OnClick);
        t2_btn.onClick.AddListener(Task2OnClick);
        t3_btn.onClick.AddListener(Task3OnClick);
        
    }

    void Task1OnClick(){
        Debug.Log("Task1 clicked");
        SceneManager.LoadScene("10_Greeting");
    }
    void Task2OnClick(){
        Debug.Log("Task2 clicked");
        SceneManager.LoadScene("30_Background");
    }

    void Task3OnClick(){
        Debug.Log("Task3 clicked");
        SceneManager.LoadScene("40_Episode");
    }


 
}
