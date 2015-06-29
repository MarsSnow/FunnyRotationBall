using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class RotationBalls : MonoBehaviour 
{
    public float m_speed = 1000.0f;
    public float m_offset = 7.0f;
    public float timeCount = 0f;
    public int m_angel = 30;
    public List<GameObject> m_elementsList = new List<GameObject>();

    private const string m_ballPrefabPath = "Chinese/Prefabs/BallPrefab";
    private const int m_aRoundDegree = 360;

    private void Start () 
    {
        InitBalls();
	}
	
	private void Update () 
    {
        timeCount += Time.deltaTime;
        if (timeCount >= 2f)
        {
            StartCoroutine("RotationCoutine");
            timeCount = 0f;
        }
	}

    private  IEnumerator RotationCoutine()
    {
        foreach (GameObject obj in m_elementsList)
        {
            yield return new WaitForSeconds(0.1f);

            BallNode ballNode = obj.GetComponent<BallNode>();
            float changeDegrees = ballNode.m_ballDegree;
            changeDegrees +=6 * m_angel + m_angel / 2;

            float xPosition = m_offset * Mathf.Cos((changeDegrees * Mathf.PI) / 180f);
            float zPosition = m_offset * Mathf.Sin((changeDegrees * Mathf.PI) / 180f);

            ballNode.m_ballDegree = changeDegrees;

            HOTween.To(obj.transform, 0.7f, "position", new Vector3(xPosition, 0, zPosition));
        }
    }
    private void InitBalls()
    {
        for (int i = 0; i < m_aRoundDegree; i += m_angel)
        {
            GameObject obj = CreateObj();
            SetBallPosition(i, obj);
            SetBallColor(i, obj);
            m_elementsList.Add(obj);
        }
    }

    private GameObject CreateObj()
    {
        GameObject ballObj = (GameObject)Resources.Load(m_ballPrefabPath) as GameObject;
        GameObject obj = GameObject.Instantiate(ballObj);
        return obj;
    }

    private void SetBallPosition(int i, GameObject obj)
    {
        float px = m_offset * Mathf.Cos((i * Mathf.PI) / 180f);
        float pz = m_offset * Mathf.Sin((i * Mathf.PI) / 180f);

        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(px, 0, pz);
        obj.name = "BallPrefab" + i.ToString();
        obj.GetComponent<BallNode>().m_ballDegree = i;
    }

    private void SetBallColor(int i, GameObject obj)
    {
        if ((i / m_angel) % 3 == 0)
        {
            obj.GetComponent<Renderer>().material.color = Color.red;
        }
        else if ((i / m_angel) % 3 == 1)
        {
            obj.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            obj.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
