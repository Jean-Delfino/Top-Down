using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour{
    [SerializeField] UnityEvent2 onInteractionEvents = default;
    [SerializeField] float totalTimeAction = default;

    protected Coroutine save = null;
    protected Action exitAction = null;

    protected bool notUsing = true;

    protected bool OnTriggerEnter2D(Collider2D other) {
        if(notUsing && other.gameObject.tag == "Player"){
            save = StartCoroutine(StartInteraction(
                    other.gameObject.GetComponent<PlayerController>()));
                
            notUsing = false;
            return true;
        }
        return false;
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if(save != null){
            StopInteraction();
            notUsing = true;
        }
    }

    protected IEnumerator StartInteraction(PlayerController pC){
        pC.ShowInteraction();

        exitAction = pC.UnShowInteraction;

        while(true){
            while(Input.GetAxis("Interaction") == 0){
                yield return null;
            }

            pC.ChangeStateWait(true);
            onInteractionEvents.Invoke();

            yield return new WaitForSeconds(totalTimeAction);

            pC.ChangeStateWait(false);
        }
    }

    protected void StopInteraction(){
        StopCoroutine(save);

        if (exitAction != null) {
            exitAction();
        }

        exitAction = null;
    }

    public UnityEvent2 GetOnInteractionEvents(){
        return this.onInteractionEvents;
    }

    public float GetTotalTimeAction(){
        return this.totalTimeAction;
    }
}
