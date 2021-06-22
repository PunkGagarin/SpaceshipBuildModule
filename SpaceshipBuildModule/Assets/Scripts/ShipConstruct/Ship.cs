    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Ship : MonoBehaviour{
        public List<Module> modules { get; private set; }
        public List<ShipNode> nodes;// { get; private set; }

        private void Awake() {
            modules ??= new List<Module>();
            nodes ??= new List<ShipNode>();
        }
    }