using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoordInfo : MonoBehaviour
{
    public GameObject Box;
    public GameObject DescriptionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoords();
    }

    private void UpdateCoords()
    {
        var worldpos = Box.transform.position;
        var localpos = Box.transform.localPosition;
        var screenPos = Camera.main.WorldToScreenPoint(Box.transform.position);


        var text = $"world pos: {worldpos.x}, {worldpos.y}, {worldpos.z}";
        text += $"\nlocal pos: {localpos.x}, {localpos.y}, {localpos.z}";
        text += $"\nscreen pos: {screenPos.x}, {screenPos.y}, {screenPos.z}";

        DescriptionText.GetComponent<TextMeshPro>().text = text;

    }
}
