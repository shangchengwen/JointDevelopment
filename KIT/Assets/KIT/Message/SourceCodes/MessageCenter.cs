//消息中心，添加删除监听，发送消息
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KIT
{
    public delegate void MessageEvent(Message message);

    public class MessageCenter
    {
        /// <summary>
        /// 一个事件 有多个监听者（处理消息的方法列表）
        /// </summary>
        private Dictionary<string, List<MessageEvent>> dicMessageEvents = null;

        #region singleton 单例模式

        private MessageCenter()
        {
            Init();
        }

        protected static MessageCenter _Instance = null;

        public static MessageCenter Instance
        {
            get
            {
                if (null == _Instance)
                {
                    _Instance = new MessageCenter();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 在创建本类的同时初始化 事件字典列表
        /// </summary>
        public void Init()
        {
            dicMessageEvents = new Dictionary<string, List<MessageEvent>>();
        }

        #endregion

        #region Add & Remove Listener
        //添加监听者（监听的消息名称，处理该消息的方法）
        public void AddListener(string messageName, MessageEvent messageEvent)
        {
            //Debug.Log("AddListener Name : " + messageName);
            List<MessageEvent> list = null;
            //1.查看有没有这个消息 有就获取处理列表 没有新建处理列表
            if (dicMessageEvents.ContainsKey(messageName))
            {
                list = dicMessageEvents[messageName];
            }
            else
            {
                list = new List<MessageEvent>();
                dicMessageEvents.Add(messageName, list);
            }

            //2.向列表中添加处理消息的方法
            // no same messageEvent then add
            if (!list.Contains(messageEvent))
            {
                list.Add(messageEvent);
            }
        }

        /// <summary>
        /// 删除监听者（监听的消息名称，处理该消息的方法）
        /// </summary>
        /// <param name="messageName"></param>
        /// <param name="messageEvent"></param>
        public void RemoveListener(string messageName, MessageEvent messageEvent)
        {
            //Debug.Log("RemoveListener Name : " + messageName);
            if (dicMessageEvents.ContainsKey(messageName))
            {
                List<MessageEvent> list = dicMessageEvents[messageName];
                if (list.Contains(messageEvent))
                {
                    list.Remove(messageEvent);
                }
                if (list.Count <= 0)
                {
                    dicMessageEvents.Remove(messageName);
                }
            }
        }

        /// <summary>
        /// 删除所有消息
        /// </summary>
        public void RemoveAllListener()
        {
            dicMessageEvents.Clear();
        }

        #endregion


        #region Send Message 发送消息

        public void SendMessage(Message message)
        {
            DoMessageDispatcher(message);
        }

        public void SendMessage(string name, object sender)
        {
            SendMessage(new Message(name, sender));
        }

        public void SendMessage(string name, object sender, object content)
        {
            SendMessage(new Message(name, sender, content));
        }

        public void SendMessage(string name, object sender, object content, params object[] dicParams)
        {
            SendMessage(new Message(name, sender, content, dicParams));
        }

        private void DoMessageDispatcher(Message message)
        {
            if (dicMessageEvents == null || !dicMessageEvents.ContainsKey(message.Name))
                return;
            List<MessageEvent> list = dicMessageEvents[message.Name];
            for (int i = 0; i < list.Count; i++)
            {
                MessageEvent messageEvent = list[i];
                if (null != messageEvent)
                {
                    messageEvent(message);
                }
            }
        }

        #endregion
    }

}

