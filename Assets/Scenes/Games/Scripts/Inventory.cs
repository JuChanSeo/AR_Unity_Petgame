using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;

[Serializable]
public class inven_data
{
    public string item_name;
    public bool is_sold;
}

public class Inventory : MonoBehaviour
{
    private string m_filePath;
    private BinaryFormatter binaryform = new BinaryFormatter();

    int num_items = 36;
    List<inven_data> datas_inven = new List<inven_data>();
    GameObject curr_clicked_item;

    Player_statu player_statu_script;

    public GameObject inventory_bt;
    public GameObject inventory_UI;
    public GameObject sale_popup;
    public TMPro.TMP_Text coin_text;
    List<string> item_names = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(inventory_UI.transform.position);
        inventory_UI.transform.position = new Vector3(1872, 842, 0);

        m_filePath = Application.persistentDataPath + "/inven.dat";
        Debug.Log(m_filePath);
        if (!File.Exists(m_filePath))
        {
            InitData();
        }
        else
        {
            LoadData();
        }

        player_statu_script = GameObject.Find("player_statu").GetComponent<Player_statu>();
        //datas_inven[5].is_sold = false;

        foreach (var item in datas_inven)
        {
            if(item.is_sold == true)
            {
                GameObject obj = GameObject.Find(item.item_name);
                if (obj == null) Debug.Log(item.item_name);
                obj.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        inventory_UI.SetActive(false);
        sale_popup.SetActive(false);

    }



    public void inventory_bt_clicked()
    {
        if(inventory_UI.gameObject.activeSelf == false)
        {
            inventory_UI.SetActive(true);
        }
    }

    public void exit_bt_clicked()
    {
        if (inventory_UI.gameObject.activeSelf == true)
        {
            inventory_UI.SetActive(false);
        }
    }

    public void SaveData()
    {
        try
        {
            using (Stream ws = new FileStream(m_filePath, FileMode.Create))
            {
                binaryform.Serialize(ws, datas_inven);
                Debug.Log("저장 성공!");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void LoadData()
    {
        using (Stream rs = new FileStream(m_filePath, FileMode.Open))
        {
            datas_inven = (List<inven_data>)binaryform.Deserialize(rs);
        }
        Debug.Log("load_inven 실행");
        Debug.Log($"item_dic 출력, 길이:{datas_inven.Count}");
    }

    public void InitData()
    {
        item_names.Add("001_Ball_Catch+Jump");//1
        item_names.Add("013_Ball_Roll");//2
        item_names.Add("014_Ball_Shake");//3
        item_names.Add("022_Charm_Bark");//4
        item_names.Add("024_Charm_Jump");//5
        item_names.Add("028_Charm_Screen");//6
        item_names.Add("037_Exercise_Boxing_Fall");//7
        item_names.Add("081_Idle_Sniffing");//8
        item_names.Add("093_LieOnBack_Turn");//9
        item_names.Add("097_Lying_Blend_Sit");//10
        item_names.Add("110_Pat_Charm_Left_Loop2");//11
        item_names.Add("120_Pat_Flat_Annoyed_R");//12
        item_names.Add("156_Pat_Hug");//13
        item_names.Add("159_Pat_Idle_Excited");//14
        item_names.Add("160_Pat_Idle_Excited2");//15
        item_names.Add("184_Pat_Loop");//16
        item_names.Add("249_Playful_Left");//17
        item_names.Add("263_Sit_Blend_LieOnBack");//18
        item_names.Add("279_Skill_Handstand");//19
        item_names.Add("283_Skill_RHand");//20
        item_names.Add("284_Skill_Stand");//21
        item_names.Add("301_Startle");//22
        item_names.Add("303_Stroll_Jump");//23
        item_names.Add("304_Stroll_MaiTu");//24
        item_names.Add("305_Stroll_No");//25
        item_names.Add("CrocodileToy_01");//26
        item_names.Add("Bed_01");//27
        item_names.Add("Bed_02");//28
        item_names.Add("BirdToy_01");//29
        item_names.Add("BoneToy_01");//30
        item_names.Add("HamsterHouse_02");//31
        item_names.Add("donutToy_01");//32
        item_names.Add("FishToy_01");//33
        item_names.Add("FoodBox_02");//34
        item_names.Add("dog_bed_01");//35
        item_names.Add("petbowl_03");//36

        for (int i=0; i<num_items; i++)
        {
            Debug.Log(item_names[i]);
            inven_data save_Dat = new inven_data
            {
                item_name = item_names[i],
                is_sold = false
            };
            datas_inven.Add(save_Dat);
        }
        SaveData();
        Debug.Log("init_inven  실행");
    }

    public void show_SalePopup()
    {
        //자물쇠가 걸려 있는 인벤토리를 선택할시.
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
        if (clickedObj.transform.GetChild(1).gameObject.activeSelf == true) //구매 이전
        {
            curr_clicked_item = clickedObj;
            sale_popup.SetActive(true);
        }
        else
        {
            Debug.Log("이미 구매 완료된 상품입니다");
            //뭔가 다른걸 넣어보자, 즉 그 아이템으로 무엇을 할것인지 정해서 넣어보자
        }
    }

    //네, 구매할게요 버튼을 클릭할 경우
    public void purchase_item_bt_clicked() 
    {
       if(curr_clicked_item != null)
        {
            foreach(var item in datas_inven)
            {
                if(item.item_name == curr_clicked_item.name)
                {
                    if (player_statu_script.Coin >= 5)
                    {
                        player_statu_script.Coin -= 5;
                        item.is_sold = true;
                        curr_clicked_item.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("돈이 부족해서 구매가 안됩니다");
                    }
                    PlayerPrefs.SetInt("Coin", player_statu_script.Coin);
                }
            }
        }
        sale_popup.SetActive(false);
        SaveData();
    }

    public void not_purchase_bt_clicked()
    {
        sale_popup.SetActive(false);
    }
}
