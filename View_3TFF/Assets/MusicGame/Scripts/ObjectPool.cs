using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjcctInfo
{
    //����. goPrefab ������Ű��.
    public GameObject goPrefab;

    //goPrefab������ų ����.
    public int count;

    //goPrefab��� ��ġ�� ������ų�� ����.
    public Transform tfPoolParent;
}


public class ObjectPool : MonoBehaviour
{

    //ObjectPool�� ���� ��𿡼��� ������ ���� �ݳ��� �� �ְ� ��������.
    public static ObjectPool instnace;

    [SerializeField] ObjcctInfo[] objectInfo = null;

    //Queue : ���Լ��� �ڷ���(���� ���� �� �����Ͱ� ���� ���� ��������)
    public Queue<GameObject> noteQueue = new Queue<GameObject>();



    void Start()
    {
        instnace = this;

        //���Ͻ�Ų ���� InsertQueue �־��ֱ�. ��ȣ������ 0����.
        noteQueue = InsertQueue(objectInfo[0]);
    }


    // Queue ������ ����.
    Queue<GameObject> InsertQueue(ObjcctInfo p_objectInfo)
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();

        //ī��Ʈ
        for (int i = 0; i < p_objectInfo.count; i++)
        {
            //������ ��ġ�� ���� ������ ��Ȱ��ȭ�� ���� ���ӿ��� ������ �ʴ´�. �ʿ��� �� ��ġ���� �ִ°ɷ� ������ �����̴�.
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);

            //��ü ���� ���ױ� ������ �ٷ� ��Ȱ��ȭ.
            t_clone.SetActive(false);

            //�θ� ����.
            //������ �����ߴ� ��Ʈ���� ��쿡�� ��Ʈ�Ŵ��� ��ũ��Ʈ�� �پ��ִ� ��ü�� �θ�. �θ� ��ü�� �����Ѵٸ� �� ��ü�� �θ�� ����ֱ�.
            if (p_objectInfo.tfPoolParent != null)
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent);

            //�θ� ������ null���̶�� t_clone.transform.SetParent(this.transform); �θ��.
            else
                t_clone.transform.SetParent(this.transform);

            //�ݺ����� �� ���� ���� Queue ī��Ʈ ������ŭ ��ü�� ���ִ�.
            t_queue.Enqueue(t_clone);


        }

        return t_queue;
    }
}
