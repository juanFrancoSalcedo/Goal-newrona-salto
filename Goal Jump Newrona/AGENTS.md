# AGENTS.md - Goal Jump Newrona

## Overview
This is a Unity 6000.3.6f1 C# project (Universal Render Pipeline). No custom code exists yet.

---

## Build / Run / Test Commands

### Unity Editor
- **Open project**: Open the folder in Unity Hub or run `unity -projectPath .`
- **Build**: `Unity -batchmode -projectPath . -buildTarget <target> -buildPlayer <path>`
- **Run tests**: Window > General > Test Runner > Run All

### Running a Single Test (from command line)
```bash
# PlayMode test
unity -batchmode -projectPath . -runTests -testPlatform playmode -testAssemblyNames "Assembly-CSharp" -testNames "Namespace.ClassName.TestMethodName"

# EditMode test
unity -batchmode -projectPath . -runTests -testPlatform editmode -testAssemblyNames "Assembly-CSharp" -testNames "Namespace.ClassName.TestMethodName"
```

### Unity CLI for CI/CD
```bash
unity -batchmode -projectPath . -executeMethod UnityEditor.BuildPlayer.BuildPlayer
```

---

## Project Structure

```
Assets/
├── Art/
│   ├── 2D/
│   │   ├── UI/
│   │   ├── Sprites/
│   │   └── Texture/
│   └── 3D/
│       ├── Models/
│       └── Materials/
├── Code/
│   ├── Scripts/
│   │   ├── Features/      # Game-specific mechanics (Player, Combat, etc.)
│   │   ├── Services/      # Singletons & globals (AudioManager, DataManager)
│   │   ├── Utils/         # Helpers, extensions, constants
│   │   └── Installers/    # DI configurations (Zenject)
│   ├── SO/                # ScriptableObject instances
│   └── Shaders/           # HLSL, Shader Graphs
├── Level/
│   ├── Prefabs/
│   └── Scenes/
├── ExternalAssets/        # Third-party code (DO NOT MODIFY)
└── Resources/             # Dynamic loading only
```

**Rule**: Never create new folders at `Assets/` root without approval.

---

## Code Style Guidelines

### Naming Conventions
| Element | Convention | Example |
|---------|-----------|---------|
| Class/Struct | PascalCase | `PlayerController` |
| Method | PascalCase | `OnJumpPressed()` |
| Private field | _camelCase | `_currentSpeed` |
| Public field | PascalCase | `MaxHealth` |
| Constant | PascalCase | `MaxJumpCount` |
| Interface | IPascalCase | `IDamageable` |
| Enum values | PascalCase | `JumpState.Airborne` |
| Namespace | PascalCase | `Features.Player` |

### C# Conventions
- Use **full type names** (e.g., `Int32`, `Single`) over aliases only where clarity improves
- Prefer `readonly` fields; use `const` for compile-time constants
- Use **expression-bodied members** for simple getters: `public int Value => _value;`
- Use **file-scoped namespaces** when targeting .NET 3.5+
- **Properties over public fields** (auto-properties are fine)

### Imports / Using Statements
- Place `using` statements at top, sorted alphabetically
- Group with blank line between Unity, System, and third-party namespaces
- Remove unused imports before committing

### Formatting
- **Indentation**: 4 spaces (verify .editorconfig if present)
- **Braces**: K&R style (opening brace on same line)
- **Line length**: Max ~120 characters (wrap if needed)
- **Blank lines**: One blank line between methods, two between class regions
- **Regions**: Use sparingly (`#region`/`#endregion`); prefer logical class organization

### Unity-Specific
- **MonoBehaviour lifecycle**: Follow order: `Awake`, `OnEnable`, `Start`, `Update`, `LateUpdate`, `OnDisable`, `OnDestroy`
- **SerializeField**: Use `[SerializeField]` on private fields that need inspector exposure
- **GetComponent**: Cache references; use generic version `GetComponent<T>()`
- **null checks**: Use null-conditional operators (`?.`) and null-coalescing (`??`)
- **Coroutines**: Name them descriptively; document expected duration
- **DontDestroyOnLoad**: Only on root GameObjects or explicitly intended objects

### Error Handling
- Use `try/catch` for external operations (file I/O, network)
- Log errors with context: `Debug.LogError($"[Player] Failed to load data: {e.Message}")`
- Use `[ContextMenu]` for debug/test methods on components
- Prefer exceptions for unrecoverable states; return success bool for recoverable failures
- Never swallow exceptions silently

### Async / Task Patterns
```csharp
// Prefer async/await over raw coroutines for long operations
public async Task LoadSceneAsync(string sceneName)
{
    var op = SceneManager.LoadSceneAsync(sceneName);
    await op;
    OnSceneLoaded?.Invoke();
}
```

### Dependency Injection (Zenject)
- Use constructor injection for mandatory dependencies
- Use `[Inject]` attribute for runtime-optional dependencies
- Keep installers focused; one responsibility per installer

---

## ScriptableObjects
- Create `.asset` files in `Assets/Code/SO/` for configuration data
- Use SO for shared data referenced by multiple systems
- Document expected values and ranges in class XML docs

---

## Performance Tips
- Avoid `FindObjectsOfType` at runtime; use events or registries
- Use `struct` for small, immutable data passed frequently
- Pool objects that are frequently created/destroyed
- Mark methods that don't need to run every frame with `[ContextMenu]` or conditional compilation

---

## Testing
- Tests go in `Assets/Code/Scripts/Tests/` (or `Tests/` assembly if separated)
- Use `[UnityTest]` for tests that need play mode
- Mock Unity services with interfaces for unit testability
- Test naming: `MethodName_State_ExpectedResult`

---

## Git Workflow
- **Commits**: Use conventional commits: `feat:`, `fix:`, `refactor:`, `docs:`
- **Branches**: `feature/`, `fix/`, `refactor/` prefixes
- **PRs**: Include test coverage for new features
- Never commit secrets, API keys, or credentials

---

## References
- Unity Docs: https://docs.unity3d.com/
- C# Coding Conventions: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions