using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Task1 : MonoBehaviour
{
    public Button home_btn; 

    // Start is called before the first frame update
    void Start()
    {   // Playing agent introduction 

        Button h_btn= home_btn.GetComponent<Button>();

        h_btn.onClick.AddListener(HomeOnClick);
        
    }

    void HomeOnClick(){
        Debug.Log("Home clicked");
        SceneManager.LoadScene("Home");
    }


 
}
