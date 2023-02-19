using System;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWork4
{
    public class CoinManager : MonoBehaviour
    {
        public event Action AllCoinsCollected = () => { };
        public event Action<int> CollectedCoinsCountChanged = (newCount) => { };
        public event Action<int> TotalCoinsCountChanged = (newTotalCount) => { };

        public int StartCoinsCount => _startCoinsCount;
        public int CoinsCollected => _coinsCollected;
        
        private int _coinsCollected = 0;
        private int _startCoinsCount = 0;
        
        [SerializeField] private List<Coin> coinsList = new();

        private void OnEnable()
        {
            Coin.CoinPicked += OnCoinPicked;
            Coin.CoinSpawned += OnCoinSpawned;
        }
        private void OnDisable()
        {
            Coin.CoinPicked -= OnCoinPicked;
        }

        private void OnCoinPicked(Coin coin)
        {
            _coinsCollected++;
            coinsList.Remove(coin);
            CheckCoinsLeft();
            CollectedCoinsCountChanged(_coinsCollected);
        }
        
        private void CheckCoinsLeft()
        {
            if (coinsList.Count == 0)
                AllCoinsCollected();
        }

        private void OnCoinSpawned(Coin coin)
        {
            if(!coinsList.Contains(coin)){
                coinsList.Add(coin);
                _startCoinsCount++;
                TotalCoinsCountChanged(_startCoinsCount);
            }
        }
        
        public Transform GetNearestCoin(Transform target)
        {
            if(coinsList.Count == 0)
                return null;
            
            Transform nearestCoin = null;
            float minSqrDistance = float.MaxValue;
            foreach (var coin in coinsList)
            {
                float sqrDistance = Vector3.SqrMagnitude(coin.transform.position - target.position);
                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    nearestCoin = coin.transform;
                }
            }
            return nearestCoin;
        }
    }
}
