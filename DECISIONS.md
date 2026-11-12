# DECISIONS.md

## Week 10 — Enemy AI: Three Enemy Types

### November 11, 2026 — Enemy AI Implementation

Developed three enemy types for the Forest Adventure Unity project:

- **Patroller:** Patrols between boundaries, detects the player using Raycast and field-of-view checks, chases the player, and attacks at close range.
- **Shooter:** Detects the player, charges a magical projectile, shoots towards the player's position, and enters a cooldown state.
- **Charger:** Detects the player, warns before attacking, performs a fast dash, and recovers before its next attack.

Each enemy uses a finite state machine (FSM) with clearly defined states.

### November 11, 2026 — AI Assistance and Debugging

Used ChatGPT to understand enemy state machines, detection systems, and attack mechanics.

During development, a modification to `PatrollerA10.cs` caused compilation errors because several methods were accidentally nested inside another method.

I reviewed the script structure, corrected the misplaced methods, and tested the enemy again in Unity.

I also adjusted the level layout, camera movement, enemy positions, and attack settings during development.

### November 12, 2026 — Playtesting

Conducted a playtest with two new players using an iPhone.

**Player 1**
- Attempts: 4
- Feedback: The second enemy (Shooter) was difficult to pass.

**Player 2**
- Attempts: 4
- Feedback: The joystick was uncomfortable to control, and the second enemy (Shooter) was difficult.

**Average attempts:** 4

Both players completed the level within the target range of 2–4 attempts.

### Playtest Analysis

Both players identified Shooter as the most challenging enemy.

One player also reported difficulty with the joystick controls.

The feedback suggests that Shooter's projectile timing and speed should be reviewed to improve gameplay balance without removing the challenge.

### Planned Balance Adjustments

Based on the feedback, the following changes were proposed:

| Parameter | Previous Value | Proposed Value |
|---|---|---|
| Shooter Cooldown | 2.0 seconds | 2.5 seconds |
| Enemy Bullet Speed | 7 | 6 |

The joystick's touch area should also be checked for usability.

The actual Inspector values and final changes must be confirmed before these adjustments are recorded as completed.

### Conclusion

The playtest provided useful feedback about enemy difficulty and mobile controls. The results informed proposed improvements to the gameplay balance.

The three enemy behaviours and their FSM implementations were tested during development.