/*
- DONE Begin game method: disable start menu, change music, activate controls, game manager: begin game
- DONE Start method: If player data sees at least one level game menu show "continue game"
+++
- *Pick world menu: open worlds menu
- *Pick level menu method: Open levels menu (take world num as input)
- DONE Pick level method: Open selected level (takes overall level num as input), game manager: begin game, change music, disable start menu, disable pick level menu, set currently selected world at player data, enable controls
- *Pick theme menu method: open theme menu
- DONE Set theme method: If theme is owned, set. Otherwise call purchase manager
+++
- *Settings: Open settings menu
- DONE Set selected controls: Reference player data for selected controls, set active, called on start
- DONE Toggle selected controls: changes currently set controls, sends to GUI and player data
- *All Audio Toggle: Toggle master mixer (MAKE THIS A GAMEOBJECT THAT IS TOGGLED WITH THE BUTTON)
+++
- *Back to menu method: Close game over menu, open start menu
- DONE Replay level method: disable game over menu, game manager: begin game, change music, enable controls
- DONE Next level method: disable game over menu, change music, enable controls, set next world at player data, THEN game manager: begin game
- DONE Game over method: show shots taken vs par for "perfect score" or not
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject world_menu, level_menu;
    public GameObject game_over_menu;
    public GameObject[] controls;
    public GameObject[] level;
    public int[] level_par;
    public GameObject gyro_controls, button_controls;
    public GameController _gc;
    public PlayerData _pd;
    public UnityEngine.UI.Text game_over_result, shots_text, par_text;
    string selected_controls;

    void Start()
    {
        selected_controls = _pd.selected_controls;
        if (selected_controls == "gyro")
        {
            gyro_controls.SetActive(true);
            button_controls.SetActive(false);
        }
        else
        {
            gyro_controls.SetActive(false);
            button_controls.SetActive(true);
        }
    }

    public void BeginGame()
    {
        main_menu.SetActive(false);
        ActivateControls(true);
        _gc.BeginGame();
    }

    public void SelectLevel(int level_base)
    {
        level[level_base].SetActive(true);
        _gc.BeginGame();
        main_menu.SetActive(false);
        world_menu.SetActive(false);
        level_menu.SetActive(false);
        _pd.SetLevelBase(level_base);
        ActivateControls(true);
    }

    public void SetTheme(int selected_theme)
    {
        // TODO: Call purchase manager if theme not owned
    }

#region Settings
    public void ToggleControls()
    {
        if (selected_controls == "gyro")
        {
            gyro_controls.SetActive(false);
            button_controls.SetActive(true);
            selected_controls = "buttons";
        }
        else 
        {
            gyro_controls.SetActive(true);
            button_controls.SetActive(false);
            selected_controls = "gyro";
        }
    }
#endregion

    public void ReplayLevel()
    {
        game_over_menu.SetActive(false);
        ActivateControls(true);
        _gc.BeginGame();
    }

    public void GameOver()
    {
        if (_gc.shots_taken <= level_par[_gc.current_base_num()])
            game_over_result.text = "Perfect!";
        else
            game_over_result.text = "Passed";
        shots_text.text = _gc.shots_taken.ToString();
        par_text.text = level_par[_gc.current_base_num()].ToString();
    }

    public void NextLevel()
    {
        game_over_menu.SetActive(false);
        ActivateControls(true);
        _pd.IncreLevelBase();
        _gc.BeginGame();
    }

    void ActivateControls(bool toggle_val)
    {
        int leng = controls.Length;
        for (int i = 0; i < leng; i++)
        {
            controls[i].SetActive(toggle_val);
        }
    }

    void TranslateBase()
    {
        int l_num = (_gc.current_base_num() % 4) + 1;
        int w_num = (_gc.current_base_num() / 4) + 1;
        Debug.Log("I: " + _gc.current_base_num() + " . World: " + w_num + " Level: " + l_num);
    }
}
