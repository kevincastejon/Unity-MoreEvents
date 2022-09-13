# CollisionEvents
Provides **UnityEvent** fields bound to **triggers** and **collisions** Unity messages methods with optional **tag filtering**.
Do not create dedicated scripts each time you need to react to a collision or trigger anymore.
Call methods on any script thanks to the **UnityEvent** serialization power.

## CollisionEvent
- **OnCollisionEnter**
- **OnCollisionStay**
- **OnCollisionExit**

## Collision2DEvent
- **OnCollisionEnter2D**
- **OnCollisionStay2D**
- **OnCollisionExit2D**

## TriggerEvent
- **OnTriggerEnter**
- **OnTriggerStay**
- **OnTriggerExit**

## Trigger2DEvent
- **OnTriggerEnter2D**
- **OnTriggerStay2D**
- **OnTriggerExit2D**

## How to use
Add the desired component to a **GameObject**.

Bind methods though the provided **UnityEvents** fields.

Optionally filter objects by tag.

Checks the currently colliding/triggering objects through a list into the inspector.

![Tuto1](https://kevincastejon.github.io/Unity-CollisionEvents/Assets/KevinCastejon/CollisionEvents/Documentation/Tuto1.png)
![Tuto2](https://kevincastejon.github.io/Unity-CollisionEvents/Assets/KevinCastejon/CollisionEvents/Documentation/Tuto2.png)
