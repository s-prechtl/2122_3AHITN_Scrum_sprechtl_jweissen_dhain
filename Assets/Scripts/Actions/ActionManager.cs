using System.Collections.Generic;
using Assets.Scripts.Actions;
using UnityEngine;

namespace Actions {
    public class ActionManager {
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

        private List<ActionHandler> _actionHandlers;
        public List<ActionHandler> ActionHandlers => _actionHandlers;
        
        private ActionManager() {
            _actionHandlers = new List<ActionHandler>();
            instatiateActionHandlers();
        }

        private void instatiateActionHandlers() {
            ActionHandlers.Add(new GrassTileHoeActionHandler());
            ActionHandlers.Add(new GrassTileShovelActionHandler());
            ActionHandlers.Add(new GrassTileFenceActionHandler());
            
            ActionHandlers.Add(new FarmlandTileShovelActionHandler());
            ActionHandlers.Add(new FarmlandTileScytheActionHandler());
            ActionHandlers.Add(new FarmlandTileWateringCanActionHandler());
            ActionHandlers.Add(new FarmlandTileWheatSeedsActionHandler());
            
            ActionHandlers.Add(new WaterTileShovelActionHandler());
            ActionHandlers.Add(new WaterTileFishingRodActionHandler());
        }
        
        public void HandleAction(GameObject gameObject, UsableItem usableItem) {
            foreach (ActionHandler actionHandler in ActionHandlers) {
                if(actionHandler.Matches(gameObject, usableItem)) {
                    actionHandler.InvokeAction(gameObject, usableItem);
                }
            }
        }
    }
}