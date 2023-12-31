using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class help_panel : MonoBehaviour
{

    public GameObject help_panel_entire;
    public GameObject[] panels;

    GameObject selected_panel;
    int current_idx;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(help_panel_entire.transform.position);
        help_panel_entire.transform.position = new Vector3(1198, 818, 0);
        time = 0;
        current_idx = 0;
        for (int i = 0; i < panels.Length; i++)
        {
            for (int j = 0; j < panels[i].transform.childCount; j++)
            {
                panels[i].transform.GetChild(j).gameObject.SetActive(false);
                //Debug.Log(panel_buf.transform.GetChild(j).gameObject.transform.name);
            }

            panels[i].SetActive(false);
        }

        help_panel_entire.SetActive(false);
    }

    private void Update()
    {
        if (selected_panel == null || selected_panel.activeSelf == false)
        {
            return;
        }
        time += Time.deltaTime;

        if(time > 5f)
        {
            
            {
                next_bt_clicked();
            }
            time = 0;
        }
    }

    public void help_bt_clicked()
    {
        if (help_panel_entire.activeSelf == true) help_panel_entire.SetActive(false);
        else help_panel_entire.SetActive(true);
    }

    public void hungry_panel_clicked()
    {
        time = 0;
        current_idx = 0;
        selected_panel = panels[0];
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        selected_panel.SetActive(true);
        selected_panel.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void sleep_panel_clicked()
    {
        time = 0;
        current_idx = 0;
        selected_panel = panels[1];
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        selected_panel.SetActive(true);
        selected_panel.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void bath_panel_clicked()
    {
        time = 0;
        current_idx = 0;
        selected_panel = panels[2];
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        selected_panel.SetActive(true);
        selected_panel.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void play_panel_clicked()
    {
        time = 0;
        current_idx = 0;
        selected_panel = panels[3];
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        selected_panel.SetActive(true);
        selected_panel.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void next_bt_clicked()
    {
        time = 0;
        if (selected_panel.activeSelf == false) return;

        selected_panel.transform.GetChild(current_idx).gameObject.SetActive(false);

        if (current_idx+1 == selected_panel.transform.childCount) current_idx = 0;
        else current_idx += 1;

        selected_panel.transform.GetChild(current_idx).gameObject.SetActive(true);
    }

    public void previous_bt_clicked()
    {
        time = 0;
        if (selected_panel.activeSelf == false) return;

        selected_panel.transform.GetChild(current_idx).gameObject.SetActive(false);

        if (current_idx == 0) current_idx = selected_panel.transform.childCount - 1;
        else current_idx -= 1;

        selected_panel.transform.GetChild(current_idx).gameObject.SetActive(true);
    }

}
