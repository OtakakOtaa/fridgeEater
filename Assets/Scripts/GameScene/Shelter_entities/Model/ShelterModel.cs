using UnityEngine;

namespace GameScene.Shelter_entities.Model
{
    public class ShelterModel : Shelter_entities.Model.Shelter
    {
        public Transform Transform { get; private set; }
        
        public ShelterModel() : base()
        {
        }
    }
}