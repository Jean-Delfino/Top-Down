using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ItemInteraction : Interaction{

    [Serializable]
    public class ItemWithEvent{
        public UnityEvent<PlayerInventory, Item, int> itemActions;
        public List<ItemWithQtd> itemWithQtd;
    }

    [Serializable]
    public class ItemWithQtd{
        public Item item;
        public int qtd;
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
                for(j = 0; j < eventItens[i].itemWithQtd.Count && continueItemActions; j++){
                    eventItens[i].itemActions.Invoke(
                        pI, eventItens[i].itemWithQtd[j].item,
                        eventItens[i].itemWithQtd[j].qtd);
                }
            }

            yield return new WaitForSeconds(GetTotalTimeAction());

            pC.ChangeStateWait(false);
            notUsing = true;
        }
    }

    public void SearchItem(PlayerInventory playerInventory, Item item, int qtd){
        continueItemActions = playerInventory.GetInventoryBag().SearchItem(item);
    }

    public void AddItem(PlayerInventory playerInventory, Item item, int qtd){
        playerInventory.AddItem(item, qtd);
    }
}
