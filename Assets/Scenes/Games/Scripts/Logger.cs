using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;

public class send_dic
{
    private string URL = "https://223.130.138.24.nip.io:32163/be.runtime/dev/v1/service/gameTypeB-2/test/pet";
    //private string URL = "https://webhook.site/12733508-03cd-492b-a728-e290ad6fbe4d";
    Dictionary<string, string> data_in_class = new Dictionary<string, string>()
    {
        {"time", DateTime.Now.ToString()},
        {"event", "0" },
        {"user_id", " " },
        {"PW", "0" },
        {"pet_name", "0" },
        {"energy", "0" },
        {"fatigue", "0" },
        {"cleanliness", "0" },
        {"intimity", "0" },
        {"coin", "0" },
        {"gameLv_c1", "0" },
        {"gameLv_c2", "0" },
        {"gameLv_c3", "0" },
    };
    List<List<IMultipartFormSection>> formdata_send = new List<List<IMultipartFormSection>>();


    //public Dictionary<string, string> data_format = new Dictionary<string, string>();

    public void insert_data(string eve_txt)
    {
        var data_format = new Dictionary<string, string>(data_in_class);
        data_format["time"] = DateTime.Now.ToString();
        data_format["event"] = eve_txt;
        data_format["user_id"] = PlayerPrefs.GetString("ID");
        data_format["PW"] = PlayerPrefs.GetString("Password");
        data_format["pet_name"] = PlayerPrefs.GetString("PetName");
        data_format["energy"] = PlayerPrefs.GetFloat("energy").ToString();
        data_format["fatigue"] = PlayerPrefs.GetFloat("fatigue").ToString();
        data_format["cleanliness"] = PlayerPrefs.GetFloat("cleanliness").ToString();
        data_format["intimity"] = PlayerPrefs.GetFloat("intimity").ToString();
        data_format["coin"] = PlayerPrefs.GetInt("Coin").ToString();
        data_format["gameLv_c1"] = PlayerPrefs.GetInt("Level_c1").ToString();
        data_format["gameLv_c2"] = PlayerPrefs.GetInt("Level_c2").ToString();
        data_format["gameLv_c3"] = PlayerPrefs.GetInt("Level_c3").ToString();

        List<IMultipartFormSection> formdata = new List<IMultipartFormSection>();
        foreach (KeyValuePair<string, string> kv in data_format)
        {
            //Debug.Log(kv.Key + "\t" + kv.Value);
            formdata.Add(new MultipartFormDataSection(kv.Key, kv.Value));
            //formdata.AddField(kv.Key, kv.Value);
            //Debug.Log(kv.Key + ":\t" + kv.Value);
        }
        formdata_send.Add(formdata);
        //if(formdata.Count == data_format.Count)
        //{
        //    formdata_send.Add(formdata);
        //}

    }


    public IEnumerator cor_send_to_server()
    {
        Debug.Log(("동작 확인1"));
        while (true)
        {
            Debug.Log("동작 확인2");
            if (formdata_send.Count == 0)
            {
                Debug.Log("No data to send");
            }
            else
            {
                Debug.Log("data 길이:" + formdata_send.Count);
            }


            for (int i = 0; i < formdata_send.Count; i++)
            {
                using (UnityWebRequest request = UnityWebRequest.Post(URL, formdata_send[i]))
                {
                    yield return request.SendWebRequest();

                    if (request.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.Log(request.error);
                    }
                    else if (request.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.Log(request.error);
                    }
                    else if (request.result == UnityWebRequest.Result.DataProcessingError)
                    {
                        Debug.Log(request.error);
                    }
                    else if (request.result == UnityWebRequest.Result.InProgress)
                    {
                        Debug.Log(request.error);
                    }
                    else
                    {
                        Debug.Log("성공!" + request.downloadHandler.text);
                        formdata_send.Clear();

                    }
                }
            }
            yield return new WaitForSecondsRealtime(5f);
        }

    }

}


public class Logger : MonoBehaviour
{
    Player_statu player_statu_script;
    public send_dic logger_master = new send_dic();
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = logger_master.cor_send_to_server();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }



    // 읽고 쓰기는 되는데 정작 파일로 저장은 안 된다... 정녕 서버 저장밖에 답이 없는 것인가...
    public string pathForFile (string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return Path.Combine(Application.persistentDataPath, filename);
        }
        else
        {
            return null;
        }
    }

    public void writeStringToFile (string str, string filename)
    {
        string path = pathForFile(filename);
        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(str);

        sw.Close();
        file.Close();
    }

    public string readStringFormFile(string filename)
    {
        string path = pathForFile(filename);

        if(File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string str;
            str = sr.ReadLine();

            sr.Close();
            file.Close();

            return str;
        }
        else
        {
            return null;
        }
    }
}
 