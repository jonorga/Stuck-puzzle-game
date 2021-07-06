using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int level_base_num;
    public string selected_controls;
    public bool theme1, theme2, theme3, noAds;
    
    void Awake()
    {
        if (PlayerPrefs.HasKey("level_base_num"))
        {
            level_base_num = PlayerPrefs.GetInt("level_base_num");
        }
        else
        {
            level_base_num = 0;
            PlayerPrefs.SetInt("level_base_num", 0);
        }
        if (PlayerPrefs.HasKey("selected_controls"))
        {
            selected_controls = PlayerPrefs.GetString("selected_controls");
        }
        else
        {
            selected_controls = "gyro";
            PlayerPrefs.SetString("current_level", "gyro");
        }

        if (PlayerPrefs.HasKey("theme1"))
        {
            if (PlayerPrefs.GetInt("theme1") == 0)
                theme1 = false;
            else 
                theme1 = true;
        }
        else 
        {
            theme1 = false;
            PlayerPrefs.SetInt("theme1", 0);
        }
        if (PlayerPrefs.HasKey("theme2"))
        {
            if (PlayerPrefs.GetInt("theme2") == 0)
                theme2 = false;
            else 
                theme2 = true;
        }
        else 
        {
            theme2 = false;
            PlayerPrefs.SetInt("theme2", 0);
        }
        if (PlayerPrefs.HasKey("theme3"))
        {
            if (PlayerPrefs.GetInt("theme3") == 0)
                theme3 = false;
            else 
                theme3 = true;
        }
        else 
        {
            theme3 = false;
            PlayerPrefs.SetInt("theme3", 0);
        }
        if (PlayerPrefs.HasKey("noAds"))
        {
            if (PlayerPrefs.GetInt("noAds") == 0)
                noAds = false;
            else 
                noAds = true;
        }
        else 
        {
            noAds = false;
            PlayerPrefs.SetInt("noAds", 0);
        }
    }

    public void SetLevelBase(int level_base)
    {
        level_base_num = level_base;
        PlayerPrefs.SetInt("level_base_num", level_base);
    }

    public void IncreLevelBase()
    {
        level_base_num++;
        PlayerPrefs.SetInt("level_base_num", level_base_num);
    }
}
