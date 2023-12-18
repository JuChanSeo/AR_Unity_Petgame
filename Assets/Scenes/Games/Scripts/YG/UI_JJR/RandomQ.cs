using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class RandomQ : MonoBehaviour
{   
    public AudioSource girlAudio;
    public TextMeshProUGUI textQuestion;
    private List<Question> questionList;

    [SerializeField]
    public List<string> useSteps;


    private void Start()
    {
        ResetQuestionList();
        RandomQuestion();
    }

    public void RandomQuestion()
    {
        int questionCount = questionList.Count;
        if (questionCount == 0)
        {
            ResetQuestionList();
        }
        int randomIndex = Random.Range(0, questionCount);
        Question randomQ = questionList[randomIndex];
        questionList.Remove(randomQ);
        AudioClip updateClip = Resources.Load<AudioClip>(randomQ.audioFileName);
        girlAudio.clip = updateClip;
        girlAudio.Play();
        textQuestion.text = randomQ.text;
        
    }

    private void ResetQuestionList()
    {
        questionList = new List<Question>();
        CSVRead();
    }

    private void CSVRead()
    {
        TextAsset csvData = Resources.Load<TextAsset>("QuestionListFile");
        string[] data = csvData.text.Split(new char[] { '\n' });
        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[]{ '\t' });
            string sceneName = row[0];
            int num = int.Parse(row[1]);
            string step = row[2];
            string text = row[3];
            string audioFileName = row[4].Split(new char[] {'.'})[0];
            if (useSteps.Contains(step))
            {
                Question question = new(sceneName, num, step, text, audioFileName);
                this.questionList.Add(question);
            }

        }
    }



}
