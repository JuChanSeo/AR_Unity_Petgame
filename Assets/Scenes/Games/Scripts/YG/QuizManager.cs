using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;


public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    // public UnityEngine.UI.Text QuestionTxt;
    public TMP_Text QuestionTxt;
    // public Text ScoreText;

    // public GameObject QuizPanel; ///////////////////// panel
    // public GameObject GoPanel; //// panel

    // int totalQuestions = 0;
    // public int score;

    private void Start()
    {
        // totalQuestions = QnA.Count;
        // GoPanel.SetActive(false);
         // makeQuestion(); 230802 테스트
    }

    void makeQuestion()
    {
        if(QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            SceneManager.LoadScene("Task2");
        }
    }

    void SetAnswers()
    {
        for(int i=0; i<options.Length; i++)
        {
            /** string clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
            Debug.Log(clickedButtonName); // 230517 동익 클릭한 버튼 출력**/

            options[i].GetComponent<AnswerScript>().isCorrect = false;
            // options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            options[i].GetComponentInChildren<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        makeQuestion();
    }



}
