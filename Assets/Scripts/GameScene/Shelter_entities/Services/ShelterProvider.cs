using System.Linq;
using GameScene.Shelter_entities.Model;
using UniRx;
using UnityEngine;

namespace GameScene.Shelter_entities.Services
{
    public class ShelterProvider : MonoBehaviour
    {
        public ReactiveCommand<Shelter[]> SheltersGathered { get; private set; } = new();
        
        public void GatherAllShelterToScene()
        {
            var shelters = (
                from item in FindObjectsOfType<ShelterView>()
                select item.Model).ToArray();
            SheltersGathered?.Execute(shelters);
        }
    }
}