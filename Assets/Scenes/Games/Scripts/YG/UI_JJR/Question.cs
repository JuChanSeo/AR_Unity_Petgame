using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string sceneName;
    public int num;
    public string step;
    public string text;
    public string audioFileName;
    public Question(string sceneName, int num, string step, string text, string audioFileName)
    {
        this.sceneName = sceneName;
        this.num = num;
        this.step = step;
        this.text = text;
        this.audioFileName = audioFileName;
    }
}
