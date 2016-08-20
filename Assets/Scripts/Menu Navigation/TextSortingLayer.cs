using UnityEngine;
using System.Collections;

public class TextSortingLayer : MonoBehaviour {

    public string layer = "Text";

    void Start() {
        GetComponent<MeshRenderer>().sortingLayerName = layer;
    }
}
