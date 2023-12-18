using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoEmotion : MonoBehaviour
{
    public SendDataEp sendDataEp;

    public TextMeshProUGUI emotionTMP;

    private string pastEmotion = "";

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {   
        string emotion = emotionTMP.text;
        sendDataEp.UpdateEmotion(emotion);
        if(emotion!=pastEmotion)
        {
            sendDataEp.Send();
        }        
        pastEmotion = emotion;
    }
    
}
