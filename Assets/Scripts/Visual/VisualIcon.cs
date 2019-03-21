using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class VisualIcon : MonoBehaviour {
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private Material m_Material;
    private void Start()
    {
        GetComponent<Renderer>().material = new Material(m_Material);
        m_Animator = GetComponent<Animator>();
    }

    public void ExplodeIcon()
    {
        m_Animator.SetTrigger("Explosion");
    }
}
