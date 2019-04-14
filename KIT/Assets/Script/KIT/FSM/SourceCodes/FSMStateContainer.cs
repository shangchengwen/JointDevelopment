using UnityEngine;
using System;
using System.Collections.Generic;

namespace KIT
{
    /// <summary>
    /// 状态机（状态模块容器）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FSMStateContainer<T> where T : MonoBehaviour
    {
        #region Members
        /// <summary>
        /// 状态机本身
        /// </summary>
        protected T mEntity;

        /// <summary>
        /// 状态列表，当前的状态
        /// </summary>
        protected List<FSMState<T>> m_states = new List<FSMState<T>>();

        /// <summary>
        /// 状态字典，全部的状态
        /// </summary>
        protected Dictionary<string, FSMState<T>> m_statesDic = new Dictionary<string, FSMState<T>>();
        #endregion

        public FSMStateContainer(T entity)
        {
            mEntity = entity;
        }

        /// <summary>
        /// 获取当前的状态机
        /// </summary>
        /// <returns></returns>
        public T GetEntity()
        {
            return mEntity;
        }

        #region 将状态模块添加到状态机中
        /// <summary>
        /// 添加状态机模块
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="name"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TS CreateAndAdd<TS>(string name, T entity) where TS : FSMState<T>
        {
            try
            {
                TS state = (TS)Activator.CreateInstance(typeof(TS), name, entity, this);
                Add2Dic(state);
                return state;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return null;
        }

        /// <summary>
        /// 将状态添加到字典中
        /// </summary>
        /// <param name="state"></param>
        protected void Add2Dic(FSMState<T> state)
        {
            if (!m_statesDic.ContainsKey(state.mName))
            {
                m_statesDic.Add(state.mName, state);
            }
        }
        #endregion


        /// <summary>
        /// 从字典中获取状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected FSMState<T> GetFromDic(string name)
        {
            if (m_statesDic.ContainsKey(name))
            {
                return m_statesDic[name];
            }
            return null;
        }

        /// <summary>
        /// 添加状态机模块到“状态列表”
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected bool Add(FSMState<T> state)
        {
            if (state == null)
            {
                return false;
            }

            foreach (FSMState<T> ts in m_states)
            {
                if (ts.mName == state.mName)
                {
                    Debug.Log("State Already Exist In this MOFFSMStateContainer!");
                    return false;
                }
            }
            m_states.Add(state);
            return true;
        }

        /// <summary>
        /// 从“状态列表”中移除状态
        /// </summary>
        /// <param name="state"></param>
        protected void Remove(FSMState<T> state)
        {
            m_states.Remove(state);
            state = null;
        }

        /// <summary>
        /// “状态列表”中是否包含该状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool ContainsState(FSMState<T> state)
        {
            return m_states.Contains(state);
        }
    }
}