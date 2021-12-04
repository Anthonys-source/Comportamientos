using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventID
{
    public static readonly ID BAKER_GOES_TO_SLEEP = new ID("baker_goes_to_sleep");

    public static readonly ID ADD_ITEM_TO_INVENTORY = new ID("add_item_to_inventory");
    public static readonly ID REMOVE_ITEM_FROM_INVENTORY = new ID("remove_item_from_inventory");
}
