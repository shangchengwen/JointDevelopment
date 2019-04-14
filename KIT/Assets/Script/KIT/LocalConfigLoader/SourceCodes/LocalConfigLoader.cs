//根据自己的表格式再进行加载
using UnityEngine;
using System.Collections;
using System.IO;
using System;

//1.用来读取json格式的配置表
//2.配置表有中文的用Notepad++进行编辑 编码格式使用UTF-8无BOM格式编码
//3.数组用ArrayList接收
//4.对象和数据用Hashtable接收
namespace KIT
{
    public class LocalConfigLoader
    {
        //读到表的回调
        public delegate void OnLoadCompletedDelegate(object zipdata, object param);

        //读表的地址
        const string configPath = "/Local/Config/";

        // 加载本地配置数据
        public static void GetLocalData(string fileName, OnLoadCompletedDelegate callback)
        {
            string fileData = string.Empty;
            string fullPath = GetConfigFullPath(fileName);
            if (File.Exists(fullPath))
            {
                byte[] fileBytes = ReadFile(fullPath);
                fileData = System.Text.Encoding.UTF8.GetString(fileBytes);
                Debug.LogError(fileData);
            }
            object jsonObj = MiniJSON.jsonDecode(fileData);
            if (jsonObj == null)
            {
                Debug.Log("Data conversion fails, Get the data address name：" + fileName);
            }
            if (null != jsonObj && (jsonObj is Hashtable) && null != callback)
            {
                Hashtable hashJson = jsonObj as Hashtable;
                int result = int.Parse(hashJson["return_code"].ToString());
                callback(hashJson["data_content"], result);
            }
            else
            {
                Debug.Log("decode json error filename:" + fileName);
                callback(null, 0);
            }
        }

        public static byte[] ReadFile(string fileName)
        {
            FileStream pFileStream = null;

            byte[] pReadByte = null;

            try
            {
                pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                BinaryReader r = new BinaryReader(pFileStream);

                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开

                pReadByte = r.ReadBytes((int)r.BaseStream.Length);
            }

            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }

            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }

            return pReadByte;
        }

        static string GetConfigFullPath(string path)
        {
            return Application.streamingAssetsPath + configPath + path;
        }
    }
}
