using System.Collections;
using UnityEngine;

namespace metakazz.FSM
{
    /// <summary>
    /// Base interface for creating a state as part of an IStateMachine where <typeparamref name="T"/> is the type of the context passed into each state.
    /// The context is passed in to help the IState perform meaningful actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IState<T> where T : MonoBehaviour
    {
        void Enter(T ctx);
        void Exit(T ctx);
        void Update(T ctx);
    }
}