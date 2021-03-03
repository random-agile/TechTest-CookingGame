using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject socketPrefab;

    [Header("Settings")]
    [SerializeField] private int maxSockets = 12; 
    [SerializeField] private float radius = 0.7f; // radius of the circle on which sockets will be placed
    [SerializeField] private float animDuration = 0.5f; // duration of the open / close animation

    private float maxObjectScale = 1.0f; // Scale max of object stored
    public float MaxObjectScale => maxObjectScale;

	private List<GameObject> objectList = new List<GameObject>();
    private List<WristSocket> sockets = new List<WristSocket>();
    private List<Vector3> socketsPos = new List<Vector3>();

    private enum AskState
    {
		None,
		Open, 
		Close,
    }

    private AskState askState;
	private bool isInAnimation;

	private void Awake()
	{
		//maxObjectScale = (2 * Mathf.PI * radius * maxSockets);
		GenerateSocketObjects();
	}

	public void AddObject(GameObject _newObject)
    {
        objectList.Add(_newObject);
    }

    public void RemoveObject(GameObject _removedObject)
    {
        objectList.Remove(_removedObject);
	}

    private Vector3 GetWristPos(int index)
    {
	    float step = 2 * Mathf.PI / maxSockets;
		float x = Mathf.Cos(index * step);
	    float y = Mathf.Sin(index * step);
	    return new Vector3(x, y, 0) * radius;
	}

	/// <summary>
	/// Generate all wristSockets of inventory in circle around the hanc
	/// </summary>
    private void GenerateSocketObjects()
    {
	    for (int i = 0; i < maxSockets; i++)
	    {
		    GameObject socketObject = Instantiate(socketPrefab, transform.position, Quaternion.identity, transform);
		    var socket = socketObject.GetComponent<WristSocket>();
		    socket.parentInventory = this;
		    socketObject.SetActive(false);
		    sockets.Add(socket);
		    socketsPos.Add(GetWristPos(i));
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

	private void DisplaySocket(int index, int posIndex)
	{
		GameObject s = sockets[index].gameObject;
		s.SetActive(true);
		s.transform.DOScale(1.0f, animDuration).OnComplete(() =>
		{
			isInAnimation = false;
		});
		s.transform.DOLocalMove(GetWristPos(posIndex), animDuration);
	}

    [ContextMenu("Open Inventory")]
    private void OpenInventory()
    {
	    Debug.Log("Open inventory");
	    isInAnimation = true;
	    int displayIndex = 0;
		for (int i = 0; i < objectList.Count; i++)
        {
			// display only sockets that contain an object
	        if (!sockets[i].ContainObject) 
		        continue;
	        DisplaySocket(i, displayIndex++);
        }
		// add empty socket if enough space
		if (objectList.Count < maxSockets)
			DisplaySocket(objectList.Count, displayIndex);
    }

    [ContextMenu("Close Inventory")]
    private void CloseInventory()
    {
	    isInAnimation = true;
		Debug.Log("Close inventory");
		foreach (var s in sockets)
		{
			s.transform.DOScale(0, animDuration);
			var s1 = s.gameObject;
			s.transform.DOLocalMove(Vector3.zero, animDuration).OnComplete(() =>
			{
				s1.SetActive(false);
				isInAnimation = false;
			});
		}
	}

	// when the user hand trigger the inventory collider, ask for open it
	// we do not want break current animation by calling directly OpenInventory or CloseInventory
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
