using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //��ŭ ���� �ӵ��� �̵� ��ų ��.
    [SerializeField] float moveSpeed = 3;

    //������ ����.
    Vector3 dir = new Vector3();

    //������.
    Vector3 destPos = new Vector3();

    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }


    void Update()
    {
        //�� ������ ���� Ű�� ���ȴ��� Ȯ���ؾ���.
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            // ���� üũ.
            //Space�� ������ Ÿ�̹� ������ �� �ְ� üũŸ�̹� ȣ��.
            if (theTimingManager.CheckTiming())
            {
                //�ùٸ� ������ ���� �����̰�.
                StartAction();

            }
        }
    }

    void StartAction()
    {
        //��� �������� ���ȴ� �� �˱� ����.
        // �Է°� or �� ����Ű WŰ=1, or �Ʒ� ����Ű SŰ=-1 ���� �� =0
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        // �̵� ��ǥ�� ���(������)
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);
    }

   /* IEnumerator MoveCo()
    {
        //A ��ǥ�� B ��ǥ���� �Ÿ����� ��ȯ SqrMagnitude: �������� ���� ex: SqrMagnitude(4) =2
        while (Vector3.SqrMagnitude(transform.position - destPos) != 0.001f)
        {
           
        }*/
    }

