using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemAttribute : ItemAttribute {
    public int healthBonus;

    public override string GetStringRepresentation()
    {
        return "Health Bonus: " + healthBonus + "\n";
    }


    public override void Initialize(int level, int quality)
    {
        healthBonus = 2 * level;
        healthBonus += Random.Range(-level, level);
    }
}
