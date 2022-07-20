using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class StatusUI : MonoBehaviour{
    [SerializeField] Slider valueStatus = default;
    [SerializeField] Image fillStatusImage = default;

    [SerializeField] Image prefabImage = default;
    [SerializeField] TextMeshProUGUI statusName = default;

    public void Setup(Status stat){
        prefabImage.sprite = stat.GetIcon();
        statusName.text = stat.GetStatusName();
        
        ChangeStatusValue(stat);
    }

    public void ChangeStatusValue(Status stat, Gradient fillColor){
        ChangeStatusValue(stat);

        fillStatusImage.color = fillColor.Evaluate(valueStatus.normalizedValue);
    }

    public void ChangeStatusValue(Status stat){
        valueStatus.value = stat.GetActualValueStatus();
    }
}
