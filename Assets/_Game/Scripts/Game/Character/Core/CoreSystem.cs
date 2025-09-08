using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Core
{
    using Sensor;
    using Navigation;
    using Movement;
    using Display;

    public abstract class CoreSystem : MonoBehaviour
    {
        public CharacterStats Stats { get; private set; }
        [SerializeField]
        Character _character;
        public Character CHARACTER => _character;
        [SerializeField]
        MovementCore _movement;
        public MovementCore MOVEMENT => _movement;
        [SerializeField]
        DisplayCore _display;
        public DisplayCore DISPLAY => _display;
        [SerializeField]
        NavigationCore _navigation;
        public NavigationCore NAVIGATION => _navigation;
        [SerializeField]
        SensorCore _sensor;
        public SensorCore SENSOR => _sensor;

        List<BaseCore> cores = new();

        public StateMachine stateMachine;

        void Awake()
        {
            SENSOR.ReceiveInfo(NAVIGATION);
        }

        public virtual void Initialize(CharacterStats stats)
        {
            Stats = stats;

            MOVEMENT.Initialize(this);
            DISPLAY.Initialize(this);
            NAVIGATION.Initialize(this);
            SENSOR.Initialize(this);
        }

        public virtual void UpdateData()
        {
            foreach (var comp in cores)
            {
                comp.UpdateData();
            }
        }

        public virtual void FixedUpdate()
        {
            foreach (var comp in cores)
            {
                comp.FixedUpdateData();
            }
        }

        public void AddCoreComp(BaseCore comp)
        {
            if (!cores.Contains(comp))
                cores.Add(comp);
        }

        public void OnDeath()
        {
            stateMachine.ChangeState(STATE.DEAD);
        }
    }
}
