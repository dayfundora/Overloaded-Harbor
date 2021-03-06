# ⚓ Overloaded Harbor
### Simulation Project Based on Discreet Events 📓



#### About the Problem
In a super-tanker harbor that has 3 docks and a tugboat for unloading these ships simultaneously, you want to know the average waiting time of the ships to be loaded in the harbor. The harbor has a tugboat available to assist the tankers. Tankers of any size need a tugboat to approach the dock from the harbor and to leave the dock back to the harbor. The arrival interval time of each ship is distributed by an exponential function with `λ = 8 hours`. There are three different sizes of tankers: small, medium and large, the probability corresponding to the size of each tanker is described in the following table. The loading time of each tanker depends on its size and the normal distribution parameters that represent it are also described in the following table.

|  Size  | Arrival Probability |  Loading Time   |
| :----: | :-----------------: | :-------------: |
| Small  |        0.25         | µ = 9    θ2 = 1 |
| Medium |        0.25         | µ = 12   θ2 = 2 |
| Large  |         0.5         | µ = 18   θ2 = 3 |

Generally, when a tanker arrives at the harbor, it waits in a (virtual) queue until there is an empty dock and a tugboat is available to serve it. When the tugboat is available, it assists it so that it can start its load, this process takes a time that distributes exponentially with `λ = 2 hours`. The loading process begins immediately after the ship arrives at the dock. Once this process is finished, the assistance of the tugboat is necessary (waiting until it is available) to take it back to the port, the time of this operation is exponentially distributed with `λ = 1 hour`. The transfer between the harbor and a dock by the tugboat without a tanker distributes exponential with `λ = 15 minutes`.

When the tugboat finishes the operation of approaching a tanker to the dock, it then takes the first ship that was waiting to leave to the harbor, in case there is no ship to leave and a dock is empty, then the tugboat heads towards the harbor to take the first ship waiting for the empty dock; in case that no ship is waiting, then the tugboat will wait for a ship at a dock to take it to the harbor. When the tugboat completes the operation of taking a ship to the port, it immediately takes the first ship waiting to the empty dock. In case there are no ships at the docks, and no ships waiting to go to the dock, then the tugboat stays in the harbor waiting for a ship to take to a dock.

**Fully simulate port operation. Determine the average waiting time at the docks.**

####  Main Idea followed to solve the Problem
A series of events is used to know what state the tugboat is in, and depending on this state and the queues used to save information about the ships waiting in the harbor and/or docks, make the tugboat move according to specifications.

**Discrete Event Simulation Model Developed:**
* *Ship arrival:* The ships arrive following an exponential distribution, when the event of the arrival of a ship is fulfilled, this ship is added to the queue to enter the dock.
* *Enter boat:* Once the tugboat tows the first boat in the queue to enter, and they reach the dock, this event is fulfilled.
* *Loading boat:* The boats start loading right when they reach the dock. The time they must finish is generated, depending on their size, and when they finish loading this event is generated. The ship is added to the queue of those who are ready to leave.
* *Take out the boat:* If the tugboat is on the dock and a boat wants to leave, then that space is freed on the dock and the tugboat takes care of taking the boat out.
* *Tugboat alone from dock to harbor:* When the tugboat has to move without carrying any ship. It happens when a ship is waiting in the harbor, and there are some empty space in the dock and no ship is ready to leave.
* *Tugboat alone from harbor to dock:* When the tugboat has to move without carrying any ship. It occurs when the tugboat is in the harbor, and there are no ships wanting to enter the dock, and some ship in the dock is ready leave.

#### Considerations obtained from the execution of the problem’s simulations:
Considerations obtained from the data of 10 simulation runs. In 20 days (`480 hours`) and with 4 docks:
* The average waiting at the port: `8 hours and 19 minutes` to `35 minutes`.
* The average waiting at the dock: `13 hours` to `15 hours and 45 minutes`.