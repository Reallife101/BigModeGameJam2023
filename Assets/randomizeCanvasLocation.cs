using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class randomizeCanvasLocation : MonoBehaviour
{
    RectTransform rt;
    [SerializeField]
    int maxX;
    [SerializeField]
    int maxY;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.position = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0);
    }

}
