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

    [Header("Camera")]
    [Range(1, 10)]
    [SerializeField] private float m_RotateSpeed = 5;
    [Range(1, 1000)]
    [SerializeField] private float m_ZoomSpeed = 1000;

    void Start()
    {
        m_ButtonR.OnClickAsObservable().Subscribe(_ => m_MeshRenderer.material.SetColor("BaseColor_Ref", Color.red)).AddTo(this);
        m_ButtonG.OnClickAsObservable().Subscribe(_ => m_MeshRenderer.material.SetColor("BaseColor_Ref", Color.green)).AddTo(this);
        m_ButtonB.OnClickAsObservable().Subscribe(_ => m_MeshRenderer.material.SetColor("BaseColor_Ref", Color.blue)).AddTo(this);


        Observable.EveryUpdate().Where(_x => Input.GetMouseButton(0)).Subscribe(_ =>
        {
            RotateCameraAroundObject(m_MeshRenderer.transform.position);
        }).AddTo(this);

        Observable.EveryUpdate().Where(_x => Input.GetAxis("Mouse ScrollWheel") != 0).Subscribe(_ =>
        {
            ScrollToZoom();
        }).AddTo(this);
    }

    float _RotationX = 0f;
    float _RotationY = 0f;
    void RotateCameraAroundObject(Vector3 _target)
    {
        var mouseX = Input.GetAxis("Mouse X") * m_RotateSpeed;
        var mouseY = Input.GetAxis("Mouse Y") * m_RotateSpeed;
        var distanceToTarget = Vector3.Distance(Camera.main.transform.position, _target);

        _RotationX += mouseX;
        _RotationY += mouseY;

        _RotationY = Mathf.Clamp(_RotationY, -90f, 90f);

        var nextRot = new Vector3(-_RotationY, _RotationX, 0);
        Camera.main.transform.localEulerAngles = nextRot;

        var nextPos = _target - Camera.main.transform.forward * distanceToTarget;
        Camera.main.transform.position = nextPos;
    }

    void ScrollToZoom()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.transform.Translate(Vector3.forward * scroll * Time.deltaTime * m_ZoomSpeed);
    }
}
