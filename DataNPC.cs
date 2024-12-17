using UnityEngine;

namespace Patrick


{
 [CreateAssetMenu(menuName = "Patrick/NPC")]

 public class DataNPC : ScriptableObject 
{
    [Header("NPC AI needs to analyze")]
    public string [] sentences;
 }

}
