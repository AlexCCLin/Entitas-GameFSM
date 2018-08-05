//created by snippet - ecs_feature_systems


public sealed class FSMGameSystems : Feature {
	public FSMGameSystems(Contexts contexts) {

		Add (new FSMUniTestSystem (contexts)); //UniTest
		//以下順序綁定
		//為了達成處理System hold著時, FSMSwitch依然留著, 當FSMContinue 解開FSMHold時
		//留在entity裡的FSMSwitch仍然可以被處裡
		//手法是當 Hold System處理FSMContine ,remove FSMHold時,會copy 一份原FSMSwitch
		//篩除原來的FSMSwitch, 再新增加一個FSMSwitch,目的是為了trigger FSMSwitchSystem
		//達成以上功能
		Add(new FSMHoldSystem(contexts));
		Add(new FSMReturnSystem (contexts));
		Add(new FSMSwitchSystem(contexts));
		Add(new FSMResetSystem(contexts));
		Add (new FSMAdapterSystem (contexts));				//For Event Processing
		Add(new FSMProcessingSystem(contexts));				//For Event Processing
		Add (new FSMProcessingEventSystem (contexts));  	//For Event Processing
		//
	}
}