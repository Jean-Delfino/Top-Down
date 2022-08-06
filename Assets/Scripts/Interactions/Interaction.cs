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

    protected void OnTriggerEnter2D(Collider2D other) {
        print("ENTROU NORMAL");
        if(other.isTrigger && other.gameObject.tag == "Player"){
            StartCoroutineInteraction(StartInteraction(
                    other.gameObject.GetComponent<PlayerController>()));
        }
    }

    protected void OnTriggerEnter2D(Collider2D other, IEnumerator routine) {
        print("ENTROU AQUI ?");
        if(other.gameObject.tag == "Player"){
            StartCoroutineInteraction(routine);
        }
    }

    protected void StartCoroutineInteraction(IEnumerator routine){
        if(save == null){
            save = StartCoroutine(routine);
        }
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
            while(Input.GetAxis("Interaction") == 0 && notUsing){
                yield return null;
            }

            notUsing = false;
            pC.ChangeStateWait(true);
            onInteractionEvents.Invoke();

            yield return new WaitForSeconds(totalTimeAction);

            pC.ChangeStateWait(false);
            notUsing = true;
        }
    }

    protected void StopInteraction(){
        StopCoroutine(save);

        if (exitAction != null) {
            exitAction();
        }
        save = null;
        exitAction = null;
    }

    public UnityEvent2 GetOnInteractionEvents(){
        return this.onInteractionEvents;
    }

    public float GetTotalTimeAction(){
        return this.totalTimeAction;
    }

    public void ChangeNotUsing(bool state){
        notUsing = state;
    }
}
