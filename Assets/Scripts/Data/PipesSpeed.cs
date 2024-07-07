using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PipesSpeed", menuName = "ScriptableObjects/PipesSpeed")]
public class PipesSpeed : ScriptableObject
{
    [SerializeField] public string ID = "";
        [TextArea]
        [SerializeField] public string Name = "";

        [SerializeField] public float Speed;

        private void OnValidate() {
            if (ID == "")
            {
                ID = Guid.NewGuid().ToString();
            }
        }
}
