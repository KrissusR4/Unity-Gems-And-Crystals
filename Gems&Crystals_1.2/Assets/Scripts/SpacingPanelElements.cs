using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpacingPanelElements : MonoBehaviour
{
    public RectTransform MyHandPanel;

    public void Update()
    {
        HorizontalLayoutGroup HLG = MyHandPanel.GetComponent<HorizontalLayoutGroup>();
        int childNo = MyHandPanel.transform.childCount;
        if(childNo > 4)
        {
            HLG.spacing = ((float)childNo - 4) * (-10);
        }
        else
        {
            HLG.spacing = (float)30;
        }
    }
}
