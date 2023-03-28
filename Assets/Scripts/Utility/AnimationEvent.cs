using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private System.Action[] aniEventActions = new System.Action[3];

    public void SetEventAction( params System.Action[] actions )
    {
        for(int i = 0; i < actions.Length; ++i)
        {
            aniEventActions[i] = actions[i];
        }
    }

    public void EventCall(int index)
    {
        aniEventActions[index]?.Invoke();
    }
}
