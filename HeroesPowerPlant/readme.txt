	= Heroes Power Plant Release 6 BETA =
Please read all of this if you have no previous experience with Sonic Heroes/Shadow the Hedgehog level editing!

	= Viewport =
- In the viewport, you can see a 3D model of your level.
- Textures are not loaded from TXD files. If you want textures to display, you must put them in PNG format in a folder with the same name of the TXD in the program's directory (for example, \Textures\s01\). If textures are not found, the model will be displayed blank or glitchy. Textures in the Common folder will always be loaded; you can place object and enemy textures here. You can extract textures using Magic.TXD.

	Keyboard view controls:
- W, A, S, D: move view forward, left, backward, right
- Shift + (W, S): move view up, down
- Ctrl + (W, A, S, D): rotate view up, left, down, right
- Q, E: decrease interval, increase interval
- R: reset view
- Z: toggle mouse mode*

	Mouse controls:
- Left click to select an object or camera
- Mouse wheel to move forward/backward
- Right click and drag to pan
- Middle click and drag to rotate view

*Mouse mode: similar to a first person camera. The view rotates automatically as you move the mouse. Use the keyboard to move around.

	Project:
A project INI is Heroes Power Plant's way of saving a group of file paths - this way, when you're editing your level, you don't have to open all your files individually again. In the Project INI, the paths for the Config Editor, Level Editor, Visibility Editor, Collision Editor, Layout Editor, Camera Editor and object ONE files are saved. Note that saving a project does *not* save the files for the individual editors, as they are independent of each other. Be aware of this so you don't lose your work.

	Object ONEs:
The options to add and clear object ONEs let you pick ONE files containing (mostly) DFF models, such as a s**_obj.one. This will allow the Layout Editor to display some objects with their actual models instead of representing them by small cubes.

	Options:
- No culling (C): toggles backface culling
- Wireframe (F): toggles wireframe mode
- Background color: allows you to choose a new background color for the view
- Selection color: allows you to choose a new tint for selected objects
- Start Pos (Y): toggles display of Start/End/Bragging positions from the Config Editor.
- Splines (U): toggles display of splines from the Spline Editor.
- Render By Chunk (H): toggles between display of entire level or just current chunk from the Level/Visibility Editor.
- Chunk Boxes (B): toggles display of chunk boundaries.
- Collision (X): toggles between display of the level model and the collision model.
- Quadtree (T): toggles display of the quadtree from the Collision Editor.
- Objects (G): toggles display of objects from the Layout Editor. There are 3 settings: off will display no objects, indeterminate will display objects in render distance and on will display all objects.
- Cameras (V): toggles display of camera triggers from the Camera Editor. Note that camera and target positions will only display for the currently selected camera.
- Graphics Mode: toggles between "Fast" and "Nice" graphic display modes ("Nice" is default)
- VSync: toggles frame limiter (wanna see how much FPS you can get with the entire level being rendered?)

	= View Config (F1) =
Press F1 or click the status bar to display the view config window. Here you can set the view's position, rotation and a few other settings.

	= Mod Loader Config Editor (F2) =
- This editor allows you to create a new or open an existing Config.CC configuration file for the default Heroes Mod Loader Generic Stage Injection Mod DLL.
- You can choose a level preset (required for the correct spline and position pointers to be saved) and edit start positions, ending positions and bragging positions for all teams (depending on which stage they are available for). You can get the original positions from the EXE using Heroes Tweaker. Note that not all level presets include spline pointers.
- You can edit the Config.cc file in a text editor if you want to (and remove the spline pointer to leave them untouched, for example).

	= Level Editor (F3) =
- For Heroes: this tool allows you to create new, open and edit .ONE level model archives and *_blk.bin visibility files.
- For Shadow: this tool allows you to create new, open and edit stage folders which contain level models and collision files.
Note that the level editor only works on the PC, XBOX and GameCube versions of Heroes and Shadow; it can't display or extract the models from the PS2 versions, but it can import new ones for it.

	Level Models:
- You can import DAE, OBJ, or raw BSP. The model must use triangular faces and must contain vertex position and texture coordinate data; vertex normals will be ignored and vertex colors are optional for .DAE files. Your model must contain only one channel of each data type (vertex position, texture coordinate, vertex colors and normals), otherwise the import will fail.
- You can export one or all of the BSPs as OBJ as well. The exported OBJs contain vertices, triangles, texture coordinates and vertex colors; since OBJ traditionally has no support for vertex colors, you can use the script located in the Tools folder to import these files to 3ds Max with vertex colors. Otherwise, they will be ignored. The material library expects PNG files in the same folder as the models (you can extract these with Magic.TXD).
- You can double click on a BSP to rename it.

	Visibility Editor:
- The visibility editor allows you to edit the chunk boundaries *_blk.bin file.
- Make sure all maximum values are higher than the corresponding minimum.
- The AutoChunk function will automatically set chunk boundaries based on the extremities of the BSPs with the same number. The value you can set here is an offset to be added to the boundaries.
- If you open a ONE archive, it'll try to open the *_blk.bin automatically.

	Shadow Collision Editor:
The Shadow collision editor allows you to import OBJ files for Shadow collision, which will be converted to BSP.
- Append one of the following letters to the material names (not mesh names, not texture names!) in your 3D model editor to set up collision flags:
 _a - angled wall
 _c - ceiling
 _f - normal floor/wall
 _fm - metal floor/wall
 _fs - stone floor/wall
 _g - green goo
 _i - invisible wall
 _i2 - another invisible wall
 _k - protection barrier (ground-only wall)
 _m - metal floor
 _t - triangle jumpable wall
 _x - death collision
- You can double click on a BSP to rename it.

	= Collision Editor (F4) =
The collision editor allows you to import an OBJ file, which will be converted to CL.
- New: lets you choose an OBJ file to import and a new CL file to save as.
- Open: opens a CL file and allows you to display or import an OBJ over it.
- Close: closes the CL file so you can't display or edit it anymore.
- Import OBJ: lets you choose an OBJ file to import and overwrite the current CL.
-- You can choose the maximum depth of the quadtree or let it be chosen automatically.
-- If you're looking at _xx (death) or _wt (water) models, normals are reversed, so you should disable culling as the models will show up upside down. Make sure to check "flip normals" when importing.
-- Append one of the following letters to the mesh names (not material names, not texture names!) in your 3D model editor to set up collision flags:
 _a - water collision (for _wt.CL)
 _b - bingo slide
 _i - invisible wall
 _k - protection barrier (ground-only wall)
 _l - slippery wall
 _p - pinball Table
 _s - stairs
 _t - triangle jumpable wall
 _w - wall
 _x - death collision (for _xx.CL)
- Note: if your collision model appears completely black after importing, just open the file again and it should display correctly.

	= Layout Editor (F5) =
The object layout editor allows you to add, remove and modify objects.
Note: currently, adding new objects (or changing their types) to Shadow layouts is unsupported. You can try doing so, but the file certainly will not work ingame. Deleting, copying and editing existing ones is fine though.
- You can open s*_*.bin files (from Heroes) or stg*_*.dat (from Shadow) or create new ones.
- Objects will be displayed as small cubes. You can click on the objects to select them.
- To render using their actual models, you must add the ONE which contains the DFF to the object ONEs (explained above).
- If SONIC HEROES(TM) is running, you can get values from ingame or teleport yourself to an object's location (even for Shadow layouts!)
- You can export and import the objects with their information to an INI file and import objects from another file without overwriting the currently open one, and OBJ files (in this case, a ring will be placed in each vertex, this is useful for importing lines of rings).
- You can take a look at the Object Editing pages in Sonic Retro to understand a bit more about this.

	= Spline Editor (F6) =
The spline editor allows you to view, choose the type of, delete and import new splines from OBJ files.
- Splines will be loaded from OBJ files in the Splines folder located in the same directory as your CC file (a new one will be created if it doesn't exist).
- When importing, make sure your OBJ file contains only one spline.
- The spline editor is reliant on the Mod Loader Config Editor, and thus will only show up or allow you to save the splines if an existing CC is open in it.
- There is no way to display or edit splines straight from the game's EXE. You can extract them from the EXE using Heroes Tweaker.
- If no splines are present, a null one will be created.

	= Camera Editor (F7) =
The camera editor is currently unavailable. It has been replaced with a live fire course for military androids. We apologize for the inconvenience and wish you the best of luck.

But, if there were a camera editor, it would be like this:
The camera layout editor allows you to add, remove and modify cameras.
- You can open s*_cam.bin files or create new ones.
- Camera triggers will be displayed as their shape; you can click on the center of the triggers to select them.
- For the selected camera, the camera location will be displayed as a pink cube and points A, B and C will be displayed as red, green and blue cubes (as long as they're not at the origin).
- Note that we don't know 100% about cameras yet, but if you take a look at the original camera files and the information on Sonic Retro you might be able to understand a bit of it.
- Know something we don't? Tell us! Don't keep it to yourself.