using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UniRx;
using UnityEngine;

namespace GameScene.Shelter
{
    public class ShelterProvider : MonoBehaviour
    {
        private List<Shelter> _shelters;
        public ReactiveCommand<List<Shelter>> SheltersGathered { get; private set; }
        public BoolReactiveProperty FreeShelterExist { get; private set; }

        private void Awake()
        {
            _shelters = new List<Shelter>();
            SheltersGathered = new ReactiveCommand<List<Shelter>>();
            FreeShelterExist = new BoolReactiveProperty();
        }

        public void GatherAllShelterToScene()
        {
            _shelters = new List<Shelter>((
                from item in FindObjectsOfType<ShelterView>()
                select item.Model).ToList());
            
            FreeShelterExist.Value = true;
            SheltersGathered.Execute(_shelters);
        }

        public bool TryGetFreeShelter(out Shelter shelter)
        {
            shelter = null;
            
            var freeShelters = (
                from item in _shelters
                where item.Contained.Value == false
                select item).ToList();

            if (freeShelters.IsEmpty())
                return false;
            shelter = freeShelters[0];
            return true;
        }
    }
}