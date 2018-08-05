//created by snippet - ecs_react_cleanup_system
using System.Collections.Generic;
//using System.Linq;
using Entitas;
public sealed class FSMProcessingSystem : ReactiveSystem<GameEntity> {

	//connect services
	FSMDebugService fsm_debug_service = FSMDebugService.singleton;

	//readonly Contexts _contexts;
	//readonly IGroup<GameEntity> _listener_list;

	public FSMProcessingSystem (Contexts contexts) : base(contexts.game){
		//_contexts = contexts;
		//_listener_list = contexts.game.GetGroup(GameMatcher.FSMProcessingListener);
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector (GameMatcher.FSMProcessingCnt);
	}

	protected override bool Filter(GameEntity entity) {
		return entity.isFSMProcessing & entity.hasFSMProcessingCnt;
	}

	protected override void Execute(List<GameEntity> entities) {
		fsm_debug_service.fsm_msg_report ("fsm processing execute : ");
		foreach (var e in entities)
		{
			//fsm_debug_service.fsm_msg_report ( e.fSMProcessingCnt.listener_busy_cnt.ToString() );
			if (e.fSMProcessingCnt.listener_busy_cnt == 0) {
				e.isFSMProcessing = false;
				e.RemoveFSMProcessingCnt ();
			}
		}
	}
		

}