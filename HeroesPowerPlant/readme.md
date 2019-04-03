# Heroes Power Plant v0.7
## Viewport
* In the viewport, you can see a 3D model of your level.
* Textures are not loaded from TXD files. If you want textures to display, you must put them in PNG format in a folder with the same name of the TXD in the program's directory (for example, \Textures\s01\). If textures are not found, the model will be displayed blank or glitchy. Textures in the Common folder will always be loaded; you can place object and enemy textures here. You can extract textures using Magic.TXD.

### Keyboard view controls:
* W, A, S, D: move view forward, left, backward, right
* Shift + (W, S): move view up, down
* Ctrl + (W, A, S, D): rotate view up, left, down, right
* Q, E: decrease interval, increase interval
* R: reset view
* Z: toggle mouse mode*

*Mouse mode: similar to a first person camera. The view rotates automatically as you move the mouse. Use the keyboard to move around.

### Mouse controls:
* Left click to select an object or camera
* Mouse wheel to move forward/backward
* Right click and drag to pan
* Middle click and drag to rotate view

## Project:
A project JSON is Heroes Power Plant's way of saving a group of file paths and settings - this way, when you're editing your level, you don't have to open all your files individually again. When you save a Project, the following data is saved and reapplied when opened:
* Paths for the Config, Level, Visibility, Collision, Layout, Camera, Particle, Pattern and SET ID Table editors
* TXD files, texture folders and object ONEs
* No culling, wireframe, background and selection colors
* Which items are being displayed (on/off)
* Current view position, draw distance and FOV
Note: saving the project file will not save the individual editors' files! You must save them individually.

## Resources:
In the resources tab, you can open or clear the currently loaded textures and object ONEs.
* Textures: you can load those from either a TXD file, a ONE archive which contains TXD files, or a folder containing PNGs. You can load any number of files; if textures with the same names are found, they will be overwritten.
* Object ONEs: pick ONE files containing (mostly) DFF models, such as a s**_obj.one. This will allow the Layout Editor to display some objects with their actual models instead of representing them by small cubes. TXD files inside the ONEs will be imported automatically.

## Options (and their shortcut keys):
* No culling (C): toggles backface culling.
* Wireframe (F): toggles wireframe mode.
* Colors:
	* Background Color: allows you to choose a new background color for the view.
	* Selection Color: allows you to choose a new tint for selected objects.
	* Reset: reset to default colors.

* Mouse mode:
	* Objects: click to select layout objects.
	* Cameras: click to select camera triggers.
* Start Pos (Y): toggles display of Start/End/Bragging positions from the Config Editor.
* Splines (U): toggles display of splines from the Spline Editor.
* Render By Chunk (H): toggles between display of entire level or just current chunk from the Level/Visibility Editor.
* Chunk Boxes (B): toggles display of chunk boundaries.
* Collision (X): toggles between display of the level model and the collision model.
* Quadtree (T): toggles display of the quadtree from the Collision Editor.
* Objects (G): toggles display of objects from the Layout Editor. There are 3 settings: off will display no objects, indeterminate will display objects in render distance and on will display all objects.
* Cameras (V): toggles display of camera triggers from the Camera Editor. Note that camera and target positions will only display for the currently selected camera.

* VSync: toggles frame limiter (wanna see how much FPS you can get with the entire level being rendered?)
* Auto-Load Last Project on Launch: if this is on, the last project which was opened in Heroes Power Plant when it was last closed will be opened along with the program.
* Auto-Save Project on Closing: if this is on, Heroes Power Plant will automatically save the Project file when closing, if one is open.
If these two last settings are on, you can close the program and when you open it again it will be showing the exact same it was before.

## View Config (F1)
Press F1 or click the status bar to display the view config window. Here you can set the view's position, rotation, movement speed (interval), draw distance, field of view and height to display the quadtree.

## Mod Loader Config Editor (F2)
* This editor allows you to create a new or open an existing Stage.json configuration file for the default Reloaded Mod Loader Generic Stage Injection Mod.
* You can choose a level preset (required for the correct spline and position pointers to be saved) and edit start positions, ending positions and bragging positions for all teams. You can get the original positions from the EXE using Heroes Tweaker.
* Note that splines will not work on levels which did not previously contain them.
* You can open Config.cc files (legacy Heroes Mod Loader Generic Stage Injection Mod) as well, but not save them.

## Level Editor (F3)
* For Heroes: this tool allows you to create new, open and edit .ONE level model archives and *_blk.bin visibility files.
* For Shadow: this tool allows you to create new, open and edit stage folders which contain level models and collision files.
Note that the level editor only works on the PC, XBOX and GameCube versions of Heroes and Shadow; it can't display or extract the models from the PS2 versions, but can import new ones for it.

### Level Models:
* You can import DAE, OBJ, or raw BSP. The model must use triangular faces and must contain vertex position and texture coordinate data; vertex normals will be ignored and vertex colors are optional for .DAE files. Your model must contain only one channel of each data type (vertex position, texture coordinate, vertex colors and normals), otherwise the import will fail.
* You can export one or all of the BSPs as OBJ as well. The exported OBJs contain vertices, triangles, texture coordinates and vertex colors; since OBJ traditionally has no support for vertex colors, you can use the script located in the Tools folder to import these files to 3ds Max with vertex colors. Otherwise, they will be ignored. The material library expects PNG files in the same folder as the models (you can extract these with Magic.TXD).
* You can double click on a BSP to rename it. This will automatically set the new material flag and chunk number. If it cannot be detected, a default one will be set. The BSP name will not be fixed for you, so name it properly.
* Heroes Power Plant's rendering is not always accurate to ingame. Always test your models ingame.

### Visibility Editor:
* The visibility editor allows you to edit the chunk boundaries *_blk.bin file.
* You can edit each chunk entry's chunk number (you can have multiple entries enabling the same chunk) and bounding box values.
* Make sure all maximum values are higher than the corresponding minimum.
* The AutoChunk function will automatically set chunk boundaries based on the extremities of the BSPs with the same number. The value you can set here is an offset for the boundaries (will be added to positive values and subtracted from negative ones).
* If you open a ONE archive, it'll try to open the *_blk.bin automatically.

### Shadow Collision Editor:
The Shadow collision editor allows you to import OBJ files for Shadow collision, which will be converted to BSP.
* Append one of the following letters to the material names (not mesh names, not texture names!) in your 3D model editor to set up collision flags:
	* _a - angled wall
	* _c - ceiling
	* _f - normal floor/wall
	* _fm - metal floor/wall
	* _fs - stone floor/wall
	* _g - green goo
	* _i - invisible wall
	* _i2 - another invisible wall
	* _k - protection barrier (ground-only wall)
	* _m - metal floor
	* _t - triangle jumpable wall
	* _x - death collision
* You can double click on a BSP to rename it.

## Collision Editor (F4)
The collision editor allows you to import an OBJ file, which will be converted to CL.
* New: lets you choose an OBJ file to import and a new CL file to save as.
* Open: opens a CL file and allows you to display or import an OBJ over it.
* Close: closes the CL file so you can't display or edit it anymore.
* Import OBJ: lets you choose an OBJ file to import and overwrite the current CL.
*  You can choose the maximum depth of the quadtree or let it be chosen automatically (I recommend setting it to 5).
*  Append one of the following letters to the mesh names (not material names, not texture names!) in your 3D model editor to set up collision flags:
	* _a - water collision (for _wt.CL)
	* _b - bingo slide
	* _i - invisible wall
	* _k - protection barrier (ground-only wall)
	* _l - slippery wall
	* _p - pinball Table
	* _s - stairs
	* _t - triangle jumpable wall
	* _w - wall
	* _x - death collision (for _xx.CL)
* Note: if your collision model appears completely black after importing, just open the file again and it should display correctly.
* If you're looking at _xx (death) or _wt (water) models, normals are reversed, so you should disable culling as the models will show up upside down.
* To import _xx or _wt, append the appropriate flag to the mesh names, check "flip normals" and set the quadtree depth to 1 or 2 before importing.

## Layout Editor (F5)
The object layout editor allows you to add, remove and modify objects.
* You can open s*_*.bin files (from Heroes) or stg*_*.dat (from Shadow) or create new ones.
* Objects will mostly be displayed as small cubes. You can click on the objects to select them.
* To render using their actual models, you must add the ONE archive which contains the DFF to the object ONEs (explained above). This works for Heroes and Shadow files.
* Many objects have documented misc. settings and thus will allow you to use a pretty editor instead of the generic one. If you want an object's misc. settings added to Heroes Power Plant, tell me and I'll try to add it.
* If SONIC HEROES(TM) is running, you can get values from ingame or teleport yourself to an object's location (even if you're editing a Shadow layout!)
* You can export and import the objects with their information to an INI file and import objects from another file without overwriting the currently open one, and OBJ files (in this case, a ring will be placed in each vertex, this is useful for importing lines of rings).
* You can take a look at the Object Editing pages in Sonic Retro to understand a bit more about this.

## Spline Editor (F6)
The spline editor allows you to view, choose the type of, delete and import new splines from OBJ files.
* The spline editor is reliant on the Mod Loader Config Editor, and thus will only show up or allow you to save the splines if an existing CC or JSON is open in it.
* Splines will be loaded from OBJ files in the Splines folder located in the same directory as your CC or JSON file (a new one will be created if it doesn't exist).
* When importing, make sure your OBJ file contains only one spline.
* There is no way to display or edit splines straight from the game's EXE. You can extract them from the EXE using Heroes Tweaker.
* If no splines are present, a null one will be created.

## Camera Editor (F7)
The camera layout editor allows you to add, remove and modify cameras.
* You can open s*_cam.bin files or create new ones.
* Camera triggers will be displayed as their shape; you can click on the triggers to select them as long as the mouse mode is set to Camera.
* For the selected camera, the camera location will be displayed as a pink cube and points A, B and C will be displayed as red, green and blue cubes.
* Note that we don't know 100% about cameras yet, but if you take a look at the original camera files and the information on Sonic Retro you might be able to understand a bit of it.
* Know something we don't? Tell us! Don't keep it to yourself.

## Particle Editor (F8)
The particle effect editor allows you to add, remove and modify particle entries.
* You can open s*_ptcl.bin files or create new ones.
* The selected particle entry's properties will be displayed and editable in the property box. Not everything about them is known yet, and some attributes might be wrongly labeled.
* Particles can be emitted by the layout object Particle Emitter (01 FF). The reference in the object is offset by 50, so particle number 50 in the object will be number 0 in the ptcl.
* Particles are referenced by certain objects by their index, so if you remove entries, you might break certain objects.
* Know something we don't? Tell us! Don't keep it to yourself.

## Texture Pattern Editor (F9)
The texture pattern editor allows you to add, remove and modify texture pattern entries.
* You can open s*_.txc files or create new ones. Each file can have a number of pattern entries. You can also play the animations and preview them.
* Each pattern needs, in the TXD file for the level, a group of textures following a name such as text_animation.n, in which each frame of the animation has a different number n starting from 1.
* The pattern's texture name is the name of the texture that will be replaced by the animation. The texture with this name will technically not show up ingame. It can be the name of one of the textures in the animation, but this is optional.
* The animation name is the name of the textures that compose the animation without the number at the end. So, if in the TXD your images are named text_animation.n (with n going from 1 to 8), this field will have "text_animation" in it.
* The frame count is the total lenght of the animation. Key frames with an offset higher than this will be ignored.
* The key frames are the frames in which the image changes. You don't need to change it every frame.
* The texture number is the number of the texture that will be set in that frame (the number after the dot at the end of the image name).
* You can have multiple entries (with different texture names) which use the same images (same animation name), but with different frame counts and key frames (such as an animation which plays in a different speed, starts on a different frame, or even a different frame order).
* Note: the animations are only playing at the correct speed if the FPS is 60.

## Light Editor (F10)
The light editor allows you to add, remove and modify light entries.
* You can open s*_light.bin files or create new ones.
* The selected light entry's properties will be displayed and editable in the property box. Not everything about them is known yet, and some attributes might be wrongly labeled.
* Know something we don't? Tell us! Don't keep it to yourself.

## SET ID Table Editor
The SET ID Table Editor allows you to edit the object avaliability setidtbl.bin (Heroes) or setid.bin (Shadow) file.
* You can open an existing SET ID Table file, but you can't create a new one. Object entries cannot be added or removed from the file either.
* You can, hoewever, choose in which stages that specific layout object will be available for placement.
* If you wish to use a layout object in a level, it must be enabled for that level, otherwise the game will crash.
* Note that enabling an object in the SET ID Table Editor is not enough for it to work ingame. All of the object's models and animations must be present in the s**obj.one file, and in many cases specific textures are also needed.
* Also, not all objects will work properly in level slots they were not intended to. They might use different sounds, particle effects, or simply not work at all.
* The AutoLevel function will get the object entries from the currently opened Layout Editor file and apply them as available on the stage you have selected on the box. Use this with caution, as you might enable an object and forget to put its required files in the s*obj.one.