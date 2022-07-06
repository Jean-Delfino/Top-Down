using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour{
    [SerializeField] UnityEvent onInteractionEvents = default;
    [SerializeField] float totalTimeAction = default;

    private Coroutine save = null;
    Action exitAction = null;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            save = StartCoroutine(StartInteraction(
                    other.gameObject.GetComponent<PlayerController>()));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(save != null){
            StopInteraction();
        }
    }

    private IEnumerator StartInteraction(PlayerController pC){
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

    private void StopInteraction(){
        StopCoroutine(save);

        if (exitAction != null) {
            exitAction();
        }

        exitAction = null;
    }




}
