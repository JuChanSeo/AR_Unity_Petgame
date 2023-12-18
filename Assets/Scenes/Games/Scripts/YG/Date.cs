using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Date : MonoBehaviour
{

    public TextMeshProUGUI date; 
    private string day_name;

    // Start is called before the first frame update
    void Start()
    {
        day_name=System.DateTime.Now.ToString("dddd");
        Debug.Log(day_name);
        if (day_name=="Monday"){
            day_name="월요일";
        }else if (day_name=="Tuesday"){
            day_name="화요일";
        }else if (day_name=="Wednesday"){
            day_name="수요일";
        }else if (day_name=="Thursday"){
            day_name="목요일";
        }else if (day_name=="Friday"){
            day_name="금요일";
        }else if (day_name=="Saturday"){
            day_name="토요일";
        }else if (day_name=="Sunday"){
            day_name="일요일";
        }
        date.text=System.DateTime.Now.ToString("yy년 MM월 dd일")+" "+day_name;
        Debug.Log(date.text);
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
