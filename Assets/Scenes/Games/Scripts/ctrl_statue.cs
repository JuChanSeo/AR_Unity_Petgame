using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ctrl_statue : MonoBehaviour
{
    string ID;
    string PW;
    string PetName;


    public TMP_InputField ID_input;
    public TMP_InputField PW_input;
    public TMP_InputField PetName_input;
    public GameObject panel_hungry;
    public GameObject panel_sleep;
    public GameObject panel_bath;



    Player_statu player_statu_script;

    // Start is called before the first frame update
    void Start()
    {
        player_statu_script = GameObject.Find("player_statu").GetComponent<Player_statu>();

        TMP_Text tmp_text_h = panel_hungry.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();
        TMP_Text tmp_text_s = panel_sleep.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();
        TMP_Text tmp_text_b = panel_bath.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();

        tmp_text_h.text = player_statu_script.Level_hungry.ToString();
        tmp_text_s.text = player_statu_script.Level_sleep.ToString();
        tmp_text_b.text = player_statu_script.Level_bath.ToString();

        ID = player_statu_script.ID;
        PW = player_statu_script.Password;
        PetName = player_statu_script.PetName;

        ID_input.text = ID;
        PW_input.text = PW;
        PetName_input.text = PetName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void inc_bt_clicked_hungry()
    {
        TMP_Text tmp_text = panel_hungry.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();
        string num_text = tmp_text.text;        
        int level = int.Parse(num_text);
        if (level < 3) level += 1;

        player_statu_script.Level_hungry = level;
        PlayerPrefs.SetInt("Level_c1", player_statu_script.Level_hungry);
        tmp_text.text = level.ToString();
    }

    public void dec_bt_clicked_hungry()
    {
        TMP_Text tmp_text = panel_hungry.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();
        string num_text = tmp_text.text;
        int level = int.Parse(num_text);
        if(level > 1) level -= 1;

        player_statu_script.Level_hungry = level;
        PlayerPrefs.SetInt("Level_c1", player_statu_script.Level_hungry);
        tmp_text.text = level.ToString();
    }

    public void inc_bt_clicked_sleep()
    {
        TMP_Text tmp_text = panel_sleep.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();
        string num_text = tmp_text.text;
        int level = int.Parse(num_text);
        if (level < 3) level += 1;

        player_statu_script.Level_sleep = level;
        PlayerPrefs.SetInt("Level_c2", player_statu_script.Level_sleep);
        tmp_text.text = level.ToString();
    }

    public void dec_bt_clicked_sleep()
    {
        TMP_Text tmp_text = panel_sleep.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();
        string num_text = tmp_text.text;
        int level = int.Parse(num_text);
        if (level > 1) level -= 1;

        player_statu_script.Level_sleep = level;
        PlayerPrefs.SetInt("Level_c2", player_statu_script.Level_sleep);
        tmp_text.text = level.ToString();
    }
    public void inc_bt_clicked_bath()
    {
        TMP_Text tmp_text = panel_bath.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();
        string num_text = tmp_text.text;
        int level = int.Parse(num_text);
        if (level < 3) level += 1;

        player_statu_script.Level_bath = level;
        PlayerPrefs.SetInt("Level_c3", player_statu_script.Level_bath);
        tmp_text.text = level.ToString();
    }

    public void dec_bt_clicked_bath()
    {
        TMP_Text tmp_text = panel_bath.gameObject.transform.GetChild(3).transform.GetComponent<TMP_Text>();
        string num_text = tmp_text.text;
        int level = int.Parse(num_text);
        if (level > 1) level -= 1;

        player_statu_script.Level_bath = level;
        PlayerPrefs.SetInt("Level_c3", player_statu_script.Level_bath);
        tmp_text.text = level.ToString();
    }

    public void save_bt_clicked()
    {
        if(ID_input.text == "" || PW_input.text == "" || PetName_input.text == "")
        {
            return;
        }
        else
        {
            PlayerPrefs.SetString("ID", ID_input.text);
            PlayerPrefs.SetString("Password", PW_input.text);
            PlayerPrefs.SetString("PetName", PetName_input.text);
        }

        SceneManager.LoadScene("Chapter1");
    }

    public void coin_10000()
    {
        PlayerPrefs.SetInt("Coin", 10000);
    }

    public void plus_statu()
    {

    }

    public void minus_statu()
    {

    }

}
