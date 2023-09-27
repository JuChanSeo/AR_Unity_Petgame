using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Button pet_btn; 
    public Button task1_btn; 
    public Button task2_btn; 
    public Button task3_btn; 
    // Start is called before the first frame update
    void Start()
    {
        Button btn1= pet_btn.GetComponent<Button>();
        btn1.onClick.AddListener(Pet_OnClick);
        Button btn2= task1_btn.GetComponent<Button>();
        btn2.onClick.AddListener(BTN2_OnClick);
        Button btn3= task2_btn.GetComponent<Button>();
        btn3.onClick.AddListener(BTN3_OnClick);
        Button btn4= task3_btn.GetComponent<Button>();
        btn4.onClick.AddListener(BTN4_OnClick);

    }

    void Pet_OnClick(){
        Debug.Log("Pet page loaded");
        // SceneManager.LoadScene("scene_name");        
    }
    void BTN2_OnClick(){
        Debug.Log("20_Emotion");
        SceneManager.LoadScene("20_Emotion");        
    }
    void BTN3_OnClick(){
        Debug.Log("30_Background");
        SceneManager.LoadScene("30_Background");        
    }
    void BTN4_OnClick(){
        Debug.Log("40_Episode");
        SceneManager.LoadScene("40_Episode");        
    }

    // void LoadingScene(){
    //     SceneManager.LoadScene("10_Greeting");
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
