using System.Collections.Generic;
using Assets.Scripts.Actions;
using UnityEngine;

namespace Actions {
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
        
        private ActionManager() {
            _clickActionHandlers = new List<ClickActionHandler>();
            _nextDayActionHandlers = new List<NextDayActionHandler>();
            InstantiateClickActionHandlers();
            InstantiateNextDayActionHandlers();
        }

        private void InstantiateNextDayActionHandlers() {
            _nextDayActionHandlers.Add(new FarmlandTileNextDayActionHandler());
        }

        private void InstantiateClickActionHandlers() {
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
        
        public void ClickAction(GameObject gameObject, UsableItem usableItem) {
            foreach (ClickActionHandler actionHandler in _clickActionHandlers) {
                if(actionHandler.Matches(gameObject, usableItem)) {
                    actionHandler.InvokeAction(gameObject);
                }
            }
        }
        
        public void NextDayAction(GameObject gameObject) {
            Debug.Log("nextday action");
            foreach (NextDayActionHandler actionHandler in _nextDayActionHandlers) {
                if(actionHandler.Matches(gameObject)) {
                    actionHandler.InvokeAction(gameObject);
                }
            }
        }
    }
}