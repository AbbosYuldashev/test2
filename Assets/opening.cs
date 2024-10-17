using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class opening : MonoBehaviour
{
   
    public void Close()
    {
        Debug.Log("thanks");
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void StartGame()
    {
        
        SceneManager.LoadScene("leaderboard");
    }
    public void info()
    {
        SceneManager.LoadScene("DemoScene");
    }
}
