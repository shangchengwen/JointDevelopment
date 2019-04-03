using UnityEngine;
using KIT;

public class MessageTestListener : MonoBehaviour {

    void OnEnable()
    {
        MessageCenter.Instance.AddListener(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_ONE, TheEventHappenONE);
        MessageCenter.Instance.AddListener(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_TWO, TheEventHappenTWO);
        MessageCenter.Instance.AddListener(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_THREE, TheEventHappenTHREE);
        MessageCenter.Instance.AddListener(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_FOUR, TheEventHappenFOUR);
    }
    void OnDisable()
    {
        MessageCenter.Instance.RemoveListener(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_ONE, TheEventHappenONE);
        MessageCenter.Instance.RemoveListener(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_TWO, TheEventHappenTWO);
        MessageCenter.Instance.RemoveListener(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_THREE, TheEventHappenTHREE);
        MessageCenter.Instance.RemoveListener(MessageCommand.EVENT_COMPLETE_SUCCESS_HAPPEN_FOUR, TheEventHappenFOUR);
    }
    //接收一
    void TheEventHappenONE(Message m)
    {
        if (m != null)
        {
            MessageTestSender sender = (MessageTestSender)m.Sender;
            Debug.LogError(sender.name + ":" + m.Content);
        }
    }
    //接收二
    void TheEventHappenTWO(Message m)
    {
        if (m != null)
        {
            string name = m["name"].ToString();
            int age = (int)m["age"];
            Debug.LogError("名字：" + name + " 年龄：" + age);

            //因为Message有public IEnumerator<KeyValuePair<string, object>> GetEnumerator()方法所以能遍历元素
            foreach (var value in m)
            {
                Debug.LogError(value.ToString());
            }
        }
    }
    //接收三
    void TheEventHappenTHREE(Message m)
    {
        if (m != null)
        {
            foreach (var value in m)
            {
                Debug.LogError(value.ToString());
            }
        }
    }
    //接收四
    void TheEventHappenFOUR(Message m)
    {
        if (m != null)
        {
            Debug.LogError(m.Content);
        }
    }
}
