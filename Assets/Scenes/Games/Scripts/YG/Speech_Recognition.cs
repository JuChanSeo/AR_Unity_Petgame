using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[RequireComponent (typeof (AudioSource))]

public class Speech_Recognition : MonoBehaviour {
    struct ClipData{
        public int samples;
    }
    

    const int HEADER_SIZE = 44;
    private int minFreq;
    private int maxFreq;
    private bool micConnected = false;
    private AudioSource goAudioSource;

    private bool is_speaking; 
    public TextMeshProUGUI user_text; 



    public Button speak; 
    public Button stop; 

    // 본인 프로젝트 api key 입력
    public string apiKey="AIzaSyBulIpjir1WwDg1E0sMtADThiCSHZSQbdc";

	void Start () {
        

        Button btn= speak.GetComponent<Button>();
        btn.onClick.AddListener(SpeakOnClicked);

        Button btn_stop= stop.GetComponent<Button>();
        btn_stop.onClick.AddListener(StopOnClicked);
        
        // 연결된 마이크가 있는지 확인 
        if(Microphone.devices.Length <= 0){
                // 마이크 없음
                Debug.LogWarning("Microphone not connected!");
        }
        else{
            Debug.LogWarning("Microphone connected!");
                micConnected = true;
                Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);
                if(minFreq == 0 && maxFreq == 0){
                        maxFreq = 44100;
                }
                goAudioSource = this.GetComponent<AudioSource>();
        }
	}

    void SpeakOnClicked(){
        is_speaking=true;
        Recording();

    }

    void StopOnClicked(){
        is_speaking=false;
        Recording();

    }

    void Recording(){
        if(micConnected){
            if(!Microphone.IsRecording(null)){
                if(is_speaking){
                        //Currently set for a n second clip
                        goAudioSource.clip = Microphone.Start( null, false, 30, maxFreq);
                }
            }else{
                if(!is_speaking){
                    float filenameRand = UnityEngine.Random.Range (0.0f, 10.0f);
                    string filename = "testing" + filenameRand;
                    Microphone.End(null); //Stop the audio recording
                    Debug.Log( "Recording Stopped");
                    if (!filename.ToLower().EndsWith(".wav")){
                            filename += ".wav";
            		}
                var filePath = Path.Combine("testing/", filename);
                filePath = Path.Combine(Application.persistentDataPath, filePath);
                Debug.Log("Created filepath string: " + filePath);

                // 디렉토리 있는지 확인 
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                Temp_Audio.Save (filePath, goAudioSource.clip);

                // 본인 api key append 하여 send 
                string lang="Kor";
                string url=$"https://naveropenapi.apigw.ntruss.com/recog/v1/stt?lang={lang}";
                // HttpWebRequest request =(HttpWebRequest)WebRequest.Create(url);
                string Response;
                Response = HttpUploadFile(url, filePath, "file", "audio/wav; rate=44100");
                Debug.Log(Response);
                AudioSource speech_audio;
                speech_audio=goAudioSource.GetComponent<AudioSource>();
                speech_audio.Play();
                // string apiURL = "http://www.google.com/speech-api/v2/recognize?output=json&lang=en-us&key=" + apiKey;
                // string Response;
                // Response = HttpUploadFile (apiURL, filePath, "file", "audio/wav; rate=44100");

                // var jsonresponse = SimpleJSON.JSON.Parse(Response);

                // if (jsonresponse != null) {		
                //     string resultString = jsonresponse ["result"] [0].ToString ();
                //     var jsonResults = SimpleJSON.JSON.Parse (resultString);
                //     string transcripts = jsonResults ["alternative"] [0] ["transcript"].ToString ();
                //     }
                // //일시 저장한 파일 삭제
                File.Delete(filePath);
                }
            }
          }else{
            //마이크가 없는 경우 Warning 띄우기
            Debug.Log("No Mic");
          }
    }

    public string HttpUploadFile(string url, string file, string paramName, string contentType) {
        string FilePath=file;
        FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        byte[] fileData = new byte[fs.Length];
        fs.Read(fileData, 0, fileData.Length);
        fs.Close();

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("X-NCP-APIGW-API-KEY-ID", "6dknphwwnh");
        request.Headers.Add("X-NCP-APIGW-API-KEY", "DjBYuME5zRgmhHuOYvFwVQuoFGhAChyFJDBD6dYQ");
        request.Method = "POST";
        request.ContentType = "application/octet-stream";
        request.ContentLength = fileData.Length;
        using (Stream requestStream = request.GetRequestStream())
        {
            requestStream.Write(fileData, 0, fileData.Length);
            requestStream.Close();
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream);
        string result =  string.Format("{0}", reader.ReadToEnd());
        stream.Close();
        response.Close();
        reader.Close();
        Console.WriteLine(result[0]);
        var jsonresponse = SimpleJSON.JSON.Parse(result);

        user_text.text=jsonresponse["text"];
        if (result.Contains("안녕")){
            play_compliment();
        }

        return result;
    }

    void play_compliment(){
        Debug.Log("칭찬합니당");
    }



    // public string HttpUploadFile_temp(string url, string file, string paramName, string contentType) {
    //         HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
    //         Stream rs = wr.GetRequestStream();
    //         FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
    //         byte[] buffer = new byte[4096];
    //         int bytesRead = 0;
    //         while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0) {
    //                 rs.Write(buffer, 0, bytesRead);
    //         }
    //         fileStream.Close();
    //         rs.Close();

    //         Debug.Log(url);
    //         // System.Net.ServicePointManager.Expect100Continue = false;

    //         // wr.UseDefaultCredentials = true;
    //         // wr.PreAuthenticate = true;
    //         wr.Headers.Add("X-NCP-APIGW-API-KEY-ID", "6dknphwwnh");
    //         wr.Headers.Add("X-NCP-APIGW-API-KEY", "DjBYuME5zRgmhHuOYvFwVQuoFGhAChyFJDBD6dYQ");
    //         // wr.Headers.Add("X-NCP-APIGW-API-KEY-ID", "6dknphwwnh");
    //         // wr.Headers.Add("X-NCP-APIGW-API-KEY", "DjBYuME5zRgmhHuOYvFwVQuoFGhAChyFJDBD6dYQ");
    //         wr.Method = "POST";
    //         wr.ContentType = "audio/l16; rate=44100";
    //         wr.ContentLength=buffer.Length;

    //         // wr.KeepAlive = true;
    //         // wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            

    //         WebResponse wresp = null;
    //         try {
    //             wresp = wr.GetResponse();
    //             Stream stream2 = wresp.GetResponseStream();
    //             StreamReader reader2 = new StreamReader(stream2);

    //             string responseString =  string.Format("{0}", reader2.ReadToEnd());
    //             Debug.Log(responseString);
    //             return responseString;
    //         } catch(Exception ex) {
    //             Debug.Log(string.Format("Error uploading file error: {0}", ex));
    //             if(wresp != null) {
    //                     wresp.Close();
    //                     wresp = null;
    //                     return "Error";
    //             }
    //         } finally {
    //             wr = null;
    //         }
    //         return "empty";
    // }
 }