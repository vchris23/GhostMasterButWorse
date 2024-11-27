using System.Collections;
using System.Collections.Generic;
using KadaXuanwu.UtilityDesigner.Scripts.Execution.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class PlayAnimation : ActionNode
{
    private Animator humanAnimator;
    private Humanblackboard humanBlackboard;
    private UtilityEntity target;
    private AnimatorOverrideController overrider;
    private AnimationClip placeholder;
    private NavMeshAgent agent;
    private RuntimeAnimatorController originanlAnimator;
    private bool hasBegun;

    protected override void OnEnable()
    {
        agent = ThisGameObject.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.updateRotation = false;
        humanAnimator = ThisGameObject.GetComponent<Animator>();
        humanBlackboard = ThisGameObject.GetComponent<Humanblackboard>();
        target = humanBlackboard.target;
        ThisGameObject.transform.forward = target.interactionSpot.transform.forward;
        overrider = new AnimatorOverrideController(humanAnimator.runtimeAnimatorController);
        originanlAnimator = MonoBehaviour.Instantiate(humanAnimator.runtimeAnimatorController);
        hasBegun = false;
        KeyValuePair<AnimationClip, AnimationClip> clips = new KeyValuePair<AnimationClip, AnimationClip>();
        foreach (AnimationClip clip in overrider.animationClips)
        {
            if (clip.name == "Placeholder")
            {
                //Debug.Log("Found placeholder for animation objects: " +  humanBlackboard.target.name);
                placeholder = clip;
                target.humanInteractionClip.legacy = false;
                clips = new KeyValuePair<AnimationClip, AnimationClip>( placeholder, target.humanInteractionClip);
            }
        }

        overrider["Placeholder"] = target.humanInteractionClip;
        humanAnimator.runtimeAnimatorController = overrider;
        humanAnimator.SetTrigger("DoNeedAnimation");
    }


    protected override NodeState OnUpdate()
    {
        if (!hasBegun)
        {
            //Debug.Log("Has not begun");
            if (humanAnimator.GetCurrentAnimatorStateInfo(0).IsName("Placeholder"))
            {
                hasBegun = true;
            }

            return NodeState.Running;
        }

        if (humanAnimator.GetCurrentAnimatorStateInfo(0).IsName("Placeholder"))
        {
            //Debug.Log("Playing placeholder");
            return NodeState.Running;
        }
        //Debug.Log("Return success for: " +  humanBlackboard.target.name);
        return NodeState.Success;
        
    }

    protected override void OnDisable()
    {
        if(target.occupiedBy?.gameObject == ThisGameObject) target.occupiedBy = null;
        overrider[target.humanInteractionClip.name] = placeholder;
        agent.isStopped = false;
        agent.updateRotation = true;
        humanAnimator.runtimeAnimatorController = originanlAnimator;

    }
}
