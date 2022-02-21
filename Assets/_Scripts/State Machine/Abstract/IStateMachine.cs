using UnityEngine;

namespace metakazz.StateMachine
{
    public interface IStateMachine<T> where T : MonoBehaviour
    {
        void SwitchState(IState<T> newState);
    }
}
