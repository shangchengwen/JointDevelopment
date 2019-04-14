using KIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalConfigLoaderTest : MonoBehaviour {

    void Start()
    {
        LocalConfigLoader.GetLocalData("menu.data", RecData);
    }

    void RecData(object jsonData, object ret_code)
    {
        Hashtable data = jsonData as Hashtable;
        if ((int)ret_code > 0 && null != data)
        {
            float time = System.Convert.ToSingle(data["time"]); //属性
            Debug.LogError("time:" + time);
            bool temp = System.Convert.ToBoolean(data["bool"]); //属性
            Debug.LogError("Bool:" + temp);
            ArrayList game_list = data["game_list"] as ArrayList; //数组
            foreach (Hashtable item in game_list)
            {
                //Hashtable item 对象
                Debug.LogError(item["gamename"]);
            }
        }
        else
        {
            Debug.Log("game data is valid.");
        }
    }
}

//json例子
/*{
	"return_code": 1,
	"data_content": {
		"time": 12.25,
		"bool": true,
		"game_list": [{
				"gamename":"game one",
				"gameid": "001",
				"gameobjs": [{
					"obj_tpye": "player",
					"name": "player"
				}, {
					"obj_tpye": "enemy1",
					"name": "enemy1"
				}, {
					"obj_tpye": "enemy2",
					"name": "enemy2"
				}]
			},
			{
				"gamename": "game two",
				"gameid": "002",
				"gameobjs": [{
					"obj_tpye": "player",
					"name": "player"
				}, {
					"obj_tpye": "enemy1",
					"name": "enemy1"
				}, {
					"obj_tpye": "enemy2",
					"name": "enemy2"
				}]
			}
		]
	}
}*/
