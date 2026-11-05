# Magic Forest

A 2D fantasy mobile platform adventure built in **Unity 6.6**.
Explore magical forests, jump between platforms, collect coins, avoid enemies,
and face a boss in the third level.

## Project overview

- **Genre:** 2D platform adventure
- **Target:** iOS / Android landscape UI (Unity Device Simulator tested)
- **Scenes:** `Main menu`, `Forest_Level1`, `Forest_Level2`, `Forest_Level3`
- **Controls:** on-screen joystick, JUMP, FIRE (where available), PAUSE
- **Pause menu:** CONTINUE, RESTART, MAIN MENU
- **HUD:** player hearts; boss health where relevant; mobile controls

## Opening the project

1. Clone or download this repository.
2. Open its folder in Unity Hub using a compatible Unity 6.6 editor.
3. Open `Assets/Scenes/Main menu.unity` (or another scene in `Assets/Scenes`).
4. Use the Unity Device Simulator to inspect landscape layouts for a 16:9
   phone, a notched phone and a 4:3 tablet.

## Git workflow

The `mobile-ui` feature branch was merged into `main` using
[Pull Request #1](https://github.com/snezhiinka/ForestAdventure_RK2/pull/1).
Additional repository review was committed from a separate GitHub account.

## Documentation

- `Magic_Forest_GDD.pdf` — concise game design document
- `AI_LOG.txt` — AI assistance disclosure
- `REVIEW_NOTES.md` — repository review from a second account

## Important notes

The game is edited and tested in Unity; screenshots of the Device Simulator
are part of the course submission. Do not commit `Library/`, `Logs/`, build
folders or local Unity cache files. Asset attribution and final on-device
build verification should be completed before any public release.
