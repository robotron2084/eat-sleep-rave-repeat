# Camera Package
The camera package contains useful utilities and components designed to help with enhancing your camera in a small scale game.
# How to add to your project
### 2019.3 or higher
`Window > Package Manager > + (top left button) > add package from git URL...`
 
if you do not already have GameJamStarterKit.Core installed

paste `https://gitlab.com/ASeward/gamejamstarterkit.git#core`

then paste `https://gitlab.com/ASeward/gamejamstarterkit.git#camera`

### 2019.2 or less
In your projects root directory (where the assets folder is) open `Packages > manifest.json`

**remove the core package line if you've already installed the core package.**

add this under `dependencies {`

```
"com.aseward.game-jam.starter-kit.core": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#core",
"com.aseward.game-jam-starter-kit.camera": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#camera"
```

so your manifest.json should look roughly like

```json5
{
     "dependencies": {
        "com.aseward.game-jam.starter-kit.core": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#core",
        "com.aseward.game-jam-starter-kit.camera": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#camera",
        /// other dependencies
     } 
}
```