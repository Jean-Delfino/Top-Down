
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilItem{
    public static bool SearchItem(Bag bag, string id){
        Queue<Bag> additionalBags = new Queue<Bag>();
        Bag hold;

        additionalBags.Enqueue(bag);

        while(additionalBags.Count != 0){
            hold = additionalBags.Dequeue();

            foreach(Item it in hold.GetContent()){
                if(it.GetUniqueID() == id){
                    return true;
                }
            }

            foreach(Bag bg in hold.GetContent()){
                additionalBags.Enqueue(bg);
            }
        }

        return false;
    }

    public static bool SearchItem(Bag bag, Item it){
        Queue<Bag> additionalBags = new Queue<Bag>();
        Bag hold;

        additionalBags.Enqueue(bag);

        while(additionalBags.Count != 0){
            hold = additionalBags.Dequeue();

            if(hold.GetContent().Contains(it)){
                return true;
            }

            foreach(Bag bg in hold.GetContent().OfType<Bag>().ToList()){
                additionalBags.Enqueue((Bag)bg);
            }
        }

        return false;
    }
}
