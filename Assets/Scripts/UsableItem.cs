using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : Item, IUsable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UsableItem(string displayName, string description, int id) : base(displayName, description, id) {
    }

    public void select() {
        PlayerController.getInstance().setSelectedItem(this);
    }
}
