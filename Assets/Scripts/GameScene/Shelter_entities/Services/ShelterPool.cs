using System;
using System.Collections.Generic;
using System.Linq;
using GameScene.Shelter_entities.Model;
using ModestTree;
using UniRx;

namespace GameScene.Shelter_entities.Services
{
    public class ShelterPool : IDisposable
    {
        private Shelter[] _shelters;
        private readonly CompositeDisposable _disposable = new();
        public ReactiveProperty<bool> FreeShelterExist { get; private set; } = new();

        public ShelterPool(ShelterProvider shelterProvider)
        {
            shelterProvider.SheltersGathered
                .Subscribe( pool =>
                {
                    _shelters = pool;
                    FreeShelterExist.Value = true;
                })
                .AddTo(_disposable);
        }

        public bool TryTakeFreeShelter(out Shelter shelter)
        {
            shelter = null;
            
            var freeShelters = (
                from item in _shelters
                where item.Contained.Value == false
                select item).ToArray();

            if (freeShelters.IsEmpty()) return false;
            
            UpdateFreeShelterExistProperty(freeShelters);
            shelter = freeShelters[0];
            shelter.SetContainedState(true);
            return true;
        }

        public void FreeUpShelter(Shelter shelter)
        {
            shelter.SetContainedState(true);
            FreeShelterExist.Value = true;
        }
        
        private void UpdateFreeShelterExistProperty(IReadOnlyCollection<Shelter> freeShelters)
        {
            FreeShelterExist.Value = freeShelters.Count != 1;
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}