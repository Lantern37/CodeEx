using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Carousel
{
    public class CirclePlacer
    {
        public float AngleStep { get; private set; }
        
        private CirclePlacerConfig _config;

        private float _offsetAngle;
        private float _offsetAngleToZ = 90; //For create circle around Z axe, not X

        private int _leftIndex;
        public int LeftIndex => _leftIndex;

        private int _rightIndex;
        public int RightIndex => _rightIndex;
        
        public void Initialize(CirclePlacerConfig config)
        {
            _config = config;
        }

        public void PlaceItems(List<Transform> items, Transform center, float offsetAngle = 0)
        {
            int num = items.Count;
            AngleStep = (_config.ArcAngle) / (num - 1);
            _offsetAngle = -offsetAngle;

            _leftIndex = (num - 1) / 2;
            _rightIndex = -(num - 1) / 2;

            for (int i = 0; i < num; i++)
            {
                int index = i - (num - 1) / 2;

                var radians = Mathf.Deg2Rad * (_config.ArcAngle) / (num - 1) * index;

                var radOffset = Mathf.Deg2Rad * (_offsetAngleToZ + _offsetAngle);

                var vertcial = Mathf.Sin(radians + radOffset);
                var horizontal = Mathf.Cos(radians + radOffset);

                var spawnDir = new Vector3(horizontal, 0, vertcial);

                var spawnPos = center.position + spawnDir * _config.Radius;

                var item = items[i];

                //item.transform.SetParent(parent);
                item.transform.position = spawnPos;
                item.transform.LookAt(center);
            }
        }

        public void AddItemRight(Transform item,Transform point)
        {
            AddItemAtIndex(item, point, _rightIndex);
        }

        public void AddItemLeft(Transform item, Transform point)
        {
            AddItemAtIndex(item, point, _leftIndex);
        }

        public void AddItemAtIndex(Transform item, Transform point, int index)
        {
            var radians = Mathf.Deg2Rad * AngleStep * index;

            var radOffset = Mathf.Deg2Rad * (_offsetAngleToZ + _offsetAngle);

            var vertcial = (float)Math.Round(Mathf.Sin(radians + radOffset), 3);
            var horizontal = (float)Math.Round(Mathf.Cos(radians + radOffset), 3);

            var spawnDir = new Vector3(horizontal, 0, vertcial);

            var spawnPos = point.position + spawnDir * _config.Radius;
            
            item.transform.position = spawnPos;

            item.transform.LookAt(point);
        }
    }
    
    [Serializable]
    public class CirclePlacerConfig
    {
        public float ArcAngle;
        public float Radius;
    }
}