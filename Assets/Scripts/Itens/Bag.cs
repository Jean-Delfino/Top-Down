using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
    A bag can be stored inside of a bag

    Each item has a bag, but not each bag has a bag

    The bag player is not inside any bag
*/
[CreateAssetMenu(fileName = "Bag", menuName = "Items/Bag", order = 0)]

public class Bag : Item{
    private Dictionary<Item, int> content = new Dictionary<Item, int>();

    [SerializeField] float weightLimit = default;

    public List<Item> GetContent(){
        return content.Keys.ToList();
    }

    public bool SearchItem(Item item){
        return UtilItem.SearchItem(this, item);
    }

    public bool SearchItem(Item item, int qtd){
        return UtilItem.SearchItem(this, item);
    }

    public Dictionary<Item, int> GetContentDictionary(){
        return content;
    }

    public Item GetItem(int index){
        if(index < 0 || index > content.Count) return null;

        return content.ElementAt(index).Key;
    }

    public bool ItemExist(Item item){
        return content.ContainsKey(item);
    }

    public bool AddItem(Item item){
        return AddItem(item, 1);
    }

    public int GetItemQtd(Item item){
        if(content.ContainsKey(item)){
            return content[item];
        }

        return 0;
    }

    public bool AddItem(Item item, int qtd){
        float totalWeight = (item.GetWeight() * qtd) + this.GetWeight();
        if(totalWeight > weightLimit) return false;

        item.SetFatherOfItem(this);

        if(content.ContainsKey(item)){
            content[item] += qtd; 
        }else{
            content.Add(item, qtd);
        }

        this.SetWeight(totalWeight);
        this.SetValue( (item.GetValue() * qtd) + this.GetValue());
        return true;
    }

    public bool RemoveItemInBag(int index){
        if(index < 0 || index > content.Count) return false;

        content.Remove(content.ElementAt(index).Key);
        int qtd = content.ElementAt(index).Value;

        RemoveValueAndWeight(index, qtd);
        return true;
    }

    public bool RemoveItemInBag(Item item){
        if(!content.ContainsKey(item)) return false;

        content.Remove(item);
        int qtd = content[item];

        RemoveValueAndWeight(item, qtd);
        return true;
    }

    public bool TakesItemInBag(int index, int qtd){
        if(index < 0 || index > content.Count) return false;

        int qtdValue = content.ElementAt(index).Value;
        if(qtdValue < qtd) return false;

        qtdValue = content[content.ElementAt(index).Key] = qtdValue - qtd; 

        if(qtdValue == 0){
            content.Remove(content.ElementAt(index).Key);
        }

        RemoveValueAndWeight(index, qtd);
        return true;
    }

    public bool TakesItemInBag(Item item, int qtd){
        if(!content.ContainsKey(item)) return false;

        int qtdValue = content[item];
        if(qtdValue < qtd) return false;

        qtdValue = content[item] = qtdValue - qtd; 

        if(qtdValue == 0){
            content.Remove(item);
        }

        RemoveValueAndWeight(item, qtd);
        return true;
    }
    
    private void RemoveValueAndWeight(int index, int qtd){
        RemoveValueAndWeight(content.ElementAt(index).Key, qtd);
    }

    private void RemoveValueAndWeight(Item item, int qtd){
        this.SetWeight(this.GetWeight() - (item.GetWeight() * qtd));
        this.SetValue(this.GetValue() - (item.GetValue() * qtd));
    }

    public override void UseItem(PlayerStatus pS){}
    
    public override void RemoveItem(PlayerStatus pS){} 
}
