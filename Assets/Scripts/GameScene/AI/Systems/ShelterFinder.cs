using GameScene.Shelter_entities;
using GameScene.Shelter_entities.Services;
using UnityEngine;

namespace GameScene.AI.Systems
{
    public class ShelterFinder
    {
        private ShelterProvider _shelterProvider;

        public ShelterFinder(ShelterProvider shelterProvider)
        {
            _shelterProvider = shelterProvider;
        }

        public Transform Find()
        {
            return null;
        }
    }
}