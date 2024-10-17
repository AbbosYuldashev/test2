using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leaderboard : MonoBehaviour
{ 
    public void Inventory()
    {
        SceneManager.LoadScene("ship_selections");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("levels");
    }
}
