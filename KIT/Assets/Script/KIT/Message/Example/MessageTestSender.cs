using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KIT;

public class MessageTestSender : MonoBehaviour {

    public string name = "消息发送者";

    void Update()
    {
        //方式一 直接发送消息（消息名称，发送者，消息）
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MessageCenter.Instance.SendMessage(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_ONE, this, "Hello");
        }
        //方式二 利用索引器携带信息(信息是Object类型的)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Message message = new Message(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_TWO, this);
            message["name"] = "小明";
            message["age"] = 12;
            message.Send();
        }
        //方式三
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Message message = new Message(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_THREE, this);
            message["name"] = "小明";
            message.Add("height", 173);
            message.Send();
        }
        //方式四
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Message message = new Message(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_FOUR, this);
            message["name"] = "小明";
            message["age"] = 12;
            message.Send();
        }
    }
}

class Book
{
}
