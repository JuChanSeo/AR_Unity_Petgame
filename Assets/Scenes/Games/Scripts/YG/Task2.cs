using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Task2 : MonoBehaviour
{
    public Button home_btn; 

    public Button EP1;
    public Button EP2;
    public Button EP3;
    public Button EP4;


    // Start is called before the first frame update
    void Start()
    {   // Playing agent introduction 

        Button h_btn= home_btn.GetComponent<Button>();
        h_btn.onClick.AddListener(HomeOnClick);
        
        Button ep1_btn= EP1.GetComponent<Button>();
        ep1_btn.onClick.AddListener(EpOnClick);
        Button ep2_btn= EP2.GetComponent<Button>();
        ep2_btn.onClick.AddListener(EpOnClick);
        Button ep3_btn= EP3.GetComponent<Button>();
        ep3_btn.onClick.AddListener(EpOnClick);
        Button ep4_btn= EP4.GetComponent<Button>();
        ep4_btn.onClick.AddListener(EpOnClick);

        
    }

    void HomeOnClick(){
        Debug.Log("Home clicked");
        SceneManager.LoadScene("Home");
    }

    void EpOnClick(){
        Debug.Log("Registering Episode");
        SceneManager.LoadScene("Task2_memory");

    }


 
}
