using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoQA : MonoBehaviour
{
    public SendDataEp sendDataEp;
    public TextMeshProUGUI questionTMP;
    public TextMeshProUGUI answerTMP;

    private string pastAnswer = "";

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {   
        string question = questionTMP.text;
        string answer = answerTMP.text;
        sendDataEp.UpdateQA(question, answer);
        if(answer!=pastAnswer)
        {
            sendDataEp.Send();
        }        
        pastAnswer = answer;
    }
}
