# Health System Package
Health system is a simple and easy way to implement health for game objects. Includes events, useful extension methods, and pre-made UI progress bars.
# How to add to your project
### 2019.3 or higher
`Window > Package Manager > + (top left button) > add package from git URL...` 

paste `https://gitlab.com/ASeward/gamejamstarterkit.git#health_system`

### 2019.2 or less
In your projects root directory (where the assets folder is) open `Packages > manifest.json`

**remove the core package line if you've already installed the core package.**

add this under `dependencies {`

```
"com.aseward.game-jam.starter-kit.core": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#core",
"com.aseward.game-jam-starter-kit.healthsystem": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#health_system",
```

so your manifest.json should look roughly like

```json5
{
     "dependencies": {
        "com.aseward.game-jam.starter-kit.core": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#core",
        "com.aseward.game-jam-starter-kit.healthsystem": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#health_system",
        /// other dependencies
     } 
}
```