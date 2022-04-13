using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Moving : MonoBehaviour
{
    //int score;
    //bool left, right, top, down;

    bool touched = false;
    bool moved = false;
    [SerializeField]
    GameObject[] objectsReferencecopy = new GameObject[16];

    GameObject[,] objectsReference = new GameObject[4, 4];

    int[,] scoreReference = new int[4, 4];

    [SerializeField]
    GameObject square;
    [SerializeField]
    GameObject empty;
    [SerializeField]
    Text text;

    int b = 0;
    Vector2 start;
    // Start is called before the first frame update
    void Start()
    {



        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                objectsReference[i, j] = Instantiate(empty);
                //objectsReferencecopy[b++] = Instantiate(empty);
            }
        AddRandomly();
        ShowTable();
        AddRandomly();
        ShowTable();
        /*b = 0;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                objectsReferencecopy[b++] = Instantiate(objectsReference[i, j]);
            }*/

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Move();

            /*b = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    objectsReferencecopy[b++] = Instantiate(objectsReference[i, j]);
                }*/
        }


    }

    void Move()
    {

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            start = touch.position;
            touched = true;
        }

        if (touched == true && touch.phase == TouchPhase.Moved && start.x - touch.position.x > 50)
        {
            touched = false;
            Debug.Log("tu jest1");
            MoveLeft();
            ShowTable();
            ActualiseScoreTable();
        }
        else if (touch.phase == TouchPhase.Moved && start.x - touch.position.x < -50 && touched == true)
        {
            touched = false;
            Debug.Log("tu jest2");
            MoveRight();
            ShowTable();
            ActualiseScoreTable();
        }
        else if (touch.phase == TouchPhase.Moved && start.y - touch.position.y > 50 && touched == true)
        {
            touched = false;
            Debug.Log("tu jest3");
            MoveDown();
            ShowTable();
            ActualiseScoreTable();
        }
        else if (touch.phase == TouchPhase.Moved && start.y - touch.position.y < -50 && touched == true)
        {
            touched = false;
            Debug.Log("tu jest4");
            MoveUp();
            ShowTable();
            ActualiseScoreTable();
        }

        if (moved == true)
        {
            AddRandomly();
            ShowTable();
            CheckEnd();
            moved = false;
        }

    }


    void MoveLeft()
    {
        int neededScore = 0;
        Vector2Int position;

        for (int i = 0; i < 4; i++)
        {
            position = new Vector2Int(i, 0);
            for (int j = 0; j < 4; j++)
            {
                if (objectsReference[i, j].layer == 3 && objectsReference[i, j].GetComponent<Squares>().GetScore() == neededScore)
                {
                    SwapAfterPoint(i, j, ref neededScore, ref position);
                }
                else if (objectsReference[i, j].layer == 3)
                {
                    neededScore = objectsReference[i, j].GetComponent<Squares>().GetScore();

                    for (int k = 0; k < j; k++)
                    {
                        if (objectsReference[i, k].layer != 3)
                        {
                            SwapWithNoPointHorizontal(i, j, k, ref position);
                            break;
                        }
                        else
                            position = new Vector2Int(i, j);
                    }
                }
            }
            neededScore = 0;

        }
    }

    void MoveRight()
    {
        int neededScore = 0;
        Vector2Int position;

        for (int i = 3; i >= 0; i--)
        {
            position = new Vector2Int(i, 3);
            for (int j = 3; j >= 0; j--)
            {
                if (objectsReference[i, j].layer == 3 && objectsReference[i, j].GetComponent<Squares>().GetScore() == neededScore)
                {
                    SwapAfterPoint(i, j, ref neededScore, ref position);
                }
                else if (objectsReference[i, j].layer == 3)
                {
                    neededScore = objectsReference[i, j].GetComponent<Squares>().GetScore();

                    for (int k = 3; k > j; k--)
                    {
                        if (objectsReference[i, k].layer != 3)
                        {
                            SwapWithNoPointHorizontal(i, j, k, ref position);
                            break;
                        }
                        else
                            position = new Vector2Int(i, j);
                    }
                }
            }
            neededScore = 0;

        }
    }

    void MoveUp()
    {
        int neededScore = 0;
        Vector2Int position;

        for (int i = 3; i >= 0; i--)
        {
            position = new Vector2Int(3, i);
            for (int j = 3; j >= 0; j--)
            {
                if (objectsReference[j, i].layer == 3 && objectsReference[j, i].GetComponent<Squares>().GetScore() == neededScore)
                {
                    SwapAfterPoint(j, i, ref neededScore, ref position);
                }
                else if (objectsReference[j, i].layer == 3)
                {
                    neededScore = objectsReference[j, i].GetComponent<Squares>().GetScore();

                    for (int k = 3; k > j; k--)
                    {
                        if (objectsReference[k, i].layer != 3)
                        {
                            SwapWithNoPointVertical(i, j, k, ref position);
                            break;
                        }
                        else
                            position = new Vector2Int(j, i);
                    }
                }
            }
            neededScore = 0;
        }
    }

    void MoveDown()
    {
        int neededScore = 0;
        Vector2Int position;

        for (int i = 0; i < 4; i++)
        {
            position = new Vector2Int(0, i);
            for (int j = 0; j < 4; j++)
            {
                if (objectsReference[j, i].layer == 3 && objectsReference[j, i].GetComponent<Squares>().GetScore() == neededScore)
                {
                    SwapAfterPoint(j, i, ref neededScore, ref position);
                }
                else if (objectsReference[j, i].layer == 3)
                {
                    neededScore = objectsReference[j, i].GetComponent<Squares>().GetScore();

                    for (int k = 0; k < j; k++)
                    {
                        if (objectsReference[k, i].layer != 3)
                        {
                            SwapWithNoPointVertical(i, j, k, ref position);
                            break;
                        }
                        else
                            position = new Vector2Int(j, i);
                    }
                }
            }
            neededScore = 0;
        }
    }

    void SwapAfterPoint(int i, int j, ref int neededScore, ref Vector2Int position)
    {
        Destroy(objectsReference[i, j]);
        objectsReference[i, j] = Instantiate(empty);
        objectsReference[position.x, position.y].GetComponent<Squares>().SetScore();
        text.GetComponent<Score>().score += 2 * neededScore;
        neededScore = 0;
        moved = true;
    }

    void SwapWithNoPointHorizontal(int i, int j, int k, ref Vector2Int position)
    {
        Destroy(objectsReference[i, k]);
        objectsReference[i, k] = Instantiate(objectsReference[i, j]);
        Destroy(objectsReference[i, j]);
        objectsReference[i, j] = Instantiate(empty);
        position = new Vector2Int(i, k);
        moved = true;
    }
    void SwapWithNoPointVertical(int i, int j, int k, ref Vector2Int position)
    {
        Destroy(objectsReference[k, i]);
        objectsReference[k, i] = Instantiate(objectsReference[j, i]);
        Destroy(objectsReference[j, i]);
        objectsReference[j, i] = Instantiate(empty);
        position = new Vector2Int(k, i);
        moved = true;
    }

    void AddRandomly()
    {
        while (true)
        {
            int positionX = Random.Range(0, 4);
            int positionY = Random.Range(0, 4);


            if (objectsReference[positionX, positionY].layer != 3)
            {
                int number = Random.Range(1, 101);
                GameObject newSquare = Instantiate(square);

                if (number < 100 && number > 95)
                {
                    newSquare.GetComponent<Squares>().SetScore();

                }
                else if (number == 100)
                {
                    newSquare.GetComponent<Squares>().SetScore();
                    newSquare.GetComponent<Squares>().SetScore();

                }

                Destroy(objectsReference[positionX, positionY]);
                objectsReference[positionX, positionY] = newSquare;
                ActualiseScoreTable();

                break;
            }
        }

    }

    void ShowTable()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                objectsReference[i, j].transform.position = new Vector3(1.5f * j - 2.25f, 1.5f * i - 3.65f, 0);

    }

    void ActualiseScoreTable()
    {
        for (int i = 0; i < 4; i++) 
        {
            for (int j = 0; j < 4; j++) 
            {
                if (objectsReference[i, j].layer == 3)
                    scoreReference[i, j] = objectsReference[i, j].GetComponent<Squares>().GetScore();
            }
        }
    }

    void CheckEnd()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                if (scoreReference[i, j] == 0)
                    return;
 
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
               if (scoreReference[i, j] == scoreReference[i, j + 1] || scoreReference[j,i] == scoreReference[j+1,i])
                    return;



        SceneManager.LoadScene("Menu");

    }



 
}