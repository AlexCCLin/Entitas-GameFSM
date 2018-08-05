//created by snippet - ecs_react_cleanup_system
using System.Collections.Generic;
//using System.Linq;
using Entitas;
public sealed class FSMReturnSystem : ReactiveSystem<GameEntity>, ICleanupSystem{

	//connect services
	FSMDebugService fsm_debug_service = FSMDebugService.singleton;

	//readonly Contexts _contexts;
	readonly IGroup<GameEntity> _fsm_return_entities;


	public FSMReturnSystem (Contexts contexts) : base(contexts.game) {
		//_contexts = contexts;
		_fsm_return_entities = contexts.game.GetGroup(GameMatcher.FSMReturn);
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {

		return context.CreateCollector (GameMatcher.FSMReturn.Added ());
		//return context.CreateCollector (GameMatcher.AnyOf(GameMatcher.FSMReturn, GameMatcher.FSMContinue)); //這樣不行!! no trigger
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasFSMState & entity.hasFSMStack & entity.isFSMReturn & !entity.hasFSMHold & !entity.hasFSMSwitch;
	}

	protected override void Execute(List<GameEntity> entities) {
		//fsm_debug_service.fsm_msg_report ("==> return execute");
		foreach (var e in entities)
		{
			//Return FSM
			if (e.fSMStack.fsm_stack.Count <= 1) {
				fsm_debug_service.fsm_msg_report ("Warning!! FSM is alredy in last state"); 
			} else {
				//e.fSMState.previous_state = e.fSMState.current_state;
				//e.fSMState.current_state = e.fSMStack.fsm_stack [0];
				GAMESTATE _state = e.fSMStack.fsm_stack[e.fSMStack.fsm_stack.Count-2];
				e.AddFSMSwitch(false, GAMESTATE.GAME_X, _state);
				e.fSMStack.fsm_stack.RemoveAt (e.fSMStack.fsm_stack.Count - 1);
			}
		}
	}

	public void Cleanup() {
		foreach(var e in _fsm_return_entities.GetEntities()) {
			if (!e.hasFSMHold)
				e.isFSMReturn = false;
		}
	}

}