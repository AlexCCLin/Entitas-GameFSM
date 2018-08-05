//created by snippet - ecs_react_cleanup_system
using System.Collections.Generic;
//using System.Linq;
using Entitas;
public sealed class FSMHoldSystem : ReactiveSystem<GameEntity>, ICleanupSystem{

	//connect services
	//FSMDebugService fsm_debug_service = FSMDebugService.singleton;

	readonly Contexts _contexts;
	readonly IGroup<GameEntity> _fsm_continue_entities;

	public FSMHoldSystem (Contexts contexts) : base(contexts.game){
		_contexts = contexts;
		_fsm_continue_entities = _contexts.game.GetGroup(GameMatcher.FSMContinue);
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector (GameMatcher.FSMContinue.Added());
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasFSMHold & entity.hasFSMContinue;
	}

	protected override void Execute(List<GameEntity> entities) {
		//fsm_debug_service.fsm_msg_report ("==> hold execute");
		foreach (var e in entities)
		{
			e.RemoveFSMHold ();

			if (e.hasFSMSwitch) { 
				//如果有殘留FSMSwitch, 刪除,並重生FSMSwitch trigger FSMSwitch System
				GAMESTATE _from_state = e.fSMSwitch.from_state;
				GAMESTATE _to_state = e.fSMSwitch.to_state;
				bool _stack = e.fSMSwitch.stack;
				e.RemoveFSMSwitch ();

				e.AddFSMSwitch (_stack, _from_state, _to_state);

			}
			if (e.isFSMReturn) {
				//如果有殘留FSMReturn, 刪除,並重生FSMReturn trigger FSMSwitch System
				e.isFSMReturn = false;

				e.isFSMReturn = true;
			}
		}
	}

	public void Cleanup() {
		foreach(var e in _fsm_continue_entities.GetEntities()) {
			e.RemoveFSMContinue();

		}
	}

}