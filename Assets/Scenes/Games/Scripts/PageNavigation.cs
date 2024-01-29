using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageNavigation : MonoBehaviour
{

    bgm_player bgm_player_;

    public void quit_app()
    {
        Application.Quit();
    }
    public void butoon_effect()
    {
        bgm_player_ = GameObject.Find("Audio player").GetComponent<bgm_player>();
        bgm_player_.butoon_effect();
    }

    public void Menu_CA()
    {
        butoon_effect();
        SceneManager.LoadScene("20_Emotion");
    }

    public void CA_tutorial()
    {
        butoon_effect();
        SceneManager.LoadScene("00_Tutorial");
    }

    public void Tutorial()
    {
        butoon_effect();
        SceneManager.LoadScene("Tutorial");
    }
    public void exp_game()
    {
        SceneManager.LoadScene("game_for_exp");
    }
    public void Intention_recog()
    {
        SceneManager.LoadScene("Intention_recognition");
    }
    public void PictureGame_Tutorial()
    {
        SceneManager.LoadScene("Tutorial_Game3");
    }

    public void star2Page()
    {
        butoon_effect();
        SceneManager.LoadScene("Start2");
    }

    public void StartUpPage()
    {
        SceneManager.LoadScene("StartUpPage");
    }

    public void HomePage()
    {
        SceneManager.LoadScene("HomePage");
    }

    public void Chapter1()
    {
        SceneManager.LoadScene("Chapter1");
    }

    public void Chapter2()
    {
        SceneManager.LoadScene("Chapter2");
    }

    public void Chapter3()
    {
        SceneManager.LoadScene("Chapter3");
    }

    public void Chapter4()
    {
        butoon_effect();
        SceneManager.LoadScene("Chapter4");
    }

    public void Chapter5()
    {
        SceneManager.LoadScene("Chapter5");
    }

    public void Chapter6()
    {
        SceneManager.LoadScene("Chapter6");
    }

    public void Chapter7()
    {
        SceneManager.LoadScene("Chapter7");
    }


    public void Game3()
    {
        butoon_effect();
        SceneManager.LoadScene("Game3");
    }

    public void FaceDetection()
    {
        SceneManager.LoadScene("FaceDetection");
    }

    public void ColorPicker()
    {
        SceneManager.LoadScene("ColorPicker");
    }

    public void DocumentScanner()
    {
        SceneManager.LoadScene("DocumentScanner");
    }

    public void QrReader()
    {
        SceneManager.LoadScene("QrReader");
    }

    public void ObjectDetectionMNSSD()
    {
        SceneManager.LoadScene("ObjectDetectionMNSSD");
    }

    public void ObjectDetectionYoloV4()
    {
        SceneManager.LoadScene("ObjectDetectionYoloV4");
    }
    public void MarkerLessAR()
    {
        SceneManager.LoadScene("MarkerLessAR");
    }


}
