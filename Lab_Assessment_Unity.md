# Game Programming using Unity — Lab Assessment (Challenging Task)

| | |
|---|---|
| **Roll No / Section** | BL2026270100720 · C11 + C12 |
| **Student Name** | ______________________ |
| **Unity Version** | Unity 6 (6000.x) / 2022.3 LTS — 3D (URP or Built-in; note which one you used) |
| **Submission** | 06 October 2026, midnight |

> **How to use this file:** Every `📸 SCREENSHOT n` box is a place where you paste a screenshot from your Unity Editor. The text next to it says exactly *what* to capture. Fill the `Observation` sections with your own words after you actually run the steps. C# scripts are in the `Scripts/` folder (also embedded below by name).

**Project setup (do once)**
1. Unity Hub → New Project → **3D (URP)** → name `GameLab_BL2026270100720`.
2. `Window → Package Manager` → install **Input System** (Unity Registry). When asked to enable new backends, click **Yes** (restarts Editor). Also confirm **Cinemachine is NOT required** (we write our own camera).
3. Create folders in `Assets/`: `Art`, `Materials`, `Scenes`, `Scripts`, `Audio`, `Animations`, `Prefabs`, `Terrain`.
4. Copy the scripts from `Scripts/` into `Assets/Scripts/`.
5. `Edit → Project Settings → Player → Active Input Handling` = **Input System Package (New)** or **Both**.

---

# Q1. Asset Integration, Materials, Shaders and Lighting

### 1.1 Download & import assets
1. Open `Window → Asset Store` (or <https://assetstore.unity.com>) → pick **free** assets (filter *Price: Free*). Suggested:
   - **3D model:** "Low Poly Environment / Props" pack, or a free rock/tree/statue/building.
   - **Texture:** a free PBR texture pack (e.g. stone, wood, metal).
   - **Third asset:** skybox pack, or particle/VFX pack, or another model.
2. Click **Add to My Assets** → in Unity: `Window → Package Manager → My Assets` → **Download → Import**.
3. Verify the files appear under `Assets/`.

📸 **SCREENSHOT 1.1** – Asset Store page of each of the 3 assets (show names).
📸 **SCREENSHOT 1.2** – Unity *Project* window showing the 3 imported asset folders.

| # | Asset name | Type (model/texture/skybox…) | Publisher |
|---|---|---|---|
| 1 | | 3D model | |
| 2 | | Texture | |
| 3 | | | |

### 1.2 Build the scene
1. `File → New Scene` → Basic (URP). Save as `Scenes/Q1_Showcase`.
2. Add a Plane (ground). Drag your 3D model into the scene (position 0,0,0).
3. Add the other assets (skybox: `Window → Rendering → Lighting → Environment → Skybox Material`).

📸 **SCREENSHOT 1.3** – Scene view with the model placed.

### 1.3 Create & modify a material
1. `Assets/Materials` → right-click → `Create → Material` → name `M_Showcase`.
2. Assign it to the model (drag onto the object).
3. In the **Inspector** modify and note down:
   - **Base Map / Albedo** → drag the imported texture.
   - **Tiling** (e.g. 2×2), **Offset**.
   - **Metallic** (0→1), **Smoothness** (0→1).
   - **Normal Map** (drag a normal texture; set texture type to *Normal map*).
   - **Emission** (optional glow).

📸 **SCREENSHOT 1.4** – Material Inspector with texture, metallic and smoothness set.
📸 **SCREENSHOT 1.5** – Texture import settings (Inspector of the texture: Max Size, Compression).

### 1.4 Two different shaders on the same object
Use a **duplicate** of the object (or change shader on the same material, taking a screenshot each time) so you can compare side-by-side.
| Setup | How |
|---|---|
| **Shader A: Lit** | Material → Shader → `Universal Render Pipeline/Lit` (Built-in: `Standard`) |
| **Shader B: Unlit** | Material → Shader → `Universal Render Pipeline/Unlit` |
| **Shader C (optional): Simple Lit / Toon / Shader Graph** | `Universal Render Pipeline/Simple Lit` or build a Shader Graph (`Create → Shader Graph → URP → Lit`) |

📸 **SCREENSHOT 1.6** – Object with Shader A (and its Inspector).
📸 **SCREENSHOT 1.7** – Same object with Shader B (and its Inspector).
📸 **SCREENSHOT 1.8** – Side-by-side of both objects in Game view.

**Comparison table (fill in):**
| Property | Lit | Unlit |
|---|---|---|
| Reacts to lights? | Yes (diffuse+specular) | No – flat colour/texture |
| Shadows received | Yes | No |
| Metallic/Smoothness/Normal map | Supported | Not supported |
| Relative GPU cost | Higher | Lower |
| Observation | | |

### 1.5 Two lighting setups
| | **Setup 1 – Daylight** | **Setup 2 – Night / Mood** |
|---|---|---|
| Directional Light | White-yellow, Intensity 1.2, Rotation (50,-30,0), Shadows *Soft* | Intensity 0.1–0.2, blue tint |
| Extra lights | none | Point Light (warm orange, Range 8, Intensity 3) / Spot Light |
| Skybox / Ambient | Bright skybox, Ambient *Skybox* | Dark skybox or Ambient colour dark blue |
| Fog (Lighting → Environment) | off | on, density 0.02 |

Steps: `GameObject → Light → Directional/Point/Spot`. Toggle setups by enabling/disabling the lights (or duplicate scene).

📸 **SCREENSHOT 1.9** – Setup 1 (Scene + Light Inspector).
📸 **SCREENSHOT 1.10** – Setup 2 (Scene + Light Inspector).

### 1.6 Observation & Explanation (write in your words; sample below)
- **Texture** = image data applied onto the model’s UV map; gives colour/detail (albedo, normal, roughness maps).
- **Material** = a set of parameters (which textures, colour, metallic, smoothness, tiling) *using* a shader.
- **Shader** = GPU program that decides how light, texture and material parameters are combined into final pixel colour. Lit shaders calculate lighting; Unlit just outputs colour.
- **Lighting** = provides the light that the shader calculates with; changing colour/intensity/direction/shadows totally changes mood, e.g. same stone looks warm at day and cold at night; metallic/smooth surfaces show highlights only when lights/reflections exist.
- **Together:** Final look = *Mesh + Texture → Material (parameters) → Shader (math) ← Lights/Environment*. Removing any one (e.g. no light for a Lit shader) makes the object look wrong/black.

Your observations: ______________________________________

---

# Q2. Terrain, Character, Animation and Audio Integration

### 2.1 Terrain from a heightmap
1. Get a heightmap: a greyscale **.raw** (16-bit) file – download a free one (e.g. from <https://tangrams.github.io/heightmapper/> → export, or Unity Asset Store free "Terrain Heightmaps"). Place it in `Assets/Terrain/`.
2. `GameObject → 3D Object → Terrain`.
3. Select Terrain → Inspector → **Terrain Settings (gear tab) → Heightmap → Import Raw…** → choose file → set *Depth 16 bit*, *Width/Height* (e.g. 513 × 513), *Byte Order* (Windows), *Terrain Size* (e.g. 500×100×500) → **Import**.
4. Paint: *Paint Texture* tab → Add terrain layers (grass, rock) from imported textures; optional *Paint Trees/Details*.
5. Smooth if needed: *Raise/Lower → Smooth Height*.

📸 **SCREENSHOT 2.1** – Import Raw dialog.
📸 **SCREENSHOT 2.2** – Terrain in Scene view after heightmap + textures.
📸 **SCREENSHOT 2.3** – Terrain Inspector (Terrain Settings tab).

### 2.2 Rigged & animated character
1. Download **Mixamo** character (<https://www.mixamo.com>, free Adobe login): pick a character → *Download → FBX for Unity, T-pose, With Skin*. Download animations: **Idle**, **Walking**, **Running**, **Jump** (*Format FBX, Without Skin*).
2. Drag FBXs into `Assets/Animations`.
3. Select the **character FBX → Inspector → Rig tab → Animation Type = Humanoid → Apply**. (Check *Configure…* shows green skeleton.)
4. Select each animation FBX → **Rig = Humanoid**, *Avatar Definition: Copy From Other Avatar* → pick the character’s avatar → Apply. In **Animation** tab tick **Loop Time** for Idle/Walk/Run.
5. Place character in scene on terrain, add **Animator** (auto-added).
6. Create `Animations/Player.controller` (Animator Controller) → assign it to Animator → build states (see Q3 for transitions).

📸 **SCREENSHOT 2.4** – Character FBX *Rig* tab (Humanoid, Avatar configured).
📸 **SCREENSHOT 2.5** – Animation clip import settings (Loop Time ticked).
📸 **SCREENSHOT 2.6** – Animator window with Idle & Walk (min. two animations).
📸 **SCREENSHOT 2.7** – Character standing on the terrain.

### 2.3 Audio
1. Import free audio (e.g. `footstep.wav`, `jump.wav`) from the Asset Store / freesound.org into `Assets/Audio`.
2. Select Player → `Add Component → Audio Source` → drag footstep clip, **untick Play On Awake**, *Spatial Blend* 1 (3D) or 0.
3. Add background music: empty object `Ambience` → AudioSource → `Loop` ✔, *Play On Awake* ✔.
4. Player controller (`PlayerController.cs`, Q3) plays the footstep when moving and jump sound on jump → this is the **audio triggered by a player action**.

📸 **SCREENSHOT 2.8** – Audio clip import settings.
📸 **SCREENSHOT 2.9** – Player Inspector with AudioSource + script assigned.

### 2.4 Lighting
Directional Light (sun) + skybox + `Window → Rendering → Lighting` → Environment, optionally fog. Press **Generate Lighting** (Lighting window) if baked.

📸 **SCREENSHOT 2.10** – Game view while playing (character walking across the terrain).

### 2.5 Interaction test
Press Play → move with WASD → footsteps play while moving; stop → silence; Space → jump sound.

📸 **SCREENSHOT 2.11** – Console/inspector showing AudioSource playing (the little ♪ icon / Audio Source “is playing” while in play mode).

### 2.6 Explanation – how Unity imports & manages rigged models, animations, audio
- **Models:** FBX is imported by the *Model Importer* — creates Mesh, Materials, and (if rigged) a hierarchy of bones. The *Rig* tab sets *Animation Type* (None/Legacy/Generic/Humanoid). **Humanoid** maps bones to a standard **Avatar**, which lets animations be *retargeted* between different characters.
- **Animations:** Each clip in an FBX is an `AnimationClip` (curves on bone transforms). Import settings: loop time, root motion, frame ranges. Clips are organised into an **Animator Controller** (state machine with states, transitions and parameters). The **Animator** component on the character runs the controller using the Avatar.
- **Audio:** Imported by the *Audio Importer* (Load Type: Decompress On Load / Compressed In Memory / Streaming; compression format Vorbis/PCM/ADPCM; Force To Mono; Sample rate). An **AudioClip** is played via an **AudioSource** (the speaker) and heard by the **Audio Listener** (on Main Camera). 3D audio uses *Spatial Blend* and distance rolloff.
- All are **assets** with `.meta` files (GUID + import settings) managed in the Project window; scripts refer to them by reference in the Inspector.

Your observations: ______________________________________

---

# Q3. Advanced Player Controller and Camera System

### 3.1 Input Actions (new Input System)
1. `Assets → Create → Input Actions` → name `PlayerInputActions`. Double-click.
2. Action Map `Player` with actions:
   | Action | Type | Binding |
   |---|---|---|
   | Move | Value / Vector2 | **2D Vector Composite**: W/S/A/D (+ Left Stick of gamepad) |
   | Jump | Button | Space |
   | Crouch | Button | Left Ctrl / C |
   | Sprint | Button | Left Shift |
3. **Save Asset**. Then drag actions from the asset into the `PlayerController` fields (`Move Action`, `Jump Action`…). *(Expand the asset in Project, drag the sub-items "Move", "Jump" etc. — they are `InputActionReference`s.)*

📸 **SCREENSHOT 3.1** – Input Actions editor with all four actions and bindings.

### 3.2 Player GameObject
1. Player root (your Mixamo character) → Tag = **Player**.
2. `Add Component → Character Controller` (Height 1.8, Radius 0.3, Center (0,0.9,0)).
3. Add `PlayerController.cs`. Assign: Animator (child), Camera Transform (Main Camera), Audio Source, input references.
4. Add `SmoothCameraFollow.cs` to **Main Camera** → Target = Player, Offset (0,3,-6), Smooth Time 0.2.

📸 **SCREENSHOT 3.2** – Player Inspector (Character Controller + PlayerController).
📸 **SCREENSHOT 3.3** – Main Camera Inspector with SmoothCameraFollow.

### 3.3 Animator Controller
Parameters (Animator window → Parameters tab → `+`):
`Speed` (Float), `IsGrounded` (Bool), `IsCrouching` (Bool), `Jump` (Trigger).

States: **Idle**, **Walk**, **Run**, **Jump**, (optional **Crouch**).
Best way: create a **Locomotion Blend Tree** (Right-click → Create State → From New Blend Tree), Parameter `Speed`: Idle (0), Walk (3), Run (6). Then:

| Transition | Condition | Has Exit Time | Duration |
|---|---|---|---|
| Locomotion → Jump | `Jump` trigger | ✘ | 0.05 |
| Jump → Locomotion | `IsGrounded` = true | ✘ | 0.1 |
| Locomotion → Crouch | `IsCrouching` = true | ✘ | 0.1 |
| Crouch → Locomotion | `IsCrouching` = false | ✘ | 0.1 |

(Alternative without blend tree: Idle→Walk `Speed>0.1`, Walk→Run `Speed>4.5`, Run→Walk `Speed<4.5`, Walk→Idle `Speed<0.1`, Any State→Jump `Jump`.)

📸 **SCREENSHOT 3.4** – Animator graph with all states & transitions.
📸 **SCREENSHOT 3.5** – Parameters panel.
📸 **SCREENSHOT 3.6** – Blend tree inspector (or one transition’s Inspector).

### 3.4 Script explanation (`Scripts/PlayerController.cs`, `SmoothCameraFollow.cs`)
- **Movement:** `Move` vector → camera-relative direction → `CharacterController.Move`.
- **Run:** Sprint held → `runSpeed` (6) instead of `walkSpeed` (3); crouch → 1.5.
- **Acceleration/deceleration:** `Vector3.MoveTowards(current, target, rate*dt)` — different rates for speeding up (`acceleration`) and slowing (`deceleration`), so no instant start/stop.
- **Jump:** `v = sqrt(h × -2g)`; gravity applied every frame.
- **Crouch:** shrinks CharacterController height and lowers speed; head-check prevents standing under a ceiling.
- **Invalid actions prevented:** jump only when `cc.isGrounded` (no double/air jumps, no spamming), no jump while crouching, crouch only when grounded, run only when moving forward & not crouching.
- **Camera:** `Vector3.SmoothDamp` for position + `Slerp` for rotation in `LateUpdate` (after player moved) → smooth lag, no snapping.

### 3.5 Testing (do & note)
| Test | Expected | Result ✔/✘ |
|---|---|---|
| W/A/S/D move in all directions | Moves relative to camera | |
| Hold Shift | Faster speed, Run anim | |
| Release keys | Slows gradually, returns to Idle | |
| Space on ground | Jumps once, Jump anim | |
| Mash Space in air | **No** extra jump | |
| Ctrl | Crouches, slower, crouch anim | |
| Space while crouched | No jump | |
| Camera when moving/stopping | Follows with smooth lag | |

📸 **SCREENSHOT 3.7** – Idle state in Game view + Animator showing Idle active (Play mode, both windows visible).
📸 **SCREENSHOT 3.8** – Walk/Run state.
📸 **SCREENSHOT 3.9** – Jump state (character in air).
📸 **SCREENSHOT 3.10** – Crouch state.
🎥 *Record a 20–30 s video (Win+G, or `Window → Package Manager → Unity Recorder`) showing all actions + camera lag.*

Your observations: ______________________________________

---

# Q4. Level Design, Collision Detection and Scene Management

### 4.1 Build Level 1
1. New scene `Level1`. Ground: Terrain or Cubes as platforms (`3D Object → Cube`, scale e.g. (10,1,10)).
2. **Start area:** empty `StartPoint` + a green platform; place Player there.
3. **Obstacles (≥5)** – make each a different type:
   | # | Obstacle | Setup |
   |---|---|---|
   | 1 | Spike pad | Cube, red material, Box Collider **Is Trigger ✔**, `DangerousObstacle` |
   | 2 | Lava strip | flat Cube, orange, Trigger, `DangerousObstacle` (damage 50) |
   | 3 | Moving block | Cube + **Rigidbody (Is Kinematic ✔)** + `MovingObstacle` + `DangerousObstacle` (trigger child) |
   | 4 | Wall / crate stack | Cubes with **Rigidbody** (non-kinematic, mass 1) – pushable physics objects |
   | 5 | Rotating hammer/ball | Sphere + Rigidbody + trigger damage |
   | 6 | Gap between platforms | Player falls → kill plane (Trigger below level → `DangerousObstacle` with damage 100) |
4. **Interactive object:** a Button (small cube, trigger) + `InteractiveButton` with a **Door** (cube blocking the path). Press **E** near it → door lifts.
5. **Goal area:** gold platform with a Cube trigger → add `GoalTrigger` (Next Scene Name = `Level2`).
6. Add `PlayerHealth` to Player (assign Respawn Point). Tag Player = `Player`.

📸 **SCREENSHOT 4.1** – Level 1 overall (Scene view, top-down, labelled start/obstacles/goal).
📸 **SCREENSHOT 4.2** – Collider inspector of a trigger obstacle (Is Trigger ✔) + script.
📸 **SCREENSHOT 4.3** – Rigidbody inspector of a physics object.
📸 **SCREENSHOT 4.4** – Interactive button & door setup.
📸 **SCREENSHOT 4.5** – Goal trigger inspector.

### 4.2 Collision / trigger behaviour
- Touch hazard → `OnTriggerEnter` → `PlayerHealth.TakeDamage(25)` (with cooldown).
- Health reaches 0 → scene restarts (respawn).
- Reach goal → `GoalTrigger` → `SceneLoader.Load("Level2")`.
- Colliders = shape for physics; **Trigger** = detects overlap without blocking; **Rigidbody** = physics simulation (gravity, forces, pushing); **Kinematic Rigidbody** = scripted motion that still affects others.
> Note: `CharacterController` does not receive `OnCollisionEnter`; that’s why hazards use **triggers**.

📸 **SCREENSHOT 4.6** – Play mode: Health label dropping after touching obstacle + Console log.
📸 **SCREENSHOT 4.7** – Player at goal area.

### 4.3 Level 2 & Scene Management
1. `File → Save As` Level1 → `Level2`; change layout (rearrange obstacles, new colour), set Goal's Next Scene = `MainMenu` (or Level1).
2. **Add scenes to build:** `File → Build Profiles (Build Settings) → Add Open Scenes` for every scene: MainMenu (0), Level1 (1), Level2 (2).
3. Scripts: `SceneLoader.cs`, `GoalTrigger.cs`, `MainMenu.cs`.

📸 **SCREENSHOT 4.8** – Build Settings/Profiles scene list.
📸 **SCREENSHOT 4.9** – Level 2 layout.
📸 **SCREENSHOT 4.10** – Hierarchy of Level 1 showing organised objects.

### 4.4 Testing table
| Test | Expected | Result |
|---|---|---|
| Touch spikes | Health −25, cooldown works | |
| Fall into pit | Dies / restarts | |
| Push crate | Rigidbody moves | |
| Press E at button | Door opens | |
| Enter goal | Level2 loads | |

Your observations: ______________________________________

---

# Q5. Game Performance Profiling and Optimization

### 5.1 Preparation
1. Create `MainMenu` scene: Canvas + Title + *Play* button (OnClick → `MainMenu.Play`).
2. Add `PerformanceLogger.cs` to an empty object in every scene (shows FPS, memory, draw calls; **F1** prints a line to Console).
3. Add `ObjectSpawner.cs` to an empty object in Level1; prefab = Cube with Rigidbody + MeshRenderer; **Count 1000**. Press **H** in play mode → **High-density scenario**.
4. Create **Heavy-Effects scenario** (`Level1_Effects` or enable at runtime): add 8–10 *real-time Point/Spot lights with Soft shadows*, Particle System (Max Particles 5000, Emission 500), and Post-processing (Volume: Bloom, Depth of Field, SSAO).
5. Open **Profiler**: `Window → Analysis → Profiler`. Enable CPU Usage, GPU Usage (Add Profiler → GPU), Memory, Rendering, Physics. Use **Development Build + Autoconnect Profiler** for accurate numbers, or Play in Editor (note that editor overhead is included). Also use **Stats** (Game view top-right) and **Frame Debugger** (`Window → Analysis → Frame Debugger`) if wanted.
6. How to read values:
   - **CPU ms** (frame time) = CPU Usage module → click a frame → *Timeline/Hierarchy* → top "CPU:" ms. Usage % ≈ CPU ms ÷ frame budget (16.6 ms for 60 FPS) × 100.
   - **GPU ms** from GPU Usage module (if unsupported on your GPU, use Stats "Render thread"/ `Frame Debugger`; state this honestly).
   - **Memory:** Memory Profiler module → *Total Reserved / Used*, *GC Used Memory*; or `PerformanceLogger` value.
   - **FPS** from Stats / `PerformanceLogger`.
   - Record **average over ~10 s**, same resolution each time.

### 5.2 Measurement — BEFORE optimization
📸 **SCREENSHOT 5.1** – Profiler: Main Menu.
📸 **SCREENSHOT 5.2** – Profiler: Normal gameplay.
📸 **SCREENSHOT 5.3** – Profiler: High-density (after pressing H).
📸 **SCREENSHOT 5.4** – Profiler (GPU module): Heavy effects.
📸 **SCREENSHOT 5.5** – Memory profiler/Memory module at different stages.

**Table 1 – Before optimization** *(fill with YOUR measured values; example formatting only)*
| Scenario | CPU Usage (ms / %) | GPU Usage (ms / %) | Memory Usage (MB) | FPS |
|---|---|---|---|---|
| Main Menu | | | | |
| Normal Gameplay | | | | |
| High-Density Scene | | | | |
| Cutscene / Heavy Effects | | | | |

### 5.3 Identify ≥3 bottlenecks (examples – confirm with your own profiler)
| # | Bottleneck | How to find it | Optimization |
|---|---|---|---|
| 1 | **1000+ physics objects** → high `Physics.Simulate` time / many active Rigidbodies | Profiler → CPU → *Physics.Simulate*, Physics module "Active Dynamic Bodies" | Use **object pooling** (`OptimizedSpawner.cs`), cap active objects, set Rigidbodies to **sleep**, simplify colliders (Box instead of Mesh), use Layer Collision Matrix (`Project Settings → Physics`), raise Fixed Timestep to 0.03 |
| 2 | **Many real-time shadowed lights** → huge GPU time & draw calls (SetPass) | GPU module, Frame Debugger shows many shadow passes | Reduce lights, set most to **Baked/Mixed**, Shadows = None on small lights, lower Shadow Distance (URP asset) & shadow resolution, cascade count 2 |
| 3 | **Large uncompressed textures** → high memory | Memory Profiler → Textures | Set Max Size (e.g. 4096 → 1024), enable **Compression** (Normal quality), disable *Read/Write*, generate mip maps; Terrain base-map resolution lower |
| 4 | **Too many draw calls** (separate materials/meshes) | Rendering module "Batches / SetPass calls" | Enable **GPU Instancing** on materials, **Static Batching** (tick *Static* on non-moving objects), SRP Batcher on, share materials |
| 5 | **Script cost / GC allocations** (e.g. `Find`, `GetComponent` in `Update`, string concat) | CPU Hierarchy → *GC Alloc* column | Cache references in `Awake/Start`, avoid per-frame allocations (our scripts already cache `CharacterController`) |
| 6 | **Post-processing & particles** | GPU time | Reduce particle count, disable DoF/SSAO, lower render scale (URP Asset → Render Scale 0.8) |

Implement at least three (recommended: **#1 pooling/physics, #2 lights/shadows, #3 textures**, and optionally #4). For each:
📸 **SCREENSHOT 5.6** – Before setting (e.g. shadow settings / texture size / 1000 rigidbodies profiler spike).
📸 **SCREENSHOT 5.7** – After setting (changed Inspector values).

**Optimization step log (fill in):**
1. ______________________ — change made: ______ — expected effect: ______
2. ______________________
3. ______________________

### 5.4 Measurement — AFTER optimization
📸 **SCREENSHOT 5.8** – Profiler: Normal gameplay (after).
📸 **SCREENSHOT 5.9** – Profiler: High-density (after).
📸 **SCREENSHOT 5.10** – Profiler: Heavy effects (after).

**Table 2 – After optimization**
| Scenario | CPU Usage (ms / %) | GPU Usage (ms / %) | Memory Usage (MB) | FPS |
|---|---|---|---|---|
| Main Menu | | | | |
| Normal Gameplay | | | | |
| High-Density Scene | | | | |
| Cutscene / Heavy Effects | | | | |

**Table 3 – Before vs After comparison**
| Scenario | FPS before → after | CPU ms before → after | GPU ms before → after | Memory before → after | Improvement % |
|---|---|---|---|---|---|
| Normal Gameplay | | | | | |
| High-Density Scene | | | | | |
| Heavy Effects | | | | | |

> Optional: create a bar-chart in Excel of FPS before vs after and paste it. Improvement % = (After − Before) ÷ Before × 100 for FPS; (Before − After) ÷ Before × 100 for ms/MB.

### 5.5 Conclusion
Write 5–6 lines: which bottleneck mattered most, what gave the biggest gain, trade-offs (e.g. lower shadow quality, compressed textures), and what you would do next.

______________________________________

---

# Appendix A — Script index (all in `Scripts/`)
| Script | Used in | Purpose |
|---|---|---|
| `PlayerController.cs` | Q2, Q3 | Input System movement, run, crouch, jump, acceleration, animator params, footstep/jump audio |
| `SmoothCameraFollow.cs` | Q3 | SmoothDamp third-person camera |
| `PlayerHealth.cs` | Q4 | Health, restart on death |
| `DangerousObstacle.cs` | Q4 | Damage on trigger/collision with cooldown |
| `MovingObstacle.cs` | Q4 | Kinematic Rigidbody moving hazard |
| `InteractiveButton.cs` | Q4 | Press E to open a door |
| `GoalTrigger.cs` | Q4 | Goal → load next scene |
| `SceneLoader.cs`, `MainMenu.cs` | Q4, Q5 | Scene management / menu buttons |
| `PerformanceLogger.cs` | Q5 | On-screen FPS / memory / draw calls |
| `ObjectSpawner.cs` | Q5 | High-density test (before) |
| `OptimizedSpawner.cs` | Q5 | Pooled version (after) |

# Appendix B — Submission checklist
- [ ] Unity project (zip **without** `Library/`, `Temp/`, `Logs/` folders, or push to Git)
- [ ] This lab file (export to PDF/Word) with all screenshots pasted
- [ ] Scripts folder
- [ ] Demo video for Q3 (and optionally Q2/Q4)
- [ ] Be ready to explain: material vs shader, Humanoid avatar, Input System actions, `SmoothDamp`, trigger vs collision, Profiler findings

# Appendix C — Common problems
| Problem | Fix |
|---|---|
| `InvalidOperationException … Input System` | Project Settings → Player → Active Input Handling → *Input System Package* |
| Player doesn’t animate | Check Animator Controller assigned, parameter names exactly `Speed`, `IsGrounded`, `IsCrouching`, `Jump` |
| Triggers don’t fire | One object needs a Rigidbody (or CharacterController), collider **Is Trigger**, tag = `Player` |
| Scene won’t load | Add scene to Build Settings, name matches exactly |
| Pink materials | Convert to URP: `Edit → Rendering → Materials → Convert Selected Built-in Materials to URP` |
| Character sinks/floats | Adjust Character Controller *Center/Skin Width*; Animator *Apply Root Motion* off |
