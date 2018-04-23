using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //获取按下鼠标的位置
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;

            //从屏幕鼠标位置发射一条射线
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            //如果射线接触到物体，把位置设置为 NavMeshAgent的目标位置
            if (Physics.Raycast(ray, out hit))
            {
                GetComponent<NavMeshAgent>().destination = hit.point;
            }
        }
    }
}