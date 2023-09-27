using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{   public Button next_scene;
    // Start is called before the first frame update

    void Start()
    {
        Button btn= next_scene.GetComponent<Button>();
        btn.onClick.AddListener(NextOnClick);

    }
    void NextOnClick(){
        Invoke("LoadingScene", 1);
        Debug.Log("Start button clicked -> load home scene");
        // SceneManager.LoadScene("2. Greeting");
        
    }

    void LoadingScene(){
        SceneManager.LoadScene("10_Greeting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
