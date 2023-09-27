using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logger : MonoBehaviour
{
    Player_statu player_statu_script;
    // Start is called before the first frame update
    void Start()
    {
        
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
 