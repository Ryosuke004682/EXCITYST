using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

/*Player�̈ړ��e�X�g*/

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove_Test : MonoBehaviour
{
    [Header("�ړ��p�p�����[�^�[")]
    public float _moveSpeed;
    public float groundDragPower;


    [Header("�n�ʌ��m�p�p�����[�^�[")]
    public float _player_CenterHight;//player�̖񒆐S����n�ʂ̍���
    public LayerMask isGround;
    bool grounded;


    public Transform _direction;

    float _horizontal;
    float _vertical;

    Vector3 moveDirection;

    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down,
            _player_CenterHight * 0.5f + 0.2f, isGround);


        //�����n�ʂɐڒn���Ă��������͂��l�������i���C���l���邽�߁j
        if (grounded)
        {
            _rb.drag = groundDragPower;
        }
        else
        {
            _rb.drag = 0;
        }


        MoveInput();
        SpeedController();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MoveInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical   = Input.GetAxisRaw("Vertical");
    }


    /// <summary>
    /// �v���C���[�̈ړ������̌v�Z������B
    /// </summary>
    private void MovePlayer()
    {

        moveDirection = _direction.forward * _vertical
                                + _direction.right * _horizontal;

        _rb.AddForce(moveDirection.normalized * _moveSpeed * 10, ForceMode.Force);
    }

    /// <summary>
    ///  �ݒ肵���X�s�[�h�����傫���Ȃ��Ă��邩�琧��B
    /// </summary>
    private void SpeedController()
    {
        Vector3 limitSpeed = new(_rb.velocity.x, 0.0f, _rb.velocity.z);

        //���x�̃��~�b�g��ݒ肷��
        if (limitSpeed.magnitude > _moveSpeed)
        {
            Vector3 limited = limitSpeed.normalized * _moveSpeed;
            _rb.velocity = new Vector3(limited.x, _rb.velocity.y, limited.z);
        }
    }
}
