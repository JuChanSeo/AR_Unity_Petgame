using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Starting : MonoBehaviour
{   
    public Button start_btn; 

    public float term=0.5f;
    private float timer;
    public Text textFlicker; 
    // Start is called before the first frame update
    void Start()
    {
        Button btn= start_btn.GetComponent<Button>();
        btn.onClick.AddListener(StartOnClick);


        Text textFlicker= GetComponent<Text>();
        timer = term;


    }
    void StartOnClick(){
        Invoke("LoadingScene", 1);
        Debug.Log("load Login scene");
        // SceneManager.LoadScene("2. Greeting");
        
    }

    void LoadingScene(){
        SceneManager.LoadScene("01_Position");
    }

    // private IEnumerator Transition(){
    //     yield return StartCoroutine(screenFader.FadeOut());
    //     SceneManager.LoadScene("2. Greeting");
    // }
    // Update is called once per frame

   
    void Update()
    {
        timer -= Time.deltaTime; 
        if (timer<=0){
            textFlicker.enabled=!textFlicker.enabled;
            timer=term;
        }
    }
}
