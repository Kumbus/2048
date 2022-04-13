using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;


public class HighestScore : MonoBehaviour
{

    string highScore = "0";
    Text text;
    string sPath;

    [SerializeField]
    Text textScore;

    void Start()
    {

        //string temp = AppDomain.CurrentDomain.BaseDirectory;
        //sPath = Path.Combine(temp, "HighScore.txt");
        sPath = @"C:\Users\jakub\Desktop\Wszechswiat\Unity\2048\HighScore.txt";
        //sPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        

        bool fileExist = File.Exists(sPath);

        if (fileExist)
        {
            highScore = System.IO.File.ReadAllText(sPath);
        }
        else
        {
            using (StreamWriter outputFile = new StreamWriter(sPath))
                outputFile.WriteLine(highScore);
        }


        text = GetComponent<Text>();
        text.text = highScore;
    }

    void Update()
    {
        if(Int32.Parse(highScore) < textScore.GetComponent<Score>().score)
        {
            highScore = textScore.GetComponent<Score>().score.ToString();
            using (StreamWriter outputFile = new StreamWriter(sPath))
                outputFile.WriteLine(highScore);
        }

        text.text = "Highscore\n" + highScore;
    }
}
