using UnityEngine;

namespace Patrick
{

    public class NPCController : MonoBehaviour
    {
        [SerializeField, Header("NPC information")]
        private DataNPC dataNPC;

        [SerializeField, Header("動畫參數")]

        private string[] paramaters =
        {
            "Idle_Battle",
        };


        private Animator ani; //Animators (Controller)

        public DataNPC data => dataNPC;

        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

        public void PlayAnimation(int index)
        {
            ani.SetTrigger(paramaters[index]);
        }

    }
}







