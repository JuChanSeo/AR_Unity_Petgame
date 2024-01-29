using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;  // List<>을 사용하기 위한 using 문 추가
using Newtonsoft.Json;
using TMPro;

[System.Serializable]
public class ButtonClickData
{
    public List<string> buttonNames;
}

public class ButtonClick : MonoBehaviour
{
    public Button Selection1; 
    public Button Selection2; 
    public Button Selection3; 
    public Button Selection4; 

    public Button[] buttons; // 버튼들의 배열
    // public List<Button> buttons = new List<Button>();
    // public Button nextButton;

    private List<string> selectedAnswers; // 선택한 답변을 저장하기 위한 리스트

    public Color normalColor; // 정상 상태의 색상 ;흰색? RGBA(1.000,1.000,1.000,1.000)
    public Color pressedColor; // 눌린 상태의 색상 // RGBA (0.357,0.683,0.708,0.000)
    public Color newColor = Color.blue;

    
    private bool[] isPressed = new bool[4]; // 각 버튼의 누른 상태를 저장하는 배열

    public string answer;
    public string filePath = Application.streamingAssetsPath + "/SavedButton.txt";
    public string saveFilePath = "jjjson.json";

    public TextAsset textFile;

    // public GameObject[] options;
    public TMP_Text[] options;

    public int currentQuestion;

    public TMP_Text QuestionTxt;
    public TextMeshProUGUI postAnswer;

    // [System.Serializable]
    public class QuestionAndAnswersss
    {
        public string Question;
        public string[] Answers;
    }

    public List<QuestionAndAnswersss> QnAList = new List<QuestionAndAnswersss>(); // 질문과 답변들을 저장할 리스트

    public TextAsset csvFile;
    public List<CSVData> dataList = new List<CSVData>();

    public AudioSource audioSource;

    private void PlayQuestionAudio(string audioFileName)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(audioFileName);
        if (audioClip != null)
        {
            // Debug.Log(audioClip.name);
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip not found: " + audioFileName);
        }
    }

    private void OnAudioClipComplete()
    {
        Debug.Log("Audio clip playback complete.");
        Debug.Log("dongik");
    }

    void Start()
    {   
        isPressed = new bool[4]; // 버튼 배열과 같은 크기의 상태 배열 생성 및 초기화


        // 초기 버튼 색상 저장
        // normalColor = Selection1.colors.normalColor; // (0.495, 0.601, 1.000, 1.000)
        normalColor = Selection1.GetComponent<Image>().color;
        pressedColor = new Color(0.7f, 0.7f, 1f, 0.9f);

        // Debug.Log(normalColor);

        selectedAnswers = new List<string>(); // 리스트 초기화

        LoadQuestionAndAnswers();
        LoadCSVData();
        makeQuestion();
    }

    void LoadCSVData()
    {
        if (csvFile != null)
        {
            // string[] lines = csvFile.text.Split('\n'); // CSV 파일의 내용을 줄 단위로 분할하여 배열로 저장
            string[] lines = Regex.Split(csvFile.text, "\r\n|\r|\n"); // 줄 바꿈 문자로 분리

            foreach (string line in lines)
            {
                string[] data = line.Split(','); 

                if (data.Length >= 12) // CSV 파일의 필드 수에 맞게 수정
                {
                    CSVData csvData = new CSVData();
                    csvData.SceneNumber = data[0];
                    csvData.QuestionNumber = int.Parse(data[1]);
                    csvData.Category1 = data[2];
                    csvData.Category2 = data[3];
                    csvData.Category3 = data[4];
                    csvData.Question = data[5];
                    csvData.Answers = new string[] { data[6], data[7], data[8], data[9] };
                    csvData.Concat = data[10];
                    csvData.AudioFileName = data[11];

                    dataList.Add(csvData);
                }
                else
                {
                    // Debug.LogWarning("잘못된 데이터 형식: " + line);
                }
            }

        }
        else
        {
            Debug.LogError("CSV 파일을 로드할 수 없습니다.");
        }
    }

    void LoadQuestionAndAnswers()
    {
        if (textFile != null)
        {
            string[] lines = textFile.text.Split('\n'); // 텍스트 파일의 내용을 줄 단위로 분할하여 배열로 저장
            foreach (string line in lines)
            {
                string[] data = line.Split('#');
                if (data.Length >= 2)
                {
                    QuestionAndAnswersss qna = new QuestionAndAnswersss();
                    qna.Question = data[0]; // 변수 이름은 대소문자를 구분합니다!! 
                    qna.Answers = new string[data.Length - 1];

                    for (int i = 1; i < data.Length; i++)
                    {
                        qna.Answers[i - 1] = data[i];
                    }
                    QnAList.Add(qna);
                }
            }
        }
        else
        {
            Debug.LogError("텍스트 파일을 로드할 수 없습니다.");
        }

        /** 질문 답변 출력
        foreach (QuestionAndAnswersss qna in QnAList)
        {
            Debug.Log("질문: " + qna.Question);

            for (int i = 0; i < qna.Answers.Length; i++)
            {
                Debug.Log("답변 " + (i + 1) + ": " + qna.Answers[i]);
            }
        }
        **/
    }

    public void makeQuestion()
    {
        currentQuestion = UnityEngine.Random.Range(0, dataList.Count);
        QuestionTxt.text = dataList[currentQuestion].Question;
        
        SetAnswers();

        
        string currentQuestionAudioFileName = dataList[currentQuestion].AudioFileName;
        PlayQuestionAudio(currentQuestionAudioFileName);
    }

    public void SetAnswers()
    {
        for(int i=0; i<options.Length; i++)
        {
            // options[i].transform.GetChild(0).GetComponent<Text>().text = QnAList[currentQuestion].Answers[i];
            options[i].text = dataList[currentQuestion].Answers[i];
        }
        Debug.Log("SceneNumber : " + dataList[currentQuestion].SceneNumber + " QuestionNumber : " + dataList[currentQuestion].QuestionNumber +
        " Question : " + dataList[currentQuestion].Question + " AudioFileName : " + dataList[currentQuestion].AudioFileName);
    }

    public void button1clicked()
    {
        GameObject Selection_btn1 = Selection1.gameObject;
        // Selection_btn1.onClick.AddListener

        answer = Selection1.GetComponentInChildren<TMP_Text>().text;
        this.postAnswer.text = answer;
        selectedAnswers.Add(answer);
        Selection1.onClick.AddListener(() => ToggleAnswerSelection(answer));
    }

    public void button2clicked()
    {
        answer = Selection2.GetComponentInChildren<TMP_Text>().text;
        this.postAnswer.text = answer;
        selectedAnswers.Add(answer);
        Selection1.onClick.AddListener(() => ToggleAnswerSelection(answer));
    }

    public void button3clicked()
    {
        answer = Selection3.GetComponentInChildren<TMP_Text>().text;
        this.postAnswer.text = answer;
        selectedAnswers.Add(answer);
        Selection1.onClick.AddListener(() => ToggleAnswerSelection(answer));
    }

    public void button4clicked()
    {
        GameObject Selection_btn4 = Selection4.gameObject;
        answer = Selection4.GetComponentInChildren<TMP_Text>().text;
        this.postAnswer.text = answer;
        selectedAnswers.Add(answer);
        Selection1.onClick.AddListener(() => ToggleAnswerSelection(answer));
    }



/**
    public void SetQuestion(string question, string[] answers)
    {
        // 질문 텍스트 설정
        questionText.text = question;

        // 보기 답변 버튼에 새로운 답변 옵션 설정
        for (int i = 0; i < 4; i++)
        {
            int answerIndex = i;

            if (i < answers.Length)
            {
                // 각 보기 답변 버튼에 클릭 이벤트 핸들러 등록
                buttons[i].onClick.AddListener(() => ToggleAnswerSelection(answers[answerIndex]));

                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<Text>().text = answers[i];
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
**/


    private void ToggleAnswerSelection(string answer)
    {
        // 사용자가 보기 답변을 선택 또는 취소한 경우 실행
        if (selectedAnswers.Contains(answer))
        {
            selectedAnswers.Remove(answer); // 이미 선택된 답변이면 취소
        }
        else
        {
            selectedAnswers.Add(answer); // 선택되지 않은 답변이면 선택
        }
        // Debug.Log(selectedAnswers.Count);  // selectedAnswers.Count
    }


/**
    public void SaveButtonTexts()
    {
        List<string> buttonTexts = new List<string>();

        // 선택된 버튼들의 텍스트를 가져와서 리스트에 추가
        foreach (Button button in buttons)
        {
            if (button.interactable && button.gameObject.activeInHierarchy)
            {
                buttonTexts.Add(button.GetComponentInChildren<Text>().text);
            }
        }

        // 리스트의 요소들을 문자열로 변환하여 파일에 추가 저장
        string textToSave = string.Join(",", buttonTexts.ToArray());
        File.AppendAllText(filePath, textToSave + Environment.NewLine);
    }
    **/

    public void NextQuestion() // 다음 버튼 클릭한 경우
    {
        if (selectedAnswers.Count > 0)
        {
            // 선택한 답변을 파일에 저장합니다.
            foreach (string answer in selectedAnswers)
            {
                Debug.Log(answer);
                //File.AppendAllText(filePath, answer + " ");
                
                // string json = JsonUtility.ToJson(answer, true);
                // File.WriteAllText(saveFilePath, json);

            }
            //File.AppendAllText(filePath, "SceneNumber : " + dataList[currentQuestion].SceneNumber + " QuestionNumber : " + dataList[currentQuestion].QuestionNumber +
            //" Question : " + dataList[currentQuestion].Question + " AudioFileName : " + dataList[currentQuestion].AudioFileName + Environment.NewLine);

            selectedAnswers.Clear();
        }
    }

    public void ToggleButton1(int buttonIndex)
    {
        isPressed[buttonIndex] = !isPressed[buttonIndex]; // 해당 버튼의 상태를 반전

        // Color normalColor = Selection1.GetComponent<Image>().color;
        // Selection1.GetComponent<Image>().color = normalColor * Selection1.colors.pressedColor;
        // Selection1.GetComponent<Image>().color = normalColor;

        if (isPressed[buttonIndex])
        {
            // 버튼이 눌린 상태일 때의 처리
            Selection1.GetComponent<Image>().color = pressedColor;
            // Selection1.image.color = pressedColor;
            
            // Debug.Log("Button " + buttonIndex + " is pressed.");
        }
        else
        {
            // 버튼이 눌리지 않은 상태일 때의 처리
            Selection1.GetComponent<Image>().color = normalColor;
            // Selection1.image.color = normalColor;
            
            // Debug.Log("Button " + buttonIndex + " is released.");
        }
    }

    public void ToggleButton2(int buttonIndex)
    {
        isPressed[buttonIndex] = !isPressed[buttonIndex]; // 해당 버튼의 상태를 반전

        if (isPressed[buttonIndex])
        {
            // 버튼이 눌린 상태일 때의 처리
            Selection2.image.color = pressedColor;
            // Debug.Log("Button " + buttonIndex + " is pressed.");
        }
        else
        {
            // 버튼이 눌리지 않은 상태일 때의 처리
            Selection2.image.color = normalColor;
            // Debug.Log("Button " + buttonIndex + " is released.");
        }
    }

    public void ToggleButton3(int buttonIndex)
    {
        isPressed[buttonIndex] = !isPressed[buttonIndex]; // 해당 버튼의 상태를 반전

        if (isPressed[buttonIndex])
        {
            // 버튼이 눌린 상태일 때의 처리
            Selection3.image.color = pressedColor;
            // Debug.Log("Button " + buttonIndex + " is pressed.");
        }
        else
        {
            // 버튼이 눌리지 않은 상태일 때의 처리
            Selection3.image.color = normalColor;
            // Debug.Log("Button " + buttonIndex + " is released.");
        }
    }

    public void ToggleButton4(int buttonIndex)
    {
        isPressed[buttonIndex] = !isPressed[buttonIndex]; // 해당 버튼의 상태를 반전

        if (isPressed[buttonIndex])
        {
            // 버튼이 눌린 상태일 때의 처리
            Selection4.image.color = pressedColor;
            // Debug.Log("Button " + buttonIndex + " is pressed.");
        }
        else
        {
            // 버튼이 눌리지 않은 상태일 때의 처리
            Selection4.image.color = normalColor;
            // Debug.Log("Button " + buttonIndex + " is released.");
        }
    }

    public void ResetButtonStates()
    {
        for (int i = 0; i < isPressed.Length; i++)
        {
            isPressed[i] = false;
        }

        /// normalColor = new Color(0.447f, 0.6f, 1f, 1f);
        // normalColor = new Color(0.286f, 0.474f, 0.58f, 1f);

        Selection1.interactable = true;
        Selection1.GetComponent<Image>().color = normalColor;

        Selection2.interactable = true;
        Selection2.image.color = normalColor;

        Selection3.interactable = true;
        Selection3.image.color = normalColor;

        Selection4.interactable = true;
        Selection4.image.color = normalColor;
        
        // Debug.Log("All buttons are released.");
    }
}

public class CSVData
{
    public string SceneNumber;
    public int QuestionNumber;
    public string Category1;
    public string Category2;
    public string Category3;
    public string Question;
    public string[] Answers;
    public string Concat;
    public string AudioFileName;
}

/**
public class ButtonClick : MonoBehaviour
{
    public List<Button> buttons;
    public string saveFilePath = "SavedButton.json";
    //public string saveFilePath = @"C:\Users\SCI Lab\Desktop\디치 유니티\DTx_Game_230519\Assets"+"SavedButton.txt";

    private List<string> selectedButtonNames;



    private StringBuilder stringBuilder;

    private void Awake()
    {
        stringBuilder = new StringBuilder();

        selectedButtonNames = new List<string>();

    }

    public void SaveButtonTexts()
    {
        ButtonClickData clickData = new ButtonClickData();
        clickData.buttonNames = selectedButtonNames;
        // clickData.buttonDataList = new List<ButtonData>();
        
        
        // foreach (Button button in buttons)
        // {
        //     ButtonData buttonData = new ButtonData();
        //     buttonData.buttonName = button.name;
        //     buttonData.buttonText = button.GetComponentInChildren<Text>().text;
        //     clickData.buttonDataList.Add(buttonData);
        // }
        

        string json = JsonUtility.ToJson(clickData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Button texts are saved to JSON file.");
    }

    public void ToggleButton(int buttonIndex)
    {
        Button button = buttons[buttonIndex];

        if (selectedButtonNames.Contains(button.name))
        {
            selectedButtonNames.Remove(button.name);
            Debug.Log("Button " + button.name + " deselected.");
        }
        else
        {
            selectedButtonNames.Add(button.name);
            Debug.Log("Button " + button.name + " selected.");
        }

        SaveButtonTexts(); // 버튼 상태가 변경될 때마다 버튼 이름 저장
    }

**/



    /** 230630 지우고 테스트
    public void SaveButtonTexts()
    {
        // stringBuilder.Clear();

        foreach (Button button in buttons)
        {
            string buttonText = button.GetComponentInChildren<Text>().text;
            stringBuilder.AppendLine(buttonText);
        }

        string allButtonTexts = stringBuilder.ToString();
        File.WriteAllText(saveFilePath, allButtonTexts);
        // System.IO.File.WriteAllText(saveFilePath, allButtonTexts);
    }
    **/