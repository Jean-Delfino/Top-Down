using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUserInterface.Animation;

public class DoorInteraction : StatusInteraction{
    [Space]
    [Header("Door interaction")]
    [Space]

    [SerializeField] Vector3 newPosition = default;

    [Space]
    [Header("Transition interaction")]
    [Space]

    [SerializeField] TransitionController tC = default;
    [SerializeField] TransitionType tt = default;


    private new void OnTriggerEnter2D(Collider2D other){
        OnTriggerEnter2D(other, 
            StartInteraction(other.gameObject.GetComponent<PlayerController>()));
    }

    private new void OnTriggerExit2D(Collider2D other){
        if(save != null){
            StopInteraction();
            notUsing = true;
        }
        StartCoroutine(
            FinishInteraction(other.gameObject.GetComponent<PlayerController>()));
    }

    protected IEnumerator FinishInteraction(PlayerController pC){
        yield return new WaitForSeconds(tC.PlayTransitionOut(tt));
        pC.ChangeStateWait(false);
    }

    protected new IEnumerator StartInteraction(PlayerController pC){
        pC.ShowInteraction();

        exitAction = pC.UnShowInteraction;

        while(true){
            while(Input.GetAxis("Interaction") == 0 && notUsing){
                yield return null;
            }

            GetOnInteractionEvents().Invoke();

            pC.UnShowInteraction();
            pC.ChangeStateWait(true);

            notUsing = false;
            
            yield return new WaitForSeconds(tC.PlayTransitionIn(tt));
            yield return new WaitForSeconds(GetTotalTimeAction());

            SendStatusChange(pC.gameObject);
            pC.ChangePosition(newPosition);
        }
    }
}
