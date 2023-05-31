using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLinkJump : MonoBehaviour
{

    [SerializeField] private float jumpSpeed = 10.0f;
    [SerializeField] private float gravity = -9.8f;

    private NavMeshAgent _agent;
    private Animator _anim;

    private void Awake() 
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnJump());

            yield return StartCoroutine(JumpTo());
        }
    }
    private bool IsOnJump()
    {
        if (_agent.isOnOffMeshLink)
        {
            OffMeshLinkData linkData = _agent.currentOffMeshLinkData;

            //OffMeshLinkType Manula = 0, Drop = 1, Jump = 2
            if (linkData.linkType == OffMeshLinkType.LinkTypeJumpAcross || linkData.linkType == OffMeshLinkType.LinkTypeDropDown)
            {
                return true;
            }
        }
        
        return false;
    }

    IEnumerator JumpTo()
    {
        _agent.isStopped = true;
        _anim.SetBool(AnimType.jump.ToString(), true);

        OffMeshLinkData linkData = _agent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / jumpSpeed);
        float currentTime = 0f;
        float percent = 0f;
        //y방향의 초기속도
        float v0 = (end - start).y - gravity;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;

            Vector3 position = Vector3.Lerp(start, end, percent);

            //포물선 운동 : 시작위치 + 초기속도*시간 + 중력*시간제곱
            position.y = start.y + (v0 * percent) + (gravity * percent * percent);

            transform.position = position;

            yield return null;
        }
        _agent.CompleteOffMeshLink();
        
        _anim.SetBool(AnimType.jump.ToString(), false);
        _agent.isStopped = false;
    }
}
