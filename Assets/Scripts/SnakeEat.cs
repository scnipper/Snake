using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEat : MonoBehaviour
{
    private Snake snake;

    // Start is called before the first frame update
    void Start()
    {
        snake = FindObjectOfType<Snake>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject); 
        snake.AddRandomEat();
        snake.AddPartToSnake();
        snake.AddScore();
    }
}
