using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //追随目标设置和刷新位置事件介个设置
    GameObject target = null;
    float pathTime = 0f;

    //槽位数初始化
    int slot = -1;

    // Use this for initialization
    void Start()
    {
        //获取追随目标
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //每0.5秒刷新进入If分支
        pathTime += Time.deltaTime;
        if (pathTime > 0.5f)
        {
            pathTime = 0f;

            //获得 SlotManager 脚本，为空怎不进入下面If分支
            SlotManager slotManager = target.GetComponent<SlotManager>();
            if (slotManager != null)
            {
                //存储获取槽位数
                if (slot == -1)
                    slot = slotManager.Reserve(gameObject);

                //如果没有槽了即返回
                if (slot == -1)
                    return;

                //获取 NvaMeshAgent，没有则返回，否则设置目标位置移动
                var agent = GetComponent<NavMeshAgent>();
                if (agent == null)
                    return;
                agent.destination = slotManager.GetSlotPosition(slot);
            }
        }
    }
}