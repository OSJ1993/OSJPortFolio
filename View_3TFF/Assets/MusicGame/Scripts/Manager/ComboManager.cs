using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    //����Ʈ�� �׻� ������ �� �ƴ� ���� �޺� �̻���� ����.
    [SerializeField] GameObject goComboImage = null;
    [SerializeField] Text txtCombo = null;

    //���� �� �޺��� �� ��Ÿ�� ����.
    int currentCombo = 0;

    Animator myAnim;
    string animComboUp = "ComboUp";


    void Start()
    {
        myAnim = GetComponent<Animator>();

        //ó������ �������� �ʰ� �ϱ�.
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }


    //�޺� ���� �Լ�.
    public void IncrcascCombo(int p_num = 1)
    {
        //�Ķ���͸� �ѱ��� ������ ����Ʈ�� 1�� ��ڴ�.
        currentCombo += p_num;

        //�׷��� �ؼ� ������ �޺� ���� �ؽ�Ʈ�� ǥ�� 3�ڸ����� ,(�޸�)�� ����ְڴ�.
        txtCombo.text = string.Format("{0:,##0}", currentCombo);

        //�޺� �ؽ�Ʈ, �޺� �̹����� 3�޺� �̻���� �����ϵ��� ���ǹ� �ۼ�.
        if (currentCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);
            goComboImage.SetActive(true);

            myAnim.SetTrigger(animComboUp);
        }

    }

    //���� �޺��� �� �� �ְ�.
    public int GetCurrentCombo()
    {
        return currentCombo;
    }


    //�޺� Reset
    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }
}
