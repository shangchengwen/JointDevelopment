using Mono.Csv;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public enum Enumknowledge_bankHead : int
{
    GameID = 0, QuestionID, QuestionContent, Answers
}

public class ReadCSVTest : MonoBehaviour
{

    public string name;
    string fullPath;

    // Use this for initialization
    void Start()
    {
        //fullPath = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + name + ".csv";
        fullPath = Application.dataPath + @"\KIT\CSV\Example\" + name + ".csv";
        Debug.LogError(fullPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            List<List<string>> data = CsvFileReader.ReadAll(fullPath, Encoding.GetEncoding("gbk"));
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    Debug.LogError(i + "," + j + ":" + data[i][j]);
                }
            }

            if (null != data && data.Count > 1)
            {
                List<string> head = data[0];
                data.RemoveAt(0);
                RecData(data, head);
            }
        }
    }

    private void RecData(List<List<string>> csvData, List<string> head)
    {

        if (csvData != null)
        {
            InitData(csvData);
        }
        else
        {
            Debug.Log("ad data is valid.");
        }
    }

    void InitData(List<List<string>> allData)
    {
        for (int i = 0; i < allData.Count; i++)
        {
            List<string> item = allData[i];
            string gameID = Convert.ToString(item[(int)Enumknowledge_bankHead.GameID]);
            string questionID = Convert.ToString(item[(int)Enumknowledge_bankHead.QuestionID]);
            string questionContent = Convert.ToString(item[(int)Enumknowledge_bankHead.QuestionContent]);
            List<string> answersList = new List<string>();
            for (int j = (int)Enumknowledge_bankHead.Answers; j < item.Count; j++)
            {
                if (string.IsNullOrEmpty(item[j]))
                {
                    continue;
                }
                answersList.Add(item[j]);
            }
        }
    }
}

