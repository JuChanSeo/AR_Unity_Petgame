using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scene30Answer : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI speechRecg;
    public TextMeshProUGUI answerTMP;
    public string pastSpeech = "";

    // Update is called once per frame
    void Update()
    {   
        string speech = speechRecg.text;
        if (speech != pastSpeech)
        {
            answerTMP.text = speech;
            pastSpeech = speech;
        }
        
    }
}
