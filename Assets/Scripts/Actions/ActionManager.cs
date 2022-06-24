using System.Collections.Generic;
using Assets.Scripts.Actions;
using UnityEngine;

namespace Actions {
    /// <summary>
    /// AcrionManagaer managing Actions.
    /// ActionHandler implement either NextDayActionHandler or ClickActionHandler and have to be added to the matching list in the instatiationmethod 
    /// </summary>
    public class ActionManager {
        #region Singleton
        private static ActionManager _instance;
        public static ActionManager Instance {
            get
            {
                if(_instance == null) {
                    _instance = new ActionManager();
                }
                return _instance;
            }
        }
        #endregion

        private List<ClickActionHandler> _clickActionHandlers;
        private List<NextDayActionHandler> _nextDayActionHandlers;
        private bool _enabled;

        public bool Enabled {
            get => _enabled;
            set => _enabled = value;
        }
        
        private ActionManager() {
            _clickActionHandlers = new List<ClickActionHandler>();
            _nextDayActionHandlers = new List<NextDayActionHandler>();
            instantiateClickActionHandlers();
            instantiateNextDayActionHandlers();
            Enabled = true;
        }

        /// <summary>
        /// NextDayActionHandlers to be instatiated and added to the corresponding List
        /// </summary>
        private void instantiateNextDayActionHandlers() {
            _nextDayActionHandlers.Add(new FarmlandTileNextDayActionHandler());
        }

        
        /// <summary>
        /// ClickActionHandlers to be instatiated and added to the corresponding List
        /// </summary>
        private void instantiateClickActionHandlers() {
            _clickActionHandlers.Add(new GrassTileClickHoeActionHandler());
            _clickActionHandlers.Add(new GrassTileClickShovelActionHandler());
            _clickActionHandlers.Add(new GrassTileClickFenceActionHandler());
            
            _clickActionHandlers.Add(new FarmlandTileClickShovelActionHandler());
            _clickActionHandlers.Add(new FarmlandTileClickScytheActionHandler());
            _clickActionHandlers.Add(new FarmlandTileClickWateringCanActionHandler());
            _clickActionHandlers.Add(new FarmlandTileClickWheatSeedsActionHandler());
            
            _clickActionHandlers.Add(new WaterTileClickShovelActionHandler());
            _clickActionHandlers.Add(new WaterTileClickFishingRodActionHandler());
            
            _clickActionHandlers.Add(new CowAnimalClickActionHandler());
        }
        
        /// <summary>
        /// Used to Invoke ClickActions, all ClickActionHandlers in ClickActionHandlers list are iterated through,
        /// only one will be invoked per method call 
        /// </summary>
        /// <param name="gameObject">The affected gameObject</param>
        /// <param name="usableItem">the current tool</param>
        public void ClickAction(GameObject gameObject, UsableItem usableItem) {
            if(Enabled) {
                foreach(ClickActionHandler actionHandler in _clickActionHandlers) {
                    if(actionHandler.Matches(gameObject, usableItem)) {
                        actionHandler.InvokeAction(gameObject);
                        break; // Ja Herr Professor, Sie sehen richtig. Voller Stolz verwende ich ein break.
                    }
                }
            }
        }
        
        /// <summary>
        /// Used to Invoke NextDay, all NextDayActionHandlers in NextDayActionHandlers list are iterated through,
        /// only one will be invoked per method call 
        /// </summary>
        /// <param name="gameObject">The affected gameObject</param>
        public void NextDayAction(GameObject gameObject) {
            if(Enabled) {
                foreach(NextDayActionHandler actionHandler in _nextDayActionHandlers) {
                    if(actionHandler.Matches(gameObject)) {
                        actionHandler.InvokeAction(gameObject);
                        break; // Gleich noch einmal. Und ich kann nachts immer noch zufrieden schlafen.
                    }
                }
            }
        }
    }
}