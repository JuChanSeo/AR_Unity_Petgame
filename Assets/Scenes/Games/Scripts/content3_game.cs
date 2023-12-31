using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class content3_game : MonoBehaviour
{
    public GameObject content3_panel;
    public GameObject reshow_bt;
    //public GameObject bt_face;
    public GameObject bt_picture;
    public GameObject bt_set;
    public List<Sprite> bottles_sprite = new List<Sprite>();
    public List<GameObject> bottle_3d = new List<GameObject>();
    public List<Image> bottles = new List<Image>();
    public List<GameObject> bts = new List<GameObject>();
    public TMP_Text time_remain_text;
    public GameObject debug_prefab;
    bool flag_set_empty;
    //Vector3[] spreaded_points;
    List<Vector3> spreaded_points = new List<Vector3>();
    List<Vector3> extracted_points = new List<Vector3>();

    Petctrl petctrl_script;
    bgm_player bgm_player_;
    Player_statu player;
    float time_remain;
    int level;//나중에 다른데에서 부터 받아온다
    int cnt_answer;
    string[] answer_seq_color = new string[5];//bottle_red, bottle_blue, ...
    Dictionary<string, int> color_to_num = new Dictionary<string, int>();
    public bool c3_flag;
    int cnt_false;
    bool success_flag;
    int[] level_per_corr = new int[] { 0, 3, 4, 5 };
    


    // Start is called before the first frame update
    void Start()
    {
        content3_panel.transform.position = new Vector3(1194, 834, 0);

        color_to_num.Add("red", 1);
        color_to_num.Add("orange", 2);
        color_to_num.Add("yellow", 3);
        color_to_num.Add("green", 4);
        color_to_num.Add("blue", 5);

        //System.Range r1 = 0..4;
        for (int i = 0; i< 5; i++)
        {
            bottle_3d[i].SetActive(false);
        }
        cnt_answer = 0;
        petctrl_script = GameObject.Find("Scripts").GetComponent<Petctrl>();
        bgm_player_ = GameObject.Find("Audio player").GetComponent<bgm_player>();
        player = GameObject.Find("player_statu").GetComponent<Player_statu>();
        time_remain = 0;
        content3_panel.SetActive(false);
        reshow_bt.SetActive(false);
        level = 3;

        success_flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!c3_flag) return;
        if (time_remain > 0)
        {
            time_remain -= Time.deltaTime;
            time_remain_text.text = "샴푸병 색의 순서를 기억해주세요!\n남은시간:" + ((int)time_remain % 60).ToString();
            if (time_remain <= 0)
            {                
                time_remain_text.text = "";
            }
        }

        if (Input.touchCount > 0)
        {
            //Debug.Log("logging");
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                var ray = Camera.main.ScreenPointToRay(touch.position);
                Physics.Raycast(ray, out var hit, float.PositiveInfinity);
                //Debug.Log(hit.transform.name);
                var dis = Vector2.Distance(new Vector2(hit.transform.position.x, hit.transform.position.z),
                                  new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z));

                if (hit.transform.name.StartsWith("bottle") && dis<0.5f)
                {
                    check_the_answer(hit.transform.name);
                }
                else
                {

                }
            }
        }
    }

    public void reshow_bt_clicked()
    {
        time_remain = 10f;
        reshow_bt.SetActive(false);
        //흩뿌려진 샴푸병 없애기
        for (int i=0; i<bottle_3d.Count; i++)
        {
            bottle_3d[i].SetActive(false);
        }
        //원래 정답 보여주기
        if (level == 1)
        {
            bottles[1].gameObject.SetActive(true);
            bottles[2].gameObject.SetActive(true);
            bottles[3].gameObject.SetActive(true);
        }
        if (level == 2)
        {
            bottles[1].gameObject.SetActive(true);
            bottles[2].gameObject.SetActive(true);
            bottles[3].gameObject.SetActive(true);
            bottles[4].gameObject.SetActive(true);
        }
        if (level == 3)
        {
            bottles[0].gameObject.SetActive(true);
            bottles[1].gameObject.SetActive(true);
            bottles[2].gameObject.SetActive(true);
            bottles[3].gameObject.SetActive(true);
            bottles[4].gameObject.SetActive(true);
        }

        Invoke("reshow", 10f);
    }

    void reshow()
    {
        // 다시 원래화면 보여주기
        bottles[0].gameObject.SetActive(false);
        bottles[1].gameObject.SetActive(false);
        bottles[2].gameObject.SetActive(false);
        bottles[3].gameObject.SetActive(false);
        bottles[4].gameObject.SetActive(false);
        reshow_bt.SetActive(true);

        for (int i = 0; i < level + 2; i++)
        {
            if(i>=cnt_answer)
            {
                string sprite_name = answer_seq_color[i].Split("_")[1];
                bottle_3d[color_to_num[sprite_name] - 1].SetActive(true);
            }
        }
    }

    public void bath_button_clicked()
    {
        c3_flag = true;
        petctrl_script.not_move_pet = true;
        //bt_face.SetActive(false);
        //bt_picture.SetActive(false);
        bt_set.SetActive(false);

        petctrl_script.not_move_pet = true;
        content3_panel.SetActive(true);
        petctrl_script.not_move_pet = true;

        content3_panel.transform.GetChild(0).gameObject.SetActive(true);
        content3_panel.transform.GetChild(1).gameObject.SetActive(true);
        content3_panel.transform.GetChild(2).gameObject.SetActive(true);
        content3_panel.transform.GetChild(3).gameObject.SetActive(true);
        content3_panel.transform.GetChild(4).gameObject.SetActive(true);
        bts[0].SetActive(false);
        bts[1].SetActive(false);
        bts[2].SetActive(false);
        bts[3].SetActive(false);
        bts[4].SetActive(false);



        if (level == 1)
        {
            content3_panel.transform.GetChild(0).gameObject.SetActive(false);
            content3_panel.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if(level == 2)
        {
            content3_panel.transform.GetChild(0).gameObject.SetActive(false);
            bottles[1].transform.position = new Vector3(906f - 240f, 929.22f, 0);
            bottles[2].transform.position = new Vector3(1253f - 240f, 929.22f, 0);
            bottles[3].transform.position = new Vector3(1601f - 240f, 929.22f, 0);
            bottles[4].transform.position = new Vector3(1948f - 240f, 929.22f, 0);

        }
        show_the_answer();
        time_remain = 10f;
        Invoke("reset_to_empty", 10f);

    }

    void show_the_answer()
    {
        var shuffled_idx = MakeRandomNumbers(1, 6);
        if (level == 1)
        {
            bottles[1].sprite = bottles_sprite[shuffled_idx[0]];
            bottles[2].sprite = bottles_sprite[shuffled_idx[1]];
            bottles[3].sprite = bottles_sprite[shuffled_idx[2]];
            answer_seq_color[0] = bottles[1].sprite.name;
            answer_seq_color[1] = bottles[2].sprite.name;
            answer_seq_color[2] = bottles[3].sprite.name;
        }
        else if (level == 2)
        {
            bottles[1].sprite = bottles_sprite[shuffled_idx[0]];
            bottles[2].sprite = bottles_sprite[shuffled_idx[1]];
            bottles[3].sprite = bottles_sprite[shuffled_idx[2]];
            bottles[4].sprite = bottles_sprite[shuffled_idx[3]];
            answer_seq_color[0] = bottles[1].sprite.name;
            answer_seq_color[1] = bottles[2].sprite.name;
            answer_seq_color[2] = bottles[3].sprite.name;
            answer_seq_color[3] = bottles[4].sprite.name;
        }
        else //levle==3
        {
            bottles[0].sprite = bottles_sprite[shuffled_idx[0]];
            bottles[1].sprite = bottles_sprite[shuffled_idx[1]];
            bottles[2].sprite = bottles_sprite[shuffled_idx[2]];
            bottles[3].sprite = bottles_sprite[shuffled_idx[3]];
            bottles[4].sprite = bottles_sprite[shuffled_idx[4]];
            answer_seq_color[0] = bottles[0].sprite.name;
            answer_seq_color[1] = bottles[1].sprite.name;
            answer_seq_color[2] = bottles[2].sprite.name;
            answer_seq_color[3] = bottles[3].sprite.name;
            answer_seq_color[4] = bottles[4].sprite.name;

        }
    }

    void reset_to_empty()
    {
        if (!c3_flag) return;
        //bottles[0].sprite = bottles_sprite[0];
        //bottles[1].sprite = bottles_sprite[0];
        //bottles[2].sprite = bottles_sprite[0];
        //bottles[3].sprite = bottles_sprite[0];
        //bottles[4].sprite = bottles_sprite[0];
        bottles[0].gameObject.SetActive(false);
        bottles[1].gameObject.SetActive(false);
        bottles[2].gameObject.SetActive(false);
        bottles[3].gameObject.SetActive(false);
        bottles[4].gameObject.SetActive(false);
        reshow_bt.SetActive(true);
        spread_bottle_3d();


    }

    void spread_bottle_3d()
    {
        spreaded_points.Clear();
        extracted_points.Clear();
        for(int i = 0; i < 500; i++)
        {
            //int range_x = Random.Range((int)(Screen.width * 0.2f), (int)(Screen.width * 0.8f));
            int range_x = Random.Range(0, Screen.width);
            int range_y = Random.Range(0, (int)(Screen.height / 2));
            var ray = Camera.main.ScreenPointToRay(new Vector2(range_x, range_y));
            Physics.Raycast(ray, out var hit, float.PositiveInfinity);
            //Debug.Log(range_x + "\t" + range_y + "\t" + hit.point.x + "\t" + hit.point.y);
            //Instantiate(debug_prefab, hit.point, Quaternion.identity);
            if (hit.point.y > -5f && hit.point.y < 0)
            {
                spreaded_points.Add(hit.point);
            }
            
        }

        Debug.Log("spread done, spreaded_points.Count: " + spreaded_points.Count);
        int level_per_num_bottles = level + 2;
        int[] shuffled_idx = MakeRandomNumbers(0, spreaded_points.Count);
        

        for (int i = 0; i < level_per_num_bottles; i++)
        {
            string answer_color = answer_seq_color[i].Split("_")[1];
            //Debug.Log("color_to_num[answer_color] - 1: " + (color_to_num[answer_color] - 1));
            //Debug.Log(spreaded_points[rand_idx].x + "\t" + spreaded_points[rand_idx].y + "\t" + spreaded_points[rand_idx].z);

            //int rand_idx = shuffled_idx[i];
            int sum_id = 0;
            //float dis;
            while(true)
            {
                int rand_id = Random.Range(0, spreaded_points.Count);
                for(int j = 0; j < i; j++)
                {
                    if(Vector3.Distance(spreaded_points[rand_id], extracted_points[j]) > 0.5f)
                    {
                        sum_id++;
                    }
                }

                if(sum_id == i)
                {
                    bottle_3d[color_to_num[answer_color] - 1].transform.position
                    = spreaded_points[rand_id] + 0.2f * Vector3.up;
                    extracted_points.Add(spreaded_points[rand_id]);
                    bottle_3d[color_to_num[answer_color] - 1].SetActive(true);
                    Debug.Log(answer_color + " 위치: " + bottle_3d[color_to_num[answer_color] - 1].transform.position);
                    sum_id = 0;
                    break;
                }
                else
                {
                    sum_id = 0;
                    continue;
                }
            }

            for(int j=0; j<i; j++)
            {
                float dis = Vector3.Distance(extracted_points[i], extracted_points[j]);
                Debug.Log("dis: " + dis);
            }
            
        }

        //세 개의 포인트를 선택한다
        // 세개의 포인트 선정 기: y값(높낮이)가 (-0.5, 0)인 포인트들로만 선택한다
        // 화면상의 여러 부분에 ray를 쏘아서 포인트들을 막 저장한다 -> game_mode script의 extract_point와 비슷하게 구현하면 될 듯

        //세 개의 포인트 보다 살짝 위 쪽에 게임오브젝트(물병)을 위치시킨다.

    }

    public void check_the_answer(string Name)
    {
        if (time_remain > 0) return;

        //GameObject clickedobj = EventSystem.current.currentSelectedGameObject;
        GameObject clickedobj = GameObject.Find(Name + "_prefab");
        string clicked_bt_name = Name.Split("_")[1];

        if(level == 1)
        {
            string sprite_name = answer_seq_color[cnt_answer].Split("_")[1];
            if (clicked_bt_name == sprite_name)
            {
                cnt_answer += 1;
                cnt_false = 0;
                clickedobj.SetActive(false);
                //bottles[cnt_answer].sprite = bottles_sprite[color_to_num[clicked_bt_name]];//정답을 맞추면 원래 색깔대로 바뀐다
                if(cnt_answer == 3)
                {
                    if(success_flag)
                    {
                        player.change_statu(0, -0.03f, 0.03f, 0.01f);
                        time_remain_text.text = "축하드립니다 모두 맞추셨습니다!";
                        petctrl_script.set_text_speechBubble("샴푸병을 모두\n다 모았습니다!");
                        petctrl_script.shower_effect_true();
                        bgm_player_.getitem_sound_excute();
                        //petctrl_script.pet_reaction_true();
                        Invoke("re_init", 10f);
                    }
                    else
                    {
                        petctrl_script.set_text_speechBubble("다음에 다시\n도전해볼까요?");
                        Invoke("re_init", 5f);
                    }

                }
                else
                {
                    if(success_flag)
                    {
                        petctrl_script.set_text_speechBubble("잘 맞추셨어요!");
                        bgm_player_.success_sound_excute();
                    }
                }
            }
            else
            {
                cnt_false++;
                if(cnt_false == 5)
                {
                    petctrl_script.set_text_speechBubble("다른 병을\n골라볼까요?");
                    cnt_false = 0;
                    success_flag = false;
                    //bottle_3d[cnt_answer].SetActive(false);
                    bottle_3d[color_to_num[sprite_name]-1].SetActive(false);
                    //Debug.Log(""+ $"\tbottle_3d[{cnt_answer}]: {bottle_3d[cnt_answer].name}");
                    cnt_answer += 1;
                    if(cnt_answer == level_per_corr[level])
                    {
                        Invoke("re_init", 1f);
                    }
                }
                else
                {
                    time_remain_text.text = "다시 골라볼까요?";
                    petctrl_script.pet_reaction_false();
                    bgm_player_.fail_sound_excute();
                    Invoke("clear_text", 1f);
                }
                //Debug.Log("check111");
            }
        }
        else if(level==2)
        {
            string sprite_name = answer_seq_color[cnt_answer].Split("_")[1];
            if (clicked_bt_name == sprite_name)
            {
                cnt_answer += 1;
                cnt_false = 0;
                clickedobj.SetActive(false);
                //bottles[cnt_answer].sprite = bottles_sprite[color_to_num[clicked_bt_name]];//정답을 맞추면 원래 색깔대로 바뀐다
                if (cnt_answer == 4)
                {
                    if (success_flag)
                    {
                        player.change_statu(0, -0.04f, 0.04f, 0.02f);
                        time_remain_text.text = "축하드립니다 모두 맞추셨습니다!";
                        petctrl_script.set_text_speechBubble("샴푸병을 모두\n다 모았습니다!");
                        petctrl_script.shower_effect_true();
                        bgm_player_.getitem_sound_excute();
                        //petctrl_script.pet_reaction_true();
                        Invoke("re_init", 10f);
                    }
                    else
                    {
                        petctrl_script.set_text_speechBubble("다음에 다시\n도전해볼까요?");
                        Invoke("re_init", 5f);
                    }

                }
                else
                {
                    if (success_flag)
                    {
                        petctrl_script.set_text_speechBubble("잘 맞추셨어요!");
                        bgm_player_.success_sound_excute();
                    }
                }
            }
            else
            {
                cnt_false++;
                if (cnt_false == 5)
                {
                    petctrl_script.set_text_speechBubble("다른 병을\n골라볼까요?");
                    cnt_false = 0;
                    success_flag = false;
                    bottle_3d[color_to_num[sprite_name] - 1].SetActive(false);
                    //bottle_3d[cnt_answer].SetActive(false);
                    cnt_answer += 1;
                    if (cnt_answer == level_per_corr[level])
                    {
                        Invoke("re_init", 1f);
                    }
                }
                else
                {
                    time_remain_text.text = "다시 골라볼까요?";
                    petctrl_script.pet_reaction_false();
                    bgm_player_.fail_sound_excute();
                    Invoke("clear_text", 1f);
                }
                //Debug.Log("check111");
            }
        }
        else
        {
            string sprite_name = answer_seq_color[cnt_answer].Split("_")[1];
            if (clicked_bt_name == sprite_name)
            {
                cnt_answer += 1;
                cnt_false = 0;
                clickedobj.SetActive(false);
                //bottles[cnt_answer].sprite = bottles_sprite[color_to_num[clicked_bt_name]];//정답을 맞추면 원래 색깔대로 바뀐다
                if (cnt_answer == 5)
                {
                    if (success_flag)
                    {
                        player.change_statu(0, -0.05f, 0.05f, 0.03f);
                        time_remain_text.text = "축하드립니다 모두 맞추셨습니다!";
                        petctrl_script.set_text_speechBubble("샴푸병을 모두\n다 모았습니다!");
                        petctrl_script.shower_effect_true();
                        bgm_player_.getitem_sound_excute();
                        //petctrl_script.pet_reaction_true();
                        Invoke("re_init", 10f);
                    }
                    else
                    {
                        petctrl_script.set_text_speechBubble("다음에 다시\n도전해볼까요?");
                        Invoke("re_init", 5f);
                    }

                }
                else
                {
                    if (success_flag)
                    {
                        petctrl_script.set_text_speechBubble("잘 맞추셨어요!");
                        bgm_player_.success_sound_excute();
                    }
                }
            }
            else
            {
                cnt_false++;
                if (cnt_false == 5)
                {
                    petctrl_script.set_text_speechBubble("다른 병을\n골라볼까요?");
                    cnt_false = 0;
                    success_flag = false;
                    bottle_3d[color_to_num[sprite_name] - 1].SetActive(false);
                    //bottle_3d[cnt_answer].SetActive(false);
                    cnt_answer += 1;
                    if (cnt_answer == level_per_corr[level])
                    {
                        Invoke("re_init", 1f);
                    }
                }
                else
                {
                    time_remain_text.text = "다시 골라볼까요?";
                    petctrl_script.pet_reaction_false();
                    bgm_player_.fail_sound_excute();
                    Invoke("clear_text", 1f);
                }
                //Debug.Log("check111");
            }
        }

    }

    public void re_init()
    {
        c3_flag = false;
        time_remain_text.text = "";
        petctrl_script.not_move_pet = false;
        content3_panel.SetActive(false);
        petctrl_script.not_move_pet = false;
        cnt_answer = 0;
        //bt_face.SetActive(true);
        bt_set.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            bottle_3d[i].SetActive(false);
        }
        int min_statu;
        min_statu = player.choose_higlight(); //enegry:0, fatigue:1, cleanliness:2, intimity:3
        bt_set.transform.GetChild(0).transform.position = bt_set.transform.GetChild(min_statu + 1).transform.position
                                                        + Vector3.left * 100;

        success_flag = true;
        cnt_false = 0;

        extracted_points.Clear();
        spreaded_points.Clear();
    }

    //public static int[] MakeRandomNumbers(int maxValue, int randomSeed = 0)
    //{
    //    return MakeRandomNumbers(0, maxValue, randomSeed);
    //}
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

    void clear_text()
    {
        time_remain_text.text = "";
    }
}
