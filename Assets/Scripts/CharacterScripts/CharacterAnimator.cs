using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replacebleAttackAnimation;
    public AnimationClip[] defaultAttackAnimations;
    protected AnimationClip[] currentAttackAnimations;

    const float animMoveSmoothTime = .1f;
    float speedPercent;

    NavMeshAgent agent;

    protected Animator anim;
    protected AnimatorOverrideController overrideController;
    protected CharacterCombat combat;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        combat = GetComponent<CharacterCombat>();
        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;

        currentAttackAnimations = defaultAttackAnimations;

        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        speedPercent = agent.velocity.magnitude / agent.speed;

        anim.SetFloat("Speed", speedPercent, animMoveSmoothTime, Time.deltaTime);
        anim.SetBool("InCombat", combat.inCombat);
    }

    protected virtual void OnAttack()
    {
        anim.SetTrigger("Attack");
        int attackIndex = Random.Range(0, currentAttackAnimations.Length);
        overrideController[replacebleAttackAnimation.name] = currentAttackAnimations[attackIndex];

        

    }
}
