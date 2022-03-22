using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    //������ ��Ʈ�� ��� List�����. ���������� �ִ� �� ��� ��Ʈ�� ���ؾ���.
    public List<GameObject> boxNoteList = new List<GameObject>();

    //���������� �߽��� �˷��ִ� Center���� ����.
    [SerializeField] Transform Center = null;

    //�پ��� ���������� ������ RectTransform[]�迭�� ����.
    [SerializeField] RectTransform[] timingRect = null;

    //���� ���� �ǵ��� �� Vector2[] ����. ���⿡ RectTransform ��?�� �����ٰ��Դϴ�.
    Vector2[] timingBoxs = null;


    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager theComboManager;


    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();

        //Ÿ�̹� �ڽ� ����.
        //timingBoxs �� ũ��� timingRect ��?���� �־��ֱ�.
        timingBoxs = new Vector2[timingRect.Length];

        //timingBoxs ��������.
        for (int i = 0; i < timingRect.Length; i++)
        {
            //������ ���� ���� => �ּҰ� = �߽� - (�̹����� �ʺ� / 2)
            //                    �ִ밪 = �߽� + (�̹����� �ʺ� / 2)
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }


    //�����Լ�.
    public bool CheckTiming()
    {
        //����Ʈ�� �ִ� ��Ʈ���� Ȯ���ؼ� ���� �ڽ��� �ִ� ��Ʈ�� ã�ƾ���.
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            //�� ��Ʈ�� x���� ���� �޾Ƽ� �� ������ ���������ȿ� ���Դ��� �Ǵ�.
            //�������� �ּҰ� <= ��Ʈ�� x�� <= �������� �ִ밪.
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            //�� ��Ʈ���� �������� �ȿ� �ִ� �� Ȯ���ؾ��ϰ� �� ���������� �迭�̱� ������ �ݺ������� ����.
            for (int x = 0; x < timingBoxs.Length; x++)
            {
                //���ǹ� ��Ʈ��x���� �������� �ȿ� ���� �ִ� �� �� x�ּҰ� �ִ밪y ��
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    //��Ʈ ����
                    //0��°�� ����Ʈ
                    //�ε��� 0���� Ȯ���ϹǷ� ���������� Perfect -> Cool -> Good -> Bad

                    boxNoteList[i].GetComponent<Note>().HideNote();


                    //�ش� ��Ʈ �ε����� �̿��ؼ� ��Ʈ�� ���ִ� �ڵ�.
                   
                    boxNoteList.RemoveAt(i);

                    //����Ʈ ����
                    //BadŸ�ֿ̹��� Effect�� ������ �ʰ� ���ֱ�.
                    //�ε��� 0:����Ʈ 1:�� 2 �� 3���� �̴� -1�̸� 0:����Ʈ 1:�� 2 �� �� �� ����Ʈ���.
                    if (x < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();

                    //�Ķ��Ʈ ���� x���� �Ѱ��ֱ�.
                    theEffect.JudgementEffect(x);

                    //���� ����
                    theScoreManager.IncreasaseScore(x);


                    return true;
                }
            }
        }

        //�޺� �ʱ�ȭ
        theComboManager.ResetCombo();

        theEffect.JudgementEffect(timingBoxs.Length);
        return false;
    }
}
