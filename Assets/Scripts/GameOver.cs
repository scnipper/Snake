using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Snake snake;
    private GUIStyle guiStyle;
    private Rect posLabel;
    private Rect posLabel2;
    private GUIStyle guiStyle2;

    // Start is called before the first frame update
    void Start()
    {
        snake = FindObjectOfType<Snake>();
        guiStyle = new GUIStyle();
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontSize = 150;
        
        guiStyle2 = new GUIStyle();
        guiStyle2.fontSize = 80;
        
        posLabel = new Rect(Snake.WIDTH_SCREEN / 2.0f-500, 500, 600, 200);
        posLabel2 = new Rect(Snake.WIDTH_SCREEN / 2.0f-500, 100, 300, 100);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(posLabel,"GAME OVER",guiStyle);
        if (GUI.Button(posLabel2, "Restart",guiStyle2))
        {
            SceneManager.LoadScene("MainScene");
        }

        //GUI.Label(posLabel2,"Your score: "+snake.score,guiStyle);
    }
}
