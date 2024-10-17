using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextlevel : MonoBehaviour
{
 

    private void Start()
    {
       
        
    }
    public void Playlevel2()
    {
        if (Level1Timer.isfinishedlevel1)
        {
            BossMovement.bossHealth = 50;
            SceneManager.LoadScene("level2");
        }
        else
        {
            Debug.Log("please finish the level 1");
        }
    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene("level1");
    }
}
