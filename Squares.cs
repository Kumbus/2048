using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Squares : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;
    public int score = 2;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        text.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore()
    {
        score = score * 2;
    }


}
