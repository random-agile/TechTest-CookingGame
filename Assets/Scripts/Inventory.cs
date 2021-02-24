using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject socketPrefab;

    private List<GameObject> objectList = new List<GameObject>();
    private List<GameObject> sockets = new List<GameObject>();
    private List<Vector3> socketsPos = new List<Vector3>();


    private void Start()
    {
        GetAllPointsOnCircle(transform.position, 12, 0.07f);
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
    /// <param name="center">The center of the inventory</param>
    /// <param name="maxSockets">The number of Socket you want</param>
    /// <param name="radius">How far away from the center are the sockets ?</param>
    private void GetAllPointsOnCircle(Vector3 center, int maxSockets, float radius)
    {
        float step = 2 * Mathf.PI / maxSockets;
        for (int i = 0; i < maxSockets; i++)
        {
            float x = Mathf.Cos(i * step);
            float y = Mathf.Sin(i * step);
            Vector3 pos = center + new Vector3(x, y, 0) * radius;
            GameObject socket = Instantiate(socketPrefab, transform.position, Quaternion.identity, transform);
            socket.SetActive(false);
            sockets.Add(socket);
            socketsPos.Add(pos);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Hand"))
            return;

        for (int i = 0; i < sockets.Count; i++)
        {
            GameObject s = sockets[i];
            s.transform.DOScale(0, 0.5f);
            s.transform.DOLocalMove(transform.position, 0.5f).OnComplete(() =>
            {
                s.SetActive(false);
            });
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hand"))
            return;

        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].SetActive(true);
            sockets[i].transform.DOScale(1.0f, 0.5f);
            sockets[i].transform.DOLocalMove(socketsPos[i], 0.5f);
        }
    }
}
