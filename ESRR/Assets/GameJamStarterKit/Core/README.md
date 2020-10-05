# Core Package
Core functionality for Game Jam Starter Kit. All modules depend on this, comes with many useful extensions, components, and other goodies.
# How to add to your project
### 2019.3 or higher
`Window > Package Manager > + (top left button) > add package from git URL...` 

paste `https://gitlab.com/ASeward/gamejamstarterkit.git#core`

### 2019.2 or less
In your projects root directory (where the assets folder is) open `Packages > manifest.json`

add this line under `dependencies {`

`"com.aseward.game-jam-starter-kit.core": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#core",`

so your manifest.json should look roughly like

```json5
{
     "dependencies": {
       "com.aseward.game-jam-starter-kit.core": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#core",
        /// other dependencies
     } 
}
```