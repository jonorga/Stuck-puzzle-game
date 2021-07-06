/*
- DONE Shots taken int
- DONE Begin game method: reset shots taken int, put ball at start of level
- DONE Fire ball method: Rigidbody launch ball, pause controls
- Aim ball method: Move vector of aim (both visible and target direction for launch), aim is global transform not local
- DONE Level lost method: disable controls, enable level lost menu
- DONE Lost retry: begin game method, disable level lost menu, enable controls
- DONE Lost quit: disable level, enable main menu, change music, call ad
- DONE Level won method: disable controls, enable game over menu, disable level, change music, call ad
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform ballTF, ballAim;
    public RectTransform gyro_button, gyro_aim, gyro_dummy;
    public Rigidbody ballRB;
    public int shots_taken = 0;
    public PlayerData _pd;
    public AdManager _am;
    public Transform[] level_start;
    public Button[] controls;
    public GameObject level_lost_menu, main_menu, game_over_menu;
    int level_base_num;
    bool gyro_active = false;
    float gyro_angle;
    int gyro_index;

    void Start()
    {
        level_base_num = _pd.level_base_num;
    }

    void Update()
    {
        if (gyro_active)
        {
            TranslatedPosition();
        }
        else 
        {
            gyro_button.localPosition = new Vector3(0,0,0);
        }
    }

    void TranslatedPosition()
    {
        if (Input.touches.Length <= gyro_index)
        {
            gyro_active = false;
            return;
        }
        gyro_dummy.position = new Vector3(Input.touches[gyro_index].position.x, Input.touches[gyro_index].position.y, gyro_dummy.position.z);
        gyro_aim.LookAt(gyro_dummy, Vector3.up);
        if (gyro_aim.rotation.y < 0) 
        {
            if (gyro_aim.eulerAngles.x > 270)
            {
                gyro_angle = ((gyro_aim.eulerAngles.x - 270)/180) + 0.5f;
            }
            else 
            {
                gyro_angle = (gyro_aim.eulerAngles.x/180) + 1;
            }
        }
        else
        {
            if (gyro_aim.eulerAngles.x > 270)
            {
                gyro_angle = 0.5f - ((gyro_aim.eulerAngles.x - 270)/180);
            }
            else
            {
                gyro_angle = 2 - (gyro_aim.eulerAngles.x/180);
                    
            }
        }
        if (Mathf.Abs(gyro_dummy.localPosition.x) < Mathf.Abs((67 * Mathf.Cos(Mathf.PI * gyro_angle))) && Mathf.Abs(gyro_dummy.localPosition.y) < Mathf.Abs(67 * Mathf.Sin(Mathf.PI * gyro_angle)))
            gyro_button.position = gyro_dummy.position;
        else
            gyro_button.localPosition = new Vector3(67 * Mathf.Cos(Mathf.PI * gyro_angle), 67 * Mathf.Sin(Mathf.PI * gyro_angle), 0);
    }

    public void BeginGame()
    {
        shots_taken = 0;
        level_base_num = _pd.level_base_num;
        ballTF.position = level_start[level_base_num].position;
        main_menu.SetActive(false);
        EnableControls(true);
    }

#region BallControl
    public void FireBall()
    {
        ballRB.AddForce(ballTF.forward);
        EnableControls(false);
    }

    public void GyroAim(bool click_value)
    {
        gyro_index = Input.touches.Length - 1;
        gyro_active = click_value;
        /*
        foreach (Touch touch in Input.touches)
        {
            Debug.Log(Vector3.Distance(touch.rawPosition, gyro_aim.position));
        }
        */
    }

    public void ClickAim(string direction)
    {
        switch (direction)
        {
            case "left":
                break;
            case "right":
                break;
            case "up":
                break;
            case "down":
                break;
        }
    }
#endregion

    public void LevelLost()
    {
        EnableControls(false);
        level_lost_menu.SetActive(true);
    }

    public void LevelRetry()
    {
        BeginGame();
        EnableControls(true);
        level_lost_menu.SetActive(false);
    }

    public void LevelQuit()
    {
        main_menu.SetActive(true);
        _am.callAd();
    }

    public void LevelWon()
    {
        EnableControls(false);
        game_over_menu.SetActive(true);
        _am.callAd();
    }

    void EnableControls(bool toggle_val)
    {
        int leng = controls.Length;
        for (int i = 0; i < leng; i++)
        {
            controls[i].enabled = toggle_val;
        }
    }

    public int current_base_num()
    {
        return level_base_num;
    }

    public void increment_base_num()
    {
        level_base_num++;
    }
}
