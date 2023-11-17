using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Scriptable Object", fileName = "SO")]
public class NpcSO : ScriptableObject
{
    public Sprite spriteRenderer;

    public float healthType1;
    public float healthType2;
    public float healthType3;

    public float damageType1;
    public float damageType2;
    public float damageType3;
    public float damageType4;
    public float damageType5;
    
    public float velocitySearch;
    public float velocityChase;
    public float velocityAttack;
    public float velocityAdditional1;
    public float velocityAdditional2;
    public float velocityAdditional3;
}