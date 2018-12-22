using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour {
    private static string m_pKEY_HAS_MATCH = "hasMatch";
    private static string m_pKEY_IS_DEAD = "isDead";
    [SerializeField]
    private Animator _aMonsterAnimator;

    public void TryReviveMonster(bool pIsDead)
    {
        _aMonsterAnimator.SetBool(m_pKEY_IS_DEAD, pIsDead);
        _aMonsterAnimator.SetTrigger(m_pKEY_HAS_MATCH);
    }
}
