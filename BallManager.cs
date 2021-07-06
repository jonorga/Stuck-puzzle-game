/*
- Collided with object method: Find tag, call appropriate method
- Stuck to surface method: called if tag is "barrier", stop velocity, freeze in place, unpause controls
- Destroyed method: call if tag is "red", destroy ball, call game manager: level lost
- Out of bounds method: call if tag is "boundary", call game manager: level lost
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public Rigidbody ballRB;
    public Animator ballAnim;
    public Button[] controls;
    public GameController _gc;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "barrier")
        {
            ballRB.velocity = Vector3.zero;
            EnableControls(true);
        }
        else if (collision.gameObject.tag == "red")
        {
            ballAnim.Play("ball_destroy");
            EnableControls(false);
            _gc.LevelLost();
        }
        else if (collision.gameObject.tag == "boundary")
        {
            EnableControls(false);
            _gc.LevelLost();
        }
    }

    void EnableControls(bool toggle_val)
    {
        int leng = controls.Length;
        for (int i = 0; i < leng; i++)
        {
            controls[i].enabled = toggle_val;
        }
    }
}
