using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    
    public void Answer()
    {
        if(isCorrect)
        {
            // Debug.Log("correct");
            // quizManager.correct(); 삭제
        }
        else
        {
            // Debug.Log("wrong");
            // quizManager.correct(); 삭제
        }
    }
}

/// 필요 없어서 삭제해도 됨
