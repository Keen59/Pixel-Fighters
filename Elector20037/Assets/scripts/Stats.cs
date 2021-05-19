using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : MonoBehaviour
{
   [Serializable]
       public struct AIStats
        {
        public float health;
         public float xp;

        public float armor;
        public float damage;
        public float stamina;
        public float speed;
        public float AttackSpeed;

        public float EyesRange;
        public float WeaponRange;
        }

        
        [Serializable]
       public struct player
        {
        public float health;
        public float healthBeklemesur;
        public float armor;
         public float skillPoint;
         public float IntelegentPow;
        public float StrenghtPow;
         public float Lucky;

        public float xp;
              public int level;
        public float xpLimit;
        
        public float manaBeklemeSuresi;
        public float damage;

       public float mana;
       public float stamina;

        public float Range;
       
        }


       

       void Start() {
        
        }
       
        public player playerstats;
        public AIStats aistats;
         private void Update() {
        
        }
}
