# PluggableAI
Finite state machine, which can be easily modified in Unity’s inspector.

From private repository for "Zespołowe Przedsięwzięcie Informatyczne" school subject.  
**Work in progress**

Example state:  
![alt text](http://s6.ifotos.pl/img/CheckTarg_qnqrppn.png)  

Decisions can have side effects for example TargetInSight can update last known position of target.  
RemainInState is a dummy state saying we don't want to change state.  

### Installation:
1. Paste "PluggableAI" folder into Assets folder.
2. Add "State Controller" component to gameobject and fill public fields with data.

Based on:  
https://youtu.be/cHUXh5biQMg?list=PLX2vGYjWbI0ROSj_B0_eir_VkHrEkd4pi
