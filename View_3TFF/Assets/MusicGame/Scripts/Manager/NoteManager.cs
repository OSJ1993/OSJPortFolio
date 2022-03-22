using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoteManager : MonoBehaviour
{

    public int bpm = 0;

    //��Ʈ ������ ���� �� �ð� üũ���� ����.
    double curentTime = 0d;

    //��Ʈ�� ���� �� ��ġ ����.
    [SerializeField] Transform tfNoteAppear = null;


    //TimingManager theTimingManager ������ �� �ְ� ����� �ֱ�.
    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager;
    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theTimingManager = GetComponent<TimingManager>();
    }



    void Update()
    {
        //curentTime�� 1�ʿ� 1�� ����
        curentTime += Time.deltaTime;

        //�׷��ٰ� curentTime�� 60s�� ������ bpm���� Ŀ���� ��Ʈ 1���� ����ӵ�.
        if (curentTime >= 60d / bpm)
        {
            GameObject t_note = ObjectPool.instnace.noteQueue.Dequeue();
            t_note.transform.position = tfNoteAppear.position;
            t_note.SetActive(true);



            //��Ʈ�� �����Ǵ� ���� ��ƮList�� �ش� ��Ʈ�� �߰�.
            theTimingManager.boxNoteList.Add(t_note);

            //curentTime�� 0�� �ƴ� 60d/bpm�� ���ֱ�.(0���� �ϸ� �ȵǴ� ������ ���� ���� ������ ����� �����̴�.
            curentTime -= 60d / bpm;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //�ݶ��̴� ���� �����ų� ������ �ߵ��ϴ� �Լ�.
        //Note �ױ׷� �� �ݶ��̴��� �����Ǹ� �� ��ü�� �ı�.
        if (collision.CompareTag("Note"))
        {
            //Note�� �ִ� public bool GetNoteFlag()�� enabled true�� Image�� ������ �� ����  theEffectManager.JudgementEffect(4);����.
            //�ε��� ��ü�� ��Ʈ ��ũ���� �����ͼ� GetNoteFlag �Լ� ȣ�� true�� ���� �������.
            if (collision.GetComponent<Note>().GetNoteFlag())
            {

                //��Ʈ�� ȭ�� ������ ������ �� ������ ���� 4(miss)�Ѱܼ� ����ǰ� �ϱ�.
                theEffectManager.JudgementEffect(4);

                //���ļ� �̽��ߴ� ������ �޺��ʱ�ȭ �־��ֱ�.
                theComboManager.ResetCombo();

            }


            //��Ʈ�� �ı��Ǵ� �������� �ش� ��Ʈ�� List���� ����.
            theTimingManager.boxNoteList.Remove(collision.gameObject);

            //��Ʈ �̹��� �����غ���. noteImage.enabled�� false�� �� ����.


            //��Ʈ Queue�� �ݳ������ְ� ��Ȱ��ȭ ����.
            ObjectPool.instnace.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);




        }
    }
}
