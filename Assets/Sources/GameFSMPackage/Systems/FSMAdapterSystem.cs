//created by snippet - ecs_react_init_system
using System.Collections.Generic;
//using System.Linq;
using Entitas;
using UnityEngine;


public sealed class FSMAdapterSystem : ReactiveSystem<GameEntity>, ICleanupSystem {



	//connect services
	FSMAdapterService fsm_adapter_service = FSMAdapterService.singleton;

	//readonly Contexts _contexts;
	readonly IGroup<GameEntity> _fsm_adapter_entities;

	public FSMAdapterSystem (Contexts contexts) : base(contexts.game){
		//_contexts = contexts;
		_fsm_adapter_entities = contexts.game.GetGroup(GameMatcher.FSMAdapter);
	}


	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector (GameMatcher.FSMAdapter);
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasFSMAdapter;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var e in entities)
		{
			fsm_adapter_service.fsm_create_prefab_adapter (e);
		}
	}


	public void Cleanup() {
		foreach(var e in _fsm_adapter_entities.GetEntities()) {
			e.RemoveFSMAdapter ();
		}
	}

}