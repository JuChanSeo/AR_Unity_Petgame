using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home_bt : MonoBehaviour
{
    public GameObject home_bt;
    Contents1 c1_script;
    Contents2 c2_script;
    Contents3 c3_script;
    Contents4 c4_script;
    Petctrl petctrl_script;

    private void Start()
    {
        c1_script = GameObject.Find("Scripts").GetComponent<Contents1>();
        c2_script = GameObject.Find("Scripts").GetComponent<Contents2>();
        c3_script = GameObject.Find("Scripts").GetComponent<Contents3>();
        c4_script = GameObject.Find("Scripts").GetComponent<Contents4>();
        petctrl_script = GameObject.Find("Scripts").GetComponent<Petctrl>();
    }

    private void Update()
    {
        
    }
    public void home_bt_click()
    {
        //c1_script.re_init();
        //c2_script.sleep_bt_reset();
        //c3_script.re_init();
        //c4_script.re_init();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
