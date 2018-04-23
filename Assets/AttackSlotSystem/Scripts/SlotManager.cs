using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    //储存Enemy的槽列表，槽的个数以及Enemy与Player的距离
    private List<GameObject> slots;
    public int count = 6;
    public float distance = 0.8f;

    void Start()
    {
        ///初始化槽列表，存储null
        slots = new List<GameObject>();
        for (int index = 0; index < count; ++index)
        {
            slots.Add(null);
        }
    }

    /// <summary>
    /// 获取对应索引操的位置
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
	public Vector3 GetSlotPosition(int index)
    {
        //每个操的角度间距，计算出槽相对Player的具体位置，并返回
        float degreesPerIndex = 360f / count;
        Vector3 pos = transform.position;
        Vector3 offset = new Vector3(0f, 0f, distance);
        return pos + (Quaternion.Euler(new Vector3(0f, degreesPerIndex * index, 0f)) * offset);
    }

    /// <summary>
    /// 储存Enemy，并返回当前的槽位数
    /// </summary>
    /// <param name="attacker"></param>
    /// <returns></returns>
    public int Reserve(GameObject attacker)
    {
        //计算当前Enemy和Player的normalized * distance位置
        Vector3 bestPosition = transform.position;
        Vector3 offset = (attacker.transform.position - bestPosition).normalized * distance;
        bestPosition += offset;

        //初始化bestSlot和bestDist
        int bestSlot = -1;
        float bestDist = 99999f;

        //遍历槽，把Enemy存到 对应空槽上
        for (int index = 0; index < slots.Count; ++index)
        {
            if (slots[index] != null)
                continue;
            float dist = (GetSlotPosition(index) - bestPosition).sqrMagnitude;
            if (dist < bestDist)
            {
                bestSlot = index;
                bestDist = dist;
            }
        }

        //把Enemy存到 对应空槽上，并返回当前槽位数
        if (bestSlot != -1)
            slots[bestSlot] = attacker;
        return bestSlot;
    }

    /// <summary>
    /// 释放槽上的Enemy
    /// </summary>
    /// <param name="slot"></param>
    public void Release(int slot)
    {
        slots[slot] = null;
    }

    //在场景中画出Player周围的对应槽
    void OnDrawGizmosSelected()
    {
        //遍历所有槽，根据是否为空槽，画出对应半径0.3f的WireSphere
        for (int index = 0; index < count; ++index)
        {
            if (slots == null || slots.Count <= index || slots[index] == null)
                Gizmos.color = Color.white;
            else
                Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetSlotPosition(index), 0.3f);
        }
    }
}