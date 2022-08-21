using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInteraction : Interaction{
    [Space]
    [Header("Status interaction values")]
    [Space]

    [SerializeField] List<StatusChange> onInteractionChange = default;

    protected override void OnTriggerEnter2D(Collider2D other){
        OnTriggerEnter2D(other, 
            StartInteraction(other.gameObject.GetComponent<PlayerController>()));
    }

    protected void SendStatusChange(GameObject other){
        PlayerStatus pS = other.GetComponent<PlayerStatus>();

        foreach(StatusChange sC in onInteractionChange){
            pS.ChangeStatus(sC);
        }
    }

    protected override IEnumerator StartInteraction(PlayerController pC){
        pC.ShowInteraction();

        exitAction = pC.UnShowInteraction;

        while(true){
            while(Input.GetAxis("Interaction") == 0 && notUsing){
                yield return null;
            }

            notUsing = false;
            pC.ChangeStateWait(true);
            GetOnInteractionEvents().Invoke();
            SendStatusChange(pC.gameObject);

            yield return new WaitForSeconds(GetTotalTimeAction());

            pC.ChangeStateWait(false);
            notUsing = true;
        }
    }
}
