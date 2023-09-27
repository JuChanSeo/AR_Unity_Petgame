using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_statu : MonoBehaviour
{
    public string PetName;
    public string ID;
    public string Password;

    public int Level_pet;
    public int Level_hungry;
    public int Level_sleep;
    public int Level_bath;
    public int Level_c4;
    public int Coin;
    public float energy;
    public float fatigue;
    public float intimity;
    public float cleanliness;

    public InputField ID_input;
    public InputField PW_input;
    
    public float min_y;
    int set_level_to_1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //Load from json
        //Load from playerprefs
        Level_pet = PlayerPrefs.GetInt("Level_pet");
        Level_hungry = PlayerPrefs.GetInt("Level_c1");
        Level_sleep = PlayerPrefs.GetInt("Level_c2");
        Level_bath = PlayerPrefs.GetInt("Level_c3");
        PetName = PlayerPrefs.GetString("PetName");
        ID = PlayerPrefs.GetString("ID");
        Password = PlayerPrefs.GetString("Password");
        Coin = PlayerPrefs.GetInt("Coin");
        energy = PlayerPrefs.GetFloat("energy"); // 높으면 좋은 것
        fatigue = PlayerPrefs.GetFloat("fatigue");  // 낮으면 좋은 것
        intimity = PlayerPrefs.GetFloat("intimity"); // 높으면 좋은 것
        cleanliness = PlayerPrefs.GetFloat("cleanliness"); // 높으면 좋은것
        set_level_to_1 = PlayerPrefs.GetInt("set_level_to_1");

        //Debug.Log(PetName + "\t" + ID + "\t" + Password + "\t" + energy + "\t" + fatigue);
        //Debug.Log(Level_hungry + "\t" + Level_sleep + "\t" + Level_bath);
        if(set_level_to_1 == 0)
        {
            Level_pet = 1;
            Level_hungry = 1;
            Level_sleep = 1;
            Level_bath = 1;
            PlayerPrefs.SetInt("Level_pet", 1);
            PlayerPrefs.SetInt("Level_c1", 1);
            PlayerPrefs.SetInt("Level_c2", 1);
            PlayerPrefs.SetInt("Level_c3", 1);
            PlayerPrefs.SetInt("set_level_to_1", 1);

            Debug.Log("level initialize");
        }

        if(ID == "" || Password == "" || PetName == "")
        {
            SceneManager.LoadScene("Statue");
        }

        string json = JsonUtility.ToJson(this);
        Debug.Log(json);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Chapter1")
        {
            ID_input.placeholder.GetComponent<Text>().text = PlayerPrefs.GetString("ID");
            PW_input.placeholder.GetComponent<Text>().text = PlayerPrefs.GetString("Password");
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void check_the_level()
    {
        if(true) //어떤 조건이 만족되면 pet의 level을 올려 준다.
        {
            this.Level_pet += 1;
            PlayerPrefs.SetInt("Level_pet", Level_pet);
        }
    }
    // Update is called once per frame

    public void change_statu(float _energy, float _fatigue, float _cleanliness, float _intimity)
    {
        if(energy + _energy < 0)
        {
            energy = 0;
        }
        else if (energy + _energy > 1)
        {
            energy = 1;
        }
        else
        {
            energy += _energy;
        }

        if(fatigue + _fatigue < 0)
        {
            fatigue = 0;
        }
        else if(fatigue + _fatigue > 1)
        {
            fatigue = 1;
        }
        else
        {
            fatigue += _fatigue;
        }

        if (cleanliness + _cleanliness < 0)
        {
            cleanliness = 0;
        }
        else if (cleanliness + _cleanliness > 1)
        {
            cleanliness = 1;
        }
        else
        {
            cleanliness += _cleanliness;
        }

        if (intimity + _intimity < 0)
        {
            intimity = 0;
        }
        else if (intimity + _intimity > 1)
        {
            intimity = 1;
        }
        else
        {
            intimity += _intimity;
        }

        PlayerPrefs.SetFloat("energy", energy);
        PlayerPrefs.SetFloat("fatigue", fatigue);
        PlayerPrefs.SetFloat("intimity", intimity);
    }

    public int choose_higlight()
    {
        float min;
        int min_idx = 0; //enegry:0, fatigue:1, cleanliness:2, intimity:3
        float[] arr = { 0, 0, 0, 0 };
        arr[0] = energy;
        arr[1] = 1 - fatigue;
        arr[2] = intimity;
        arr[3] = cleanliness;

        min = arr[0];
        for(int i = 1; i < 4; i++)
        {
            if(min > arr[i])
            {
                min = arr[i];
                min_idx = i;
            }
        }        
        return min_idx; //enegry:0, fatigue:1, cleanliness:2, intimity:3
    }

    public void check_info()
    {
        if(ID_input.text != ID)
        {
            return;
        }

        if(PW_input.text != Password)
        {
            return;
        }

        PageNavigation pageNavigation_script;
        pageNavigation_script = GameObject.Find("Manager").GetComponent<PageNavigation>();
        pageNavigation_script.Chapter5();

    }

    void Update()
    {

    }

    public void Jsave()
    {

    }

    public void Jload()
    {

    }

    public void Jsend()
    {
        //httprequest library 사용
    }


}
