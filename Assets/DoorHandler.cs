using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_LeftDoor;
    [SerializeField]
    private GameObject m_RightDoor;

    //開閉距離
    [SerializeField,Range(1f,10f)]
    private float m_MaxLength = 1f;

    //開閉時間
    [SerializeField]
    private float m_RunTime = 3f;

    //閉じている/開いている時の待機時間
    [SerializeField]
    private float m_WaitTime = 3f;

    //true:ドアが開いてる false:ドアが閉じている
    [SerializeField]
    private bool m_IsOpen = true;

    private float m_Time = 0f;
    private bool m_IsWait = true;
    private Vector3 m_LeftPos;
    private Vector3 m_RightPos;

    // Start is called before the first frame update
    void Start()
    {
        m_RightPos = m_RightDoor.transform.position;
        m_LeftPos = m_LeftDoor.transform.position;

        if (m_IsOpen)
        {
            m_RightDoor.transform.position = m_RightPos + this.transform.right * m_MaxLength;
            m_LeftDoor.transform.position = m_LeftPos - this.transform.right * m_MaxLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsWait)
        {
            WaitState();
        }
        else
        {
            if (m_IsOpen)
                Close();
            else
                Open();
        }

        m_Time += Time.deltaTime;
    }

    private void WaitState()
    {
        if (m_Time < m_WaitTime) return;

        m_Time = 0f;
        m_IsWait = false;
        m_IsOpen = !m_IsOpen;
    }

    public void Open()
    {
        //既に開いている
        if (m_IsOpen == true) return;

        Moving(0,m_MaxLength);
    }

    public void Close()
    {
        //既に閉まっている
        if (m_IsOpen == false) return;

        Moving(m_MaxLength,0);
    }

    //start から end に遷移する
    private void Moving(float start,float end)
    {
        float rate = m_Time / m_RunTime;
        float length = Mathf.Lerp(start,end,rate);

        m_RightDoor.transform.position = m_RightPos + this.transform.right * length;
        m_LeftDoor.transform.position = m_LeftPos - this.transform.right * length;

        if (rate < 1f) return;
        
        m_Time = 0f;
        m_IsWait = true;
    }
}
