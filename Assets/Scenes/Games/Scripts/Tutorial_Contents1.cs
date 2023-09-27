using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Tutorial_Contents1 : MonoBehaviour
{
    public bool c1_flag;
    float prev_angle;
    float prev_angle_frame;//속도 계산용
    public List<GameObject> foods = new List<GameObject>();
    List<GameObject> food_selected = new List<GameObject>();
    float[] speed_mv_avg = new float[15];
    int cnt_moving_average;
    public GameObject net;
    Petctrl petctrl_script;
    RaycastHit hit;
    Vector2 Center_device;
    int cnt_corr;
    float time_remain;
    public TMP_Text time_remain_text;
    //public GameObject bt_face;
    public GameObject bt_picture;
    public GameObject bt_set;
    //bgm_player bgm_player_;
    //Player_statu player;
    int min_statu;
    int cnt_next_bt_clicked;

    int level;

    public GameObject tutorial_panel;
    public GameObject tutorial_bt;
    public TMP_Text tutorial_msg;
    public GameObject arrow_3d;
    int[] shuffled_idx;

    // Start is called before the first frame update
    void Start()
    {
        bt_set.SetActive(false);
        bt_picture.SetActive(false);
        petctrl_script = GameObject.Find("Scripts_tutorial").GetComponent<Petctrl>();
        //level = player.Level_hungry;
        level = 1;
        time_remain = 0;
        cnt_corr = 0;
        Center_device = new Vector2(Screen.width / 2f, Screen.height / 2f);
        cnt_moving_average = 0;
        prev_angle = 0f;
        prev_angle_frame = 0f;
        cnt_next_bt_clicked = 0;
        net.SetActive(false);

        tutorial_panel.SetActive(false);
        tutorial_bt.SetActive(false);
        arrow_3d.SetActive(false);
        Invoke("bt_active_true", 6f);
        
    }

    void bt_active_true()
    {
        bt_set.SetActive(true);
        bt_picture.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        //if (time_remain > 0)
        //{
        //    time_remain -= Time.deltaTime;
        //    time_remain_text.text = "음식의 순서를 기억해주세요!\n남은시간:" + ((int)time_remain % 60).ToString();
        //    if (time_remain <= 0)
        //    {
        //        time_remain_text.text = "";
        //    }
        //}


        if (c1_flag)
        {
            var cur_angle = Camera.main.transform.eulerAngles.x;
            var catching_obj_pos = Camera.main.ScreenToWorldPoint(new Vector3(Center_device.x, Center_device.y, 0.2f));
            net.transform.position = catching_obj_pos;

            if (cur_angle > 0 && cur_angle < 90)
            {
                float cur_speed = Mathf.Abs(cur_angle - prev_angle_frame);
                float filtered_speed = filter_speed(cur_speed);

                //Debug.Log(Camera.main.transform.eulerAngles.x + "\t" + filtered_speed);
                if (filtered_speed < 2.5f)
                {
                    //Debug.Log("가마니");
                    return;
                }

                if (cur_angle - prev_angle < 0) // 들어 올릴 때
                {
                    //net.transform.eulerAngles = new Vector3(net.transform.eulerAngles.x, net.transform.eulerAngles.y, 45f);
                    var ray = Camera.main.ScreenPointToRay(Center_device);
                    Physics.Raycast(ray, out hit, float.PositiveInfinity);
                    if (hit.transform.CompareTag("food"))
                    {
                        Debug.Log(hit.transform.name);
                        if (hit.transform.name == food_selected[cnt_corr].transform.name)
                        {
                            Invoke("clear_text", 2f);
                            food_selected[cnt_corr].SetActive(false);
                            cnt_corr += 1;

                            if (level == 1)
                            {
                                if (cnt_corr == 3)
                                {
                                    //re-initialize
                                    petctrl_script.heart_effect_true();
                                    petctrl_script.pet_reaction_hungry_true();
                                    choose_and_show_random_food();
                                    net.SetActive(false);
                                    hungry_next_bt_clicked();
                                }
                                else
                                {
                                    hungry_next_bt_clicked();
                                }
                            }
                         }

                        else//다른 음식을 고른 경우 
                        {
                            //time_remain_text.text = "다른 음식을 골라주세요";
                            //Invoke("clear_text", 2f);
                            petctrl_script.pet_reaction_false();
                        }
                    }


                }
                else// 
                {
                    net.transform.eulerAngles = new Vector3(net.transform.eulerAngles.x, net.transform.eulerAngles.y, 15f);
                }

                prev_angle_frame = cur_angle;

                if (Mathf.Abs(cur_angle - prev_angle) > 5f)
                {
                    prev_angle = cur_angle;
                }

            }
            //if (cur_acce - prev_acce < 0)
            //{
            //    tilting_fw = true;
            //    Debug.Log("숙이고 있는중");
            //}
            //else
            //{
            //    tilting_fw = false;
            //    Debug.Log("숙이기 반대");
            //}

            //if (Mathf.Abs(cur_acce - prev_acce) < 0.05f)
            //    prev_acce = cur_acce;
            //prev_acce_frame = cur_acce;
        }

    }

    public void hungry_next_bt_clicked()
    {
        Debug.Log("cnt_next_bt_clicked: " + cnt_next_bt_clicked);
       if(cnt_next_bt_clicked == 0)
        {
            bt_picture.SetActive(false);
            bt_set.SetActive(false);
            petctrl_script.not_move_pet = true;
            tutorial_panel.SetActive(true);
            tutorial_bt.SetActive(true);
            tutorial_msg.text = "강아지에게 줄 음식을 한번 골라볼까요?";
            cnt_next_bt_clicked++;
        }
       else if(cnt_next_bt_clicked == 1)
        {
            tutorial_msg.text = "음식의 순서를 기억해주세요!";
            cnt_next_bt_clicked++;
            hungry_bt_click();
        }
        else if (cnt_next_bt_clicked == 2)
        {
            c1_flag = true;
            tutorial_msg.text = "음식을 순서대로 그릇에 담아주세요!\n그릇을 움직여주면 음식을 담을 수 있어요!";
            arrow_3d.SetActive(true);
            change_to_shuffled();
            cnt_next_bt_clicked++;
        }
        else if (cnt_next_bt_clicked == 3)
        {
            tutorial_msg.text = "잘 하셨어요! 두 번째 음식도 골라볼까요?";
            arrow_3d.transform.position = food_selected[1].transform.position + 0.1f * Vector3.up;
            Debug.Log("shuffled_idx[1]: " + shuffled_idx[1]);
            cnt_next_bt_clicked++;
        }
        else if (cnt_next_bt_clicked == 4)
        {
            tutorial_msg.text = "잘 하셨어요! 마지막 음식도 골라볼까요?";
            arrow_3d.transform.position = food_selected[2].transform.position + 0.1f * Vector3.up;
            Debug.Log("shuffled_idx[2]: " + shuffled_idx[2]);
            cnt_next_bt_clicked++;
        }
        else if (cnt_next_bt_clicked == 5)
        {
            arrow_3d.SetActive(false);
            tutorial_msg.text = "모두 다 잘 고르셨네요! 다음 게임도 배워볼까요?";
            tutorial_bt.SetActive(false);
            Invoke("re_init", 10f);
            cnt_next_bt_clicked = 0;
        }
    }

    public void choose_and_show_random_food()
    {
        GameObject random_food = Instantiate(foods[Random.Range(0, foods.Count)]);
        //random_food.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        random_food.transform.position = petctrl_script.spawnedObject.transform.GetChild(3).transform.position
                                        + 0.016f * Vector3.up;
        random_food.SetActive(true);
        Destroy(random_food.gameObject, 10f);

    }


    public void hungry_bt_click()
    {
        int num_food;
        num_food = level + 2; //1단계면 3개, 2단계면 4개, 3단계면 5개
        //bt_face.SetActive(false);
        //bt_picture.SetActive(false);
        //bt_set.SetActive(false);
        //petctrl_script.not_move_pet = true;

        var shuffle_idx = MakeRandomNumbers(foods.Count); ////food_selected
        for (int i = 0; i < num_food;/*레벨에 따라 3또는4또는5*/ i++)
        {
            //randomly choose food from foods gameobject
            food_selected.Add(foods[shuffle_idx[i]]);
            Debug.Log(food_selected[i].name);
            if (level == 1)
            {
                food_selected[i].transform.position = Camera.main.transform.position
                                                + 0.7f * Camera.main.transform.forward
                                                + (-0.2f + i * 0.2f) * Vector3.right
                                                + Vector3.up * 0.15f;
                food_selected[i].SetActive(true);
            }
        }

        //Invoke("change_to_shuffled", 10f);
        //time_remain = 10f;

        //foods[0].transform.position = Camera.main.transform.position + 0.3f*Vector3.forward;

        //bowl will be located infront of pet, attached to the ground
        //
        //bowl.transform.position = petctrl_script.spawnedObject.transform.position
        //                          + petctrl_script.spawnedObject.transform.forward*0.1f;
        //Debug.Log(petctrl_script.spawnedObject.transform.position + "\t"
        //          + petctrl_script.spawnedObject.transform.forward
        //          + "\t" + bowl.transform.position);

        //pop-up panel create(3d)
        //pop-up panel will be located in front of camera
        //Lv.1: 3 foods, Lv.2: 4foods, Lv3: 5foods.
        //firstly, the sequence of foods should be saved.
    }

    //showing mode, selecting mode 등으로 구분 필요



    void change_to_shuffled()
    {
        if (net.activeSelf == false) net.SetActive(true);
        //for (int i = 0; i < food_selected.Count;)
        //{
        //    food_selected[i].SetActive(false);
        //}
        shuffled_idx = MakeRandomNumbers(food_selected.Count); //food_selected_shuffled

        for (int j = 0; j < food_selected.Count; j++)
        {
            if (level == 1)
            {
                food_selected[shuffled_idx[j]].transform.position = Camera.main.transform.position
                                                                + 0.7f * Camera.main.transform.forward
                                                                + Vector3.right * (-0.2f + j * 0.2f)
                                                                + Vector3.up * 0.15f;
            }
        }
        arrow_3d.transform.position = food_selected[0].transform.position + 0.1f * Vector3.up;
        Debug.Log("shuffled_idx[0]: " + shuffled_idx[0] + "\t" + food_selected[shuffled_idx[0]].name);
    }

    public void re_init()
    {
        //bt_face.SetActive(true);
        bt_picture.SetActive(true);
        bt_set.SetActive(true);
        petctrl_script.not_move_pet = false;
        tutorial_panel.SetActive(false);
        prev_angle = 0f;
        cnt_moving_average = 0;
        petctrl_script.not_move_pet = false;
        food_selected.Clear();
        c1_flag = false;
        cnt_corr = 0;

        for (int i = 0; i < foods.Count; i++)
        {
            foods[i].SetActive(false);
        }

        //bt_set.transform.GetChild(0).transform.position = bt_set.transform.GetChild(min_statu + 1).transform.position
        //                                                + Vector3.left * 100;
    }

    public static int[] MakeRandomNumbers(int maxValue, int randomSeed = 0)
    {
        return MakeRandomNumbers(0, maxValue, randomSeed);
    }
    public static int[] MakeRandomNumbers(int minValue, int maxValue, int randomSeed = 0)
    {
        if (randomSeed == 0)
            randomSeed = (int)System.DateTime.Now.Ticks;

        List<int> values = new List<int>();
        for (int v = minValue; v < maxValue; v++)
        {
            values.Add(v);
        }

        int[] result = new int[maxValue - minValue];
        System.Random random = new System.Random(Seed: randomSeed);
        int i = 0;
        while (values.Count > 0)
        {
            int randomValue = values[random.Next(0, values.Count)];
            result[i++] = randomValue;

            if (!values.Remove(randomValue))
            {
                // Exception
                break;
            }
        }

        return result;
    }

    private float filter_speed(float cur_speed)
    {
        float filtered_speed;
        if (cnt_moving_average < speed_mv_avg.Length)
        {
            filtered_speed = 0;
            speed_mv_avg[cnt_moving_average] = cur_speed;
            cnt_moving_average += 1;
            for (int i = 0; i < cnt_moving_average; i++)
            {
                filtered_speed += speed_mv_avg[i];
            }
            filtered_speed /= cnt_moving_average;
        }
        else
        {
            speed_mv_avg = speed_mv_avg.Skip(1).ToArray(); // 맨 앞 삭제
            speed_mv_avg = speed_mv_avg.Append(cur_speed).ToArray(); // 맨 끝 추가

            filtered_speed = 0;
            for (int i = 0; i < speed_mv_avg.Length; i++)
            {
                filtered_speed += speed_mv_avg[i];
            }
            filtered_speed /= speed_mv_avg.Length;

        }

        return filtered_speed;
    }
    void clear_text()
    {
        time_remain_text.text = "";
    }
}
