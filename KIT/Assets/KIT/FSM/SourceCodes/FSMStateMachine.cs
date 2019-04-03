using UnityEngine;

namespace KIT
{
    /// <summary>
    /// 状态机（跳转逻辑）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FSMStateMachine<T> : FSMStateContainer<T> where T : MonoBehaviour
    {
        public FSMStateMachine(T entity) : base(entity)
        {

        }

        #region 根据状态名称跳转状态
        public void Push(string name)
        {
            //从字典中取出状态
            FSMState<T> state = GetFromDic(name);
            Push(state);
        }

        public void Push(FSMState<T> state)
        {
            if (state == null)
            {
                return;
            }
            if (m_states.Count > 0)
            {
                Pop(m_states[0]);
            }
            PushOne(state);
        }

        protected void PushOne(FSMState<T> state)
        {
            if (!ContainsState(state))
            {
                state.Enter();
                Add(state);
            }
        }
        #endregion

        /// <summary>
        /// 退出上一个状态
        /// </summary>
        /// <param name="state"></param>
        public void Pop(FSMState<T> state)
        {
            if (ContainsState(state))
            {
                Remove(state);
                state.Exit();
            }
        }

        public virtual void Update()
        {
            for (int i = 0; i < m_states.Count; i++)
            {
                FSMState<T> state = m_states[i];
                state.Execute();
            }
        }
    }
}