using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedItemAttribute : ItemAttribute {
    public float attackSpeed;

    public override string GetStringRepresentation()
    {
        return "Attack Speed: " + attackSpeed + "\n";
    }


    public override void Initialize(int level, int quality)
    {
        throw new System.NotImplementedException();
    }
}
