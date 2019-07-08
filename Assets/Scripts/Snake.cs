using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Snake : MonoBehaviour
{
    public const float WIDTH_SCREEN = 1920;
    public const float HEIGHT_SCREEN = 1080;
    public GameObject partSnake;
    public GameObject eatSnake;

    private List<GameObject> partsSnake;
    private Vector3 addPosition;
    private Vector3 moveVector;
    private Vector3 sizeSprite;
    private float toMove;
    public float speedSnake;
    public int score = 0;
    private GUIStyle guiStyle;


    // Start is called before the first frame update
    void Start()
    {
        addPosition = new Vector3();
        partsSnake = new List<GameObject>();
        sizeSprite = partSnake.GetComponent<SpriteRenderer>().bounds.size;
        moveVector = new Vector3(sizeSprite.x, 0, 0);

        CreatePartsSnake();
        AddRandomEat();
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.red;
    }

    

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(0,0,300,60),"Your score: "+score ,guiStyle);
    }

    
    public void AddScore()
    {
        score++;

        if (score % 3 == 0)
        {
            speedSnake -= 0.02f;
            if (speedSnake < 0.05f) speedSnake = 0.05f;
        }
    }
    public void AddPartToSnake()
    {
        GameObject lastPart = partsSnake[partsSnake.Count - 1];
        Vector3 position = lastPart.transform.position;
        GameObject objSnake = Instantiate(partSnake, new Vector3(position.x-moveVector.x, position.y-moveVector.y, 0),
            Quaternion.identity);
        partsSnake.Add(objSnake);
    }

    public void AddRandomEat()
    {
        float randomX = (int)(Random.Range(0,WIDTH_SCREEN)/sizeSprite.x) * sizeSprite.x;
        float randomY = (int)(Random.Range(0,HEIGHT_SCREEN)/sizeSprite.y) * sizeSprite.y;

        Instantiate(eatSnake, new Vector3(randomX, randomY, 0),Quaternion.identity);
    }
    private void CreatePartsSnake()
    {
        Vector3 position = transform.position;

        for (int i = 0; i < 3; i++)
        {
            GameObject objSnake = Instantiate(partSnake, new Vector3(position.x - sizeSprite.x * i, position.y, 0),
                Quaternion.identity);
            partsSnake.Add(objSnake);
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (moveVector.y == 0)
            {
                moveVector.x = 0;
                moveVector.y = sizeSprite.y;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (moveVector.y == 0)
            {
                moveVector.x = 0;
                moveVector.y = -sizeSprite.y;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (moveVector.x == 0)
            {
                moveVector.x = sizeSprite.x;
                moveVector.y = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (moveVector.x == 0)
            {
                moveVector.x = -sizeSprite.x;
                moveVector.y = 0;
            }
        }

        toMove += Time.deltaTime;

        if (toMove >= speedSnake)
        {
            toMove = 0;
            var positionThis = transform.position;
            addPosition.Set(positionThis.x + moveVector.x, positionThis.y+moveVector.y, 0);
            transform.position = addPosition;

            Vector3 prevPos = new Vector3();
            for (var i = 0; i < partsSnake.Count; i++)
            {
                GameObject part = partsSnake[i];
                var position = part.transform.position;


                if (i == 0)
                {
                    addPosition.Set(position.x + moveVector.x, position.y + moveVector.y, 0);

                    if (addPosition.x > WIDTH_SCREEN)
                    {
                        addPosition.x = 0;
                    }

                    if (addPosition.x < 0)
                    {
                        addPosition.x = WIDTH_SCREEN;
                    }

                    if (addPosition.y > HEIGHT_SCREEN)
                    {
                        addPosition.y = 0;
                    }

                    if (addPosition.y < 0)
                    {
                        addPosition.y = HEIGHT_SCREEN;
                    }

                    prevPos = part.transform.position;
                }
                else
                {
                    addPosition.Set(prevPos.x, prevPos.y, 0);
                    prevPos = part.transform.position;
                }

                part.transform.position = addPosition;
            }
        }
    }
}