//定义一个消息结构，用来作为传递消息的媒介
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KIT
{

    public class Message : IEnumerable<KeyValuePair<string, object>>
    {

        private Dictionary<string, object> dicDatas = null;  //通过索引器存放消息

        public string Name { get; private set; }
        public object Sender { get; private set; }
        public object Content { get; set; }

        //索引器设置
        #region message[key] = value or data = message[key]

        /// <summary>
        /// Gets or sets the <see cref="EdUiFrameWork.Message"/> with the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        public object this[string key]
        {
            get
            {
                if (null == dicDatas || !dicDatas.ContainsKey(key))
                    return null;
                return dicDatas[key];
            }
            set
            {
                if (null == dicDatas)
                    dicDatas = new Dictionary<string, object>();
                if (dicDatas.ContainsKey(key))
                    dicDatas[key] = value;
                else
                    dicDatas.Add(key, value);
            }
        }

        #endregion

        //可以使用foreach遍历元素
        #region IEnumerable implementation

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            if (null == dicDatas)
                yield break;
            foreach (KeyValuePair<string, object> kvp in dicDatas)
            {
                yield return kvp;
            }
        }

        #endregion

        #region IEnumerable implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dicDatas.GetEnumerator();
        }

        #endregion

        //构造方法
        #region Message Construction Function

        /// <summary>
        /// Initializes a new instance of the <see cref="EdUiFrameWork.Message"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="sender">Sender.</param>
        public Message(string name, object sender)
        {
            Name = name;
            Sender = sender;
            Content = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdUiFrameWork.Message"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="content">Content.</param>
        public Message(string name, object sender, object content)
        {
            Name = name;
            Sender = sender;
            Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdUiFrameWork.Message"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="content">Content.</param>
        /// <param name="_dicParams">_dic parameters.</param>
        public Message(string name, object sender, object content, params object[] _dicParams)
        {
            Name = name;
            Sender = sender;
            Content = content;

            //这里功能有问题 已经可以通过索引器的形式添加多个消息内容 此部分待开发
            //思路应该是传入一个字典Dictionary<string, object>
            if (_dicParams.GetType() == typeof(Dictionary<string, object>))
            {
                foreach (object _dicParam in _dicParams)
                {
                    foreach (KeyValuePair<string, object> kvp in _dicParam as Dictionary<string, object>)
                    {
                        //dicDatas[kvp.Key] = kvp.Value;  //error
                        this[kvp.Key] = kvp.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdUiFrameWork.Message"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public Message(Message message)
        {
            Name = message.Name;
            Sender = message.Sender;
            Content = message.Content;
            foreach (KeyValuePair<string, object> kvp in message.dicDatas)
            {
                this[kvp.Key] = kvp.Value;
            }
        }

        #endregion

        //通过键值对添加数据 通过键删除数据
        #region Add & Remove

        /// <summary>
        /// Add the specified key and value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public void Add(string key, object value)
        {
            this[key] = value;
        }

        /// <summary>
        /// Remove the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        public void Remove(string key)
        {
            if (null != dicDatas && dicDatas.ContainsKey(key))
            {
                dicDatas.Remove(key);
            }
        }
        #endregion

        #region Send() 调用消息中心发送数据

        /// <summary>
        /// Send this instance.
        /// </summary>
        public void Send()
        {
            //MessageCenter Send Message
            MessageCenter.Instance.SendMessage(this);
        }
        #endregion

    }
}

