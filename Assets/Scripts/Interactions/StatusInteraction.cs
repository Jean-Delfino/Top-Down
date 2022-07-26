using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StatusInteraction : Interaction{
    [Space]
    [Header("Status interaction values")]
    [Space]

    [SerializeField] List<StatusChange> onInteractionChange = default;

    private new void OnTriggerEnter2D(Collider2D other){
        if(base.OnTriggerEnter2D(other)){
            save = StartCoroutine(StartInteraction(
                    other.gameObject.GetComponent<PlayerController>()));
        }
    }

    protected void SendStatusChange(GameObject other){
        PlayerStatus pC = other.GetComponent<PlayerStatus>();

        foreach(StatusChange sC in onInteractionChange){
            pC.ChangeStatus(sC);
        }
    }

    protected new IEnumerator StartInteraction(PlayerController pC){
        pC.ShowInteraction();

        exitAction = pC.UnShowInteraction;

        while(true){
            while(Input.GetAxis("Interaction") == 0){
                yield return null;
            }

            pC.ChangeStateWait(true);
            GetOnInteractionEvents().Invoke();
            SendStatusChange(pC.gameObject);

            yield return new WaitForSeconds(GetTotalTimeAction());

            pC.ChangeStateWait(false);
        }
    }
}
