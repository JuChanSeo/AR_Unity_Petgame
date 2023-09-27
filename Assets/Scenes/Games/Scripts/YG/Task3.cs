using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Task3 : MonoBehaviour
{
    private Button picture_btn;
    private Button wording_btn;
    private Button sentence_btn;
    // Start is called before the first frame update
    void Start()
    {   // Playing agent introduction 

        picture_btn=GameObject.Find("emotion_pict_btn").GetComponent<Button>();
        wording_btn=GameObject.Find("emotion_word_btn").GetComponent<Button>();
        sentence_btn=GameObject.Find("emotion_sent_btn").GetComponent<Button>();

        picture_btn.onClick.AddListener(()=> SceneChange("21_Emo_pict"));
        wording_btn.onClick.AddListener(()=> SceneChange("22_Emo_word"));
        sentence_btn.onClick.AddListener(()=>SceneChange("23_Emo_sent"));
    }

    public void SceneChange(string scene_name){
        Debug.Log(scene_name+ " clicked");
        SceneManager.LoadScene(scene_name);
    }

    void Update(){
        
    }


 
}
