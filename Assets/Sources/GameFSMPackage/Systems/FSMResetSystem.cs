//created by snippet - ecs_react_cleanup_system
using System.Collections.Generic;
//using System.Linq;
using Entitas;
public sealed class FSMResetSystem : ReactiveSystem<GameEntity>, ICleanupSystem{

	//connect services
	

	//readonly Contexts _contexts;
	readonly IGroup<GameEntity> _fsm_reset_entities;

	public FSMResetSystem (Contexts contexts) : base(contexts.game){
		//_contexts = contexts;
		_fsm_reset_entities = contexts.game.GetGroup(GameMatcher.FSMReset);
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector (GameMatcher.FSMReset.Added());
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasFSMState & entity.hasFSMReset;
	}

	protected override void Execute(List<GameEntity> entities) {
		//_contexts.gameState.ReplaceScore (_contexts.gameState.score.value + entities.Count);
		foreach (var e in entities)
		{
			e.fSMState.previous_state = GAMESTATE.GAME_INIT;
			e.fSMState.current_state = GAMESTATE.GAME_INIT;

			if (e.hasFSMStack) {
				e.fSMStack.fsm_stack.Clear ();
				e.fSMStack.fsm_stack.Add (GAMESTATE.GAME_INIT);
			}

			if (e.isFSMProcessing) {
				e.isFSMProcessing = false;
				e.RemoveFSMProcessingCnt ();
			}
		}
	}

	public void Cleanup() {
		foreach(var e in _fsm_reset_entities.GetEntities()) {
			if (e.hasFSMHold)
				e.RemoveFSMHold ();
			
			e.RemoveFSMReset();
		}
	}

}