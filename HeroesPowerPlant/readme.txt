= Heroes Power Plant Release 5 =
Please read all of this if you have no previous experience with Sonic Heroes level editing!

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

= Viewport =
- In the viewport, you can see a 3D model of your level.
- Textures are not loaded from TXD files. If you want textures to display, you must put them in PNG format in a folder with the same name of the TXD in the program's directory (for example, \Resources\s01\). If textures are not found, the model will be displayed blank or glitchy. Textures in the CommonTextures folder will always be loaded; you can place object and enemy textures here. You can extract textures using Magic.TXD.
- Currently, Final Fortress (s14) and Metal Madness (s27) models will display and export with glitchy texture coordinates and vertex colors.

= View Config (F1) =
Press F1 to display the view config window. Here you can view and edit a few general setting for the view, such as draw distance and field of view.
In the view config, you can also open and save a project INI file: this file contains the list of all files currently open in Heroes Power Plant, so you can easily load the files for each editor from it. It also includes the list of ONE files to read object models from.

= Mod Loader Config Editor (F2) =
- This editor allows you to create a new or open an existing Config.CC configuration file for the default mod loader custom stage DLL.
- You can choose a level preset (required for the correct spline and position pointers to be saved) and edit start positions, ending positions and bragging positions for all teams (depending on which stage they are available for). There is no way to display or edit positions straight from the game's EXE. Note that not all level presets include spline pointers.
- You can edit the Config.cc file in a text editor if you want to (and remove the spline pointer to leave them untouched, for example).

= Level Editor (F3) =
- This tool allows you to create new, open and edit .ONE archives and *_blk.bin files.

Level Models:
- You can import DAE, OBJ, or raw BSP. The model must use triangular faces and must contain vertex position and texture coordinate data; vertex normals will be ignored and vertex colors are optional for .DAE files. The models being displayed will only update when you save.
- You can also import any other file, since you can put anything in a ONE. Only BSP models will be rendered, though.
- You can export one or all of the BSPs as OBJ as well. The exported OBJs contain vertices, triangles, texture coordinates and vertex colors; since OBJ traditionally has no support for vertex colors, you can use the script located in the Tools folder to import these files to 3ds Max with vertex colors. Otherwise, they will be ignored. The material library expects PNG files in the same folder as the models (you can extract these with Magic.TXD).

Visibility Editor:
- The visibility editor allows you to edit the chunk boundaries *_blk.bin file.
- The AutoChunk function will automatically set chunk boundaries based on the extremities of the BSPs with the same number. The value you can set here is an offset to be added to the boundaries.
- If you open a ONE archive, it'll try to open the *_blk.bin automatically.

= Collision Editor (F4) =
The collision editor allows you to import an OBJ file, which will be converted to CL.
- New: lets you choose an OBJ file to import and a new CL file to save as.
- Open: opens a CL file and allows you to display or import an OBJ over it.
- Close: closes the CL file so you can't display or edit it anymore.
- Import OBJ: lets you choose an OBJ file to import and overwrite the current CL.
-- You can choose the maximum depth of the quadtree or let it be chosen automatically.
-- If you're looking at _xx (death) or _wt (water) models, normals are reversed, so you should disable culling as the models will show up upside down. Make sure to check "flip normals" when importing.
-- Append one of the following letters to the mesh names in your 3D model editor to set up collision flags:
 _a - water collision (for _wtCL)
 _b - bingo slide
 _i - invisible wall
 _k - protection barrier (ground-only wall)
 _l - slippery wall
 _p - pinball Table
 _s - stairs
 _t - triangle jumpable wall
 _w - wall
 _x - death collision (for _xxCL)
- Note: if your collision model appears completely black after importing, just open the file again and it should display correctly.

= Layout Editor (F5) =
The object layout editor allows you to add, remove and modify objects. You can open s*.bin files or create new ones. Objects will be displayed as small cubes. You can click on the objects to select them, but if the object is large you must click on their center. They will only be rendered if inside their specified render distance multiplied by 100 units, just like ingame (although you can enable rendering all objects as well). To render using their actual models, you must open the object ONE using the View Config window (under Project). If SONIC HEROES(TM) is running, you can get values from ingame or teleport yourself to an object's location. The editor also allows you to export and import the objects with their information to a text file and import objects from another file without overwriting the currently open one, and OBJ files (in this case, a ring will be placed in each vertex, this is useful for importing lines of rings). You can take a look at the Object Editing pages in Sonic Retro to understand a bit more about this.

= Spline Editor (F6) =
The spline editor allows you to view, choose the type of, delete and import new splines from OBJ files. Splines will be loaded from OBJ files in the Splines folder located in the same directory as your CC file (a new one will be created if it doesn't exist). When importing, make sure your OBJ file contains only one spline. The spline editor is reliant on the Mod Loader Config Editor, and thus will only show up or allow you to save the splines if an existing CC is open in it. There is no way to display or edit splines straight from the game's EXE. If no splines are present, a null one will be created.

= Camera Editor (F7) =
The object layout editor allows you to add, remove and modify cameras. You can open s*_cam.bin files or create new ones. Camera triggers will be displayed as their shape; you can click on the center of the triggers to select them. For the selected camera, the camera location will be displayed as a pink cube and points A, B and C will be displayed as red, green and blue cubes (as long as they're not at the origin). Note that we don't know 100% about cameras yet, but if you take a look at the original camera files and the information on Sonic Retro you might be able to understand a bit of it.

= Shadow Level / Collision Viewer (XBOX) =
The Shadow level viewer allows you to open an extracted level folder from the XBOX ISO of Shadow the Hedgehog (GameCube and PS2 are unsupported) and view the stage's model and collision. The stage's visibility will be loaded as well. Neither the models or the visibility can be edited, but you can export both stage and collision models to OBJ. The same of the Heroes model exports applies here.

= Shadow Layout Editor =
The Shadow object layout editor allows you to open, view and modify layout files from Shadow the Hedgehog. You can open *.dat files or create new ones. Most information about the Heroes Layout Editor applies here as well; please note that our knowledge about files from this game is much more limited than Heroes', so this is still very experimental.