using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int lives = 3;
    public Text livesHud;
    // Start is called before the first frame update
    private void Awake()
    {
        GameAccessor.Instance().score = this;
    }

    // Update is called once per frame
    void Update()
    {
        livesHud.text = "Lives: " + lives;
    }

    public void PlayerHit()
    {
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            GameAccessor.Instance().level.Reload();
        }
    }
}
