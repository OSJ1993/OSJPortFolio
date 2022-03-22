using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Text ���� ����.
    [SerializeField] Text txtScore = null;

    //���� �� ���� ���� �ö󰡰� �� ����Ʈ����.
    [SerializeField] int increasaseScore = 10;

    //���� ������ ���� ����.
    int currentScore = 0;

    //������ ���� ����ġ�� �ٸ� �� �ְ� ���ִ� ���� ����.
    [SerializeField] float[] weight = null;

    //�޺� ���ʽ�.
    [SerializeField] int comboBonusScore = 10;


    Animator myAnim;
    string animScoreUp = "ScoreUp";

    //������ �ö��ٴ� ���� �ùٸ� �������� ����ٴ� �� �׷��� �޺��� �÷��ֱ�.
    ComboManager theCombo;


    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";
    }

    //� ��Ʈ ������ �޾ƿԴ� Ȯ��.
    public void IncreasaseScore(int p_JudgementState)
    {
        //�޺� ����.
        theCombo.IncrcascCombo();

        //�޺� ���ʽ� ���� ���.(���� �޺�/10) *10
        int t_currentCombo = theCombo.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        //����ġ ���.
        //�ӽ� ������ ������ ������ �־��ְ� ���⿡ ������ ���� ����ġ�� �־��ֱ�.
        int t_increasaseScore = increasaseScore + t_bonusComboScore;
        t_increasaseScore = (int)(t_increasaseScore * weight[p_JudgementState]);

        //���� �ݿ�.
        //�� ������ ���� ������ ���ϱ�.
        currentScore += t_increasaseScore;
        //���ڿ� ���� : ��ȭ, ����, �Ҽ���, ��¥ ǥ�� �������� ��ȯ������
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        //�ִ� ���.
        myAnim.SetTrigger(animScoreUp);
    }
}
