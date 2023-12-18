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
    bool flag_scan_mode;
    private string m_filePath;
    private BinaryFormatter binaryform = new BinaryFormatter();

    int num_items = 36;
    List<inven_data> datas_inven = new List<inven_data>();
    GameObject curr_clicked_item;
    public List<GameObject> toy_objs;

    Player_statu player_statu_script;
    Petctrl petctrl_script;
    Logger logger_script;

    public GameObject inventory_bt;
    public GameObject inventory_UI;
    public GameObject sale_popup;
    public TMPro.TMP_Text coin_text;
    List<string> item_names = new List<string>();

    Vector2 Center_device;
    RaycastHit hit;

    private GameObject copyed_obj;

    // Start is called before the first frame update
    void Start()
    {
        Center_device = new Vector2(Screen.width / 2f, Screen.height / 2f);
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
        petctrl_script = GameObject.Find("Scripts").GetComponent<Petctrl>();
        logger_script = GameObject.Find("Scripts").GetComponent<Logger>();
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

    private void Update()
    {
        if(flag_scan_mode)
        {
            scan_mode();
        }
    }


    public void scan_mode()
    {
        var ray = Camera.main.ScreenPointToRay(Center_device);
        var hasHit = Physics.Raycast(ray, out hit, float.PositiveInfinity);
        if (hasHit && hit.transform.name.StartsWith("Mesh"))
        {
            if(copyed_obj != null)
            {
                Debug.Log("copyed obj is not null obj");
                copyed_obj.transform.position = hit.point + 0.1f*Vector3.up;
                copyed_obj.transform.rotation = new Quaternion(0f, Camera.main.transform.rotation.y + 180f, 0f, 0f);
            }
            else
            {
                copyed_obj = GameObject.Find(curr_clicked_item.name);
            }
        }

        if(Input.touchCount > 0)//터치가 발생하면
        {
            Instantiate(copyed_obj, hit.point + 0.1f * Vector3.up, new Quaternion(0f, Camera.main.transform.rotation.y + 180f, 0f, 0f));
            petctrl_script.not_move_pet = false;
            copyed_obj.transform.position = new Vector3(-1000f, -1000f, -1000f);
            copyed_obj = null;
            flag_scan_mode = false;
        }

    }
    public void inventory_bt_clicked()
    {
        if(inventory_UI.gameObject.activeSelf == false)
        {
            logger_script.logger_master.insert_data("인벤토리 버튼 클릭");
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
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
        curr_clicked_item = clickedObj;
        //자물쇠가 걸려 있는 인벤토리를 선택할시.
        if (clickedObj.transform.GetChild(1).gameObject.activeSelf == true)
        {
            sale_popup.SetActive(true);
        }
        else //자물쇠 표시가 없는 아이템을 선택할 시(구매 완료된 상품)
        {
            Debug.Log("이미 구매 완료된 상품입니다");
            inventory_UI.SetActive(false);
            if(clickedObj.CompareTag("item_anim"))
            {
                logger_script.logger_master.insert_data($"인벤토리 애니메이션 실행: {clickedObj.name}");
                //Debug.Log($"인벤토리 애니메이션 실행: {clickedObj.name}");
                petctrl_script.play_anim_and_idel(clickedObj.name);
            }
            else if(clickedObj.CompareTag("item_obj"))
            {
                logger_script.logger_master.insert_data($"인벤토리 아이템 배치: {clickedObj.name}");
                flag_scan_mode = true;
                //copyed_obj = Instantiate(GameObject.Find(clickedObj.name));
                //copyed_obj.SetActive(true);
                petctrl_script.not_move_pet = true;
            }
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
                        logger_script.logger_master.insert_data($"아이템 구매 완료:{item.item_name}");
                    }
                    else
                    {
                        logger_script.logger_master.insert_data("구매 실패. 코인 부족");
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
