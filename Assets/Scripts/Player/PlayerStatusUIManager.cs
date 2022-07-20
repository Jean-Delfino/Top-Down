using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUIManager : MonoBehaviour{
    [Space]
    [Header("Health variables")]
    [Space]

    [SerializeField] Slider healthSlider = default;
    [SerializeField] Image healthFillImage = default;

    [SerializeField] Gradient healthGradient = default;

    [Space]
    [Header("Status variables")]
    [Space]

    [SerializeField] Transform spawn = default;
    [SerializeField] StatusUI prefabsStatus = default;

    [SerializeField] Gradient statusGradient = default;

    private List<StatusUI> statusInScene = new List<StatusUI>();

    public void SpawnStatus(List<Status> toSpawn){
        StatusUI hold;

        foreach(Status stat in toSpawn){
            hold = Instantiate<StatusUI>(prefabsStatus, spawn);

            hold.Setup(stat);

            statusInScene.Add(hold);
        }
    }

    public void ChangeSliderHealth(int value){
        healthSlider.value = value;

        healthFillImage.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void ChangeStatus(Status stat, int index){
        statusInScene[index].ChangeStatusValue(stat, statusGradient);
    }
}
