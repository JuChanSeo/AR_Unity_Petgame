using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Episode_scene : MonoBehaviour
{
    public Button next_scene1; 
    public Button next_scene2; 
    public Button next_scene3; 
    public Button next_scene4; 
    public Button next_scene5; 
    // Start is called before the first frame update
    void Start()
    {   Button btn1= next_scene1.GetComponent<Button>();
        next_scene1.onClick.AddListener(BTN_OnClick1);
        Button btn2= next_scene2.GetComponent<Button>();
        next_scene2.onClick.AddListener(BTN_OnClick2);
        Button btn3= next_scene3.GetComponent<Button>();
        next_scene3.onClick.AddListener(BTN_OnClick3);
        Button btn4= next_scene4.GetComponent<Button>();
        next_scene4.onClick.AddListener(BTN_OnClick4);
        Button btn5= next_scene5.GetComponent<Button>();
        next_scene5.onClick.AddListener(BTN_OnClick5);
        
    }
    void BTN_OnClick1(){
        Debug.Log("Loaded");
        SceneManager.LoadScene("41_Episode_Q");        
    }
    void BTN_OnClick2(){
        Debug.Log("Loaded");
        SceneManager.LoadScene("42_Episode_Q");        
    }
    void BTN_OnClick3(){
        Debug.Log("Loaded");
        SceneManager.LoadScene("43_Episode_Q");        
    }
    void BTN_OnClick4(){
        Debug.Log("Loaded");
        SceneManager.LoadScene("44_Episode_Q");        
    }
    void BTN_OnClick5(){
        Debug.Log("Loaded");
        SceneManager.LoadScene("45_Episode_Q");        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
