# Entitas-GameFSM
This is Game FSM package using Entitas 1.4.2 framework in Unity


在這個 Unity package下,我基於Entitas (ECS) 架構,實作了有限狀態機FSM
你可以視為是一個系統來處理遊戲中需要以FSM來描述行為的事
這個open project 將來會一直重複利用在未來幾個的遊戲計畫中
而每一個project 都會有unit-test的基本系統可以進行基本的測試驗證


## FSM 使用情境
1. 可以當整個遊戲的流程控制系統, 透過FSM狀態改變觸發事件進行場景切換,UI切換,進入遊戲,進入商店等流程控制
2. 可以當AI FSM, 可以設計成敵人的AI判斷,實作了 stack FSM, 可以設計功能強大一點的AI
3. 任何需要利用FSM流程控制的系統皆適用


## FSM 功能簡介
1. 實作一個FSM System, 可以透過FSMSwitchComponent轉換FSMStateComponent 裡的狀態
2. 支援FSM reset, 可以透過FSMResetComponent將FSM reset成初始狀態
3. 支援FSM Hold/Continue 功能,你可以透過FSMHoldComponent將FSM暫停,再用FSMContinueComponent繼續, 當FSM暫停時, 如果此時有FSMSwitchComponent進來,會暫停這個Component的觸發,一直到FSMContinueComponent出現才能繼續
4. 支援FSM Stack/Return 功能, 在FSMSwitchComponent中有stack 的bool flag,可以設定, FSM System會記下這的狀態 當產生FSMReturnComponent時,會跳回原來記下的狀態,此功能實作了 stack FSM 的功能
5. 支援 FSM event system, 當FSMSwitchComponent發生時,會觸發FSM event ,提供了 interface IFSMSwitchEventListener, 可接受FSM event 的監聽和OnFSMSwitch() 的實作

## FSM 系統介紹  
<p>
call func                  Trigger  
FSMUniTestSystem -------> FSMSwitchService ---------> FSMHoldSystem  
                                            |--------> FSMReturnSystem  
                                            |--------> FSMSwitchStstem  
                                            |--------> FSMResetSystem                                                        
                 ----> FSMAdapterService ------------> FSMAdapterSystem  
                                            |--------> FSMProcessingSystem  
                                            |--------> FSMProcessingEventSystem  
</p>                                            


