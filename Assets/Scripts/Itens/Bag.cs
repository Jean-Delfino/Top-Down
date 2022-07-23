using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    A bag can be stored inside of a bag

    Each item has a bag, but not each bag has a bag

    The bag player is not inside any bag
*/

public class Bag : Item{
    private List<Item> content = new List<Item>();

    [SerializeField] float weightLimit = default;

    public Item GetItem(int index){
        if(index < 0 || index > content.Count) return null;

        return content[index];
    }

    public void AddItem(Item item){
        float totalWeight = item.GetWeight() + this.GetWeight();
        if(totalWeight > weightLimit) return;

        item.SetFatherOfItem(this, content.Count);
        content.Add(item);

        this.SetWeight(totalWeight);
        this.SetValue(item.GetValue() + this.GetValue());

    }

    public void RemoveItemInBag(int index){
        if(index < 0 || index > content.Count) return;

        content.RemoveAt(index);

        this.SetWeight(this.GetWeight() - content[index].GetWeight());
        this.SetValue(this.GetValue() - content[index].GetValue());
    }

    public override void UseItem(PlayerStatus pS){}
    
    public override void RemoveItem(){} 
}
