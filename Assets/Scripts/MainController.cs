using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MainController : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button m_ButtonR;
    [SerializeField] private Button m_ButtonG;
    [SerializeField] private Button m_ButtonB;

    [Header("Mesh")]
    [SerializeField] private MeshRenderer m_MeshRenderer;

    void Start()
    {
        m_ButtonR.OnClickAsObservable().Subscribe(_ => m_MeshRenderer.material.SetColor("BaseColor_Ref", Color.red)).AddTo(this);
        m_ButtonG.OnClickAsObservable().Subscribe(_ => m_MeshRenderer.material.SetColor("BaseColor_Ref", Color.green)).AddTo(this);
        m_ButtonB.OnClickAsObservable().Subscribe(_ => m_MeshRenderer.material.SetColor("BaseColor_Ref", Color.blue)).AddTo(this);
    }
}
