using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public abstract class ItemAttribute : ScriptableObject {
    public abstract string GetStringRepresentation();
    public abstract void Initialize(int level, int quality);
}
