using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ItemInteraction : Interaction{

    [Serializable]
    public class ItemWithEvent{
        public UnityEvent<PlayerInventory, Item> itemActions;
        public List<Item> itens;
    }

    [SerializeField] List<ItemWithEvent> eventItens = default;

    private bool continueItemActions;

    protected override IEnumerator StartInteraction(PlayerController pC){
        pC.ShowInteraction();

        exitAction = pC.UnShowInteraction;

        while(true){
            while(Input.GetAxis("Interaction") == 0 && notUsing){
                yield return null;
            }

            int i, j;

            continueItemActions = true;
            notUsing = false;
            pC.ChangeStateWait(true);

            PlayerInventory pI = pC.gameObject.GetComponent<PlayerInventory>();

            for(i = 0; i < eventItens.Count && continueItemActions; i++){
                for(j = 0; j < eventItens[i].itens.Count && continueItemActions; j++){
                    eventItens[i].itemActions.Invoke(
                        pI, eventItens[i].itens[j]);
                }
            }

            yield return new WaitForSeconds(GetTotalTimeAction());

            pC.ChangeStateWait(false);
            notUsing = true;
        }
    }

    public void SearchItem(PlayerInventory playerInventory, Item item){
        continueItemActions = playerInventory.GetInventoryBag().SearchItem(item);
    }

    public void AddItem(PlayerInventory playerInventory, Item item){
        playerInventory.AddItem(item, 1);
    }
}
