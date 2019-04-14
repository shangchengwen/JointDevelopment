using Mono.Csv;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class WriteCSVTest : MonoBehaviour
{

    public string name;
    string fullPath;
    List<List<string>> data = new List<List<string>>();

    // Use this for initialization
    void Start()
    {
        //fullPath = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + name + ".csv";
        fullPath = Application.dataPath + @"\KIT\CSV\Example\" + name + ".csv";
        Debug.LogError(fullPath);
        for (int i = 0; i < 10; i++)
        {
            List<string> temp = new List<string>();
            for (int j = 0; j < 10; j++)
            {
                temp.Add((i * j).ToString());
            }
            for (int k = 0; k < temp.Count; k++)
            {
                Debug.LogError("%:" + temp[k]);
            }
            data.Add(temp);
        }

        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < data[i].Count; j++)
            {
                Debug.LogError("@:" + data[i][j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.LogError("写入数据");
            CsvFileWriter.WriteAll(data, fullPath, Encoding.GetEncoding("gbk"));
        }
    }
}

