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

    public Image img1;
    public Image img2;
    public Image img3;
    // Start is called before the first frame update
    void Start()
    {   // Playing agent introduction 

        picture_btn=GameObject.Find("emotion_pict_btn").GetComponent<Button>();
        wording_btn=GameObject.Find("emotion_word_btn").GetComponent<Button>();
        sentence_btn=GameObject.Find("emotion_sent_btn").GetComponent<Button>();

        picture_btn.onClick.AddListener(()=> SceneChange("21_Emo_pict"));
        wording_btn.onClick.AddListener(()=> SceneChange("22_Emo_word"));
        sentence_btn.onClick.AddListener(()=>SceneChange("23_Emo_sentence"));
        StartCoroutine(BlinkingEffect());

    }

    public void SceneChange(string scene_name){
        Debug.Log(scene_name+ " clicked");
        SceneManager.LoadScene(scene_name);
    }

    public IEnumerator BlinkingEffect(){
        while(true){
        img1.enabled=true;
        img2.enabled=true;
        img3.enabled=true;
        yield return new WaitForSeconds (1f);
        img1.enabled=false;
        img2.enabled=false;
        img3.enabled=false;
        yield return new WaitForSeconds (1f);
        }

    }

    void Update(){
        
    }


 
}
