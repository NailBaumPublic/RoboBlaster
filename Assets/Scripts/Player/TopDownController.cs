using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour // 호출할 이벤트 작성
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnFireEvent;
    public event Action OnSkillEvent;
    public event Action OnDeathEvent; // 사망 이벤트 추가

    protected bool isAttacking {  get; set; }
    protected bool isSkill {  get; set; }
    protected bool skillUsed { get; set; } // 스킬 사용 여부를 추적

    private float timeSinceLastAttack = float.MaxValue;
    private float timeSinceLastSkill = float.MaxValue;

    protected virtual void Update()
    {
        HandleAttackDelay();
        HandleSkillDelay();
    }

    private void HandleSkillDelay() // 추후 스킬 로직 변경시 수정 필요
    {
        if (timeSinceLastSkill <= 0.2f)    
        {
            timeSinceLastSkill += Time.deltaTime;
        }

        if (isSkill && !skillUsed && timeSinceLastSkill > 0.2f)
        {
            timeSinceLastSkill = 0;
            CallSkillEvent();
            skillUsed = true; // 스킬이 사용되었음을 표시
            isSkill = false; // 스킬 플래그 재설정
        }
    }

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack <= 0.2f)    
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > 0.2f)
        {
            timeSinceLastAttack = 0;
            CallFireEvent();
        }
    }
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallFireEvent()
    {
        OnFireEvent?.Invoke();
    }

    public void CallSkillEvent()
    {
        OnSkillEvent?.Invoke();
    }
    public void CallDeathEvent()
    {
        OnDeathEvent?.Invoke(); // 사망 이벤트 호출
    }
    public void ResetSkillUsed()
    {
        skillUsed = false; // 스킬 사용 여부 재설정
    }
}
