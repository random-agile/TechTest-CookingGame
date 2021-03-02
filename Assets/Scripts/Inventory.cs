using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject socketPrefab;

    [Header("Settings")]
    [SerializeField] private int numberOfSockets = 12;
    [SerializeField] private float radius = 0.7f;

    private List<GameObject> objectList = new List<GameObject>();
    private List<GameObject> sockets = new List<GameObject>();
    private List<Vector3> socketsPos = new List<Vector3>();

    const float animDuration = 0.5f;

    private enum AskState
    {
		None,
		Open, 
		Close,
    }

    private AskState askState;
	private bool isInAnimation;

	private void Start()
    {
        GetAllPointsOnCircle(numberOfSockets, radius);
    }

    private void AddObject(GameObject _newObject)
    {
        objectList.Add(_newObject);
    }

    private void RemoveObject(GameObject _removedObject)
    {
        objectList.Add(_removedObject);
    }

    /// <summary>
    /// Will generate multiple Socket in circle around the wrist inventory
    /// </summary>
    /// <param name="maxSockets">The number of Socket you want</param>
    /// <param name="radius">How far away from the center are the sockets ?</param>
    private void GetAllPointsOnCircle(int maxSockets, float radius)
    {
        float step = 2 * Mathf.PI / maxSockets;
        for (int i = 0; i < maxSockets; i++)
        {
            float x = Mathf.Cos(i * step);
            float y = Mathf.Sin(i * step);
            Vector3 pos = new Vector3(x, y, 0) * radius;
            GameObject socket = Instantiate(socketPrefab, transform.position, Quaternion.identity, transform);
            socket.SetActive(false);
            sockets.Add(socket);
            socketsPos.Add(pos);
        }
    }

    private void Update()
    {
		if (isInAnimation || askState == AskState.None)
			return;

		if (askState == AskState.Open)
			OpenInventory();
		else
			CloseInventory();

	    askState = AskState.None;
    }

    [ContextMenu("Open Inventory")]
    private void OpenInventory()
    {
	    Debug.Log("Open inventory");
	    isInAnimation = true;
		for (int i = 0; i < sockets.Count; i++)
        {
	        GameObject s = sockets[i];
            s.SetActive(true);
            s.transform.DOScale(1.0f, animDuration);
            s.transform.DOLocalMove(socketsPos[i], animDuration).OnComplete(() =>
            {
	            isInAnimation = false;
            });
        }
    }

    [ContextMenu("Close Inventory")]
    private void CloseInventory()
    {
	    isInAnimation = true;
		Debug.Log("Close inventory");
		foreach (var s in sockets)
		{
			s.transform.DOScale(0, animDuration);
			var s1 = s;
			s.transform.DOLocalMove(Vector3.zero, animDuration).OnComplete(() =>
			{
				s1.SetActive(false);
				isInAnimation = false;
			});
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Hand"))
			askState = AskState.Open;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Hand"))
			askState = AskState.Close;
	}

}
