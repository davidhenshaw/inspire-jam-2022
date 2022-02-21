using UnityEngine;

namespace metakazz.FSM
{
    public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
    {
        IState<T> _currState;

        /// <summary>
        /// Used to initialize the first state of the StateMachine
        /// </summary>
        public void Init(IState<T> first)
        {
            _currState = first;
            first.Enter(this as T);
        }

        public virtual void Update()
        {
            if (_currState != null)
                _currState.Update(this as T);
            else
                Debug.LogError("No state to update. Did you call the Init() function?", this);
        }

        public void SwitchState(IState<T> newState)
        {
            _currState.Exit(this as T);
            newState.Enter(this as T);

            _currState = newState;
        }
    }
}
