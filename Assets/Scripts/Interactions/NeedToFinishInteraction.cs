using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class NeedToFinishInteraction : Interaction{
    [SerializeField] UnityEvent2 onFinishEvent = default;
 
    protected override void OnTriggerEnter2D(Collider2D other){
        OnTriggerEnter2D(other, 
            StartInteraction(other.gameObject.GetComponent<PlayerController>()));
    }

    protected override IEnumerator StartInteraction(PlayerController pC){
        pC.ShowInteraction();

        exitAction = pC.UnShowInteraction;

        while(true){
            while(Input.GetAxis("Interaction") == 0 && notUsing){
                yield return null;
            }

            pC.UnShowInteraction();
            notUsing = false;
            pC.ChangeStateWait(true);
            GetOnInteractionEvents().Invoke();

            while(notUsing){
                yield return null;
            }
            
            onFinishEvent.Invoke();
            yield return new WaitForSeconds(GetTotalTimeAction());
            pC.ChangeStateWait(false);
            notUsing = true;
        }
    }
}
