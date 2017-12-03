using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DamageItemAttribute : ItemAttribute {
    public int minDamage;
    public int maxDamage;

    public override string GetStringRepresentation()
    {
        return "Damage: " + minDamage.ToString() + " - " + maxDamage.ToString() + "\n";
    }

    public override void Initialize(int level, int quality)
    {
        level += quality;
        minDamage = 3 * level;
        maxDamage = 6 * level;

        minDamage += Random.Range(-level, level);
        maxDamage += Random.Range(-level, level);
    }
}
