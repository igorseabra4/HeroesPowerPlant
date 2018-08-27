This is the Reloaded Mod Loader Generic Stage Injection Mod. You can copy this mod template to use with your own stage mod without any need for coding; it's basically just placing your modified files in the mod, and it'll work.

Here's how to set it up:

0. Make sure you have Reloaded Mod Loader installed and updated to the latest version.
1. Copy the Reloaded-Mod-Template folder to "\AppData\Roaming\Reloaded-Mod-Loader\Reloaded-Mods\Sonic-Heroes\", where it should be among other Reloaded mods if you have any.
2. Rename Reloaded-Mod-Template to your mod's name (for example, My Mod).
3. Open the Config.json file in the folder with a text editor and edit it to your mod's info. Make sure the ModId and ModName fields are unique, as conflicts with other mods will cause Reloaded problems. You don't need to set all the fields right now, you can leave some empty.
4. The Banner.png is the image which will be displayed by Reloaded when choosing the mod. It must have dimensions of 271x271. You don't have to make one right now, but replace the sample one before releasing your mod!
5. Your stage(s) will be placed in the mod's Stages folder. Each stage will have its own folder (a single mod can have as many stages as you want, only one is also ok though).
6. There's a default folder named "0" there. Rename it to your stage's name (for example, My Stage). You should see files named dummy.txt inside the folders. You can delete those files.
7. This is the folder where you'll save Heroes Power Plant's Config Editor's Stage.json (it must be named exactly Stage.json).
8. The \Splines\ folder will contain the stage's splines. Don't mess with this folder, let Heroes Power Plant's Spline Editor manage it. It'll be created automatically if needed.
9. The \Files\ folder is where all the stage's files will go. This is the equivalent of dvdroot. For example, s**.one files would go straight into \Files\. s**.txd would go in \Files\textures\ and s**.cl would go into \Files\collisions\.
10. Reloaded Mod Loader should find the mod and allow you to enable it. All file redirection, start/ending/bragging position replacement and spline replacement should happen automatically if you set up everything correctly.
11. Files will be updated in real time, so you can edit them without closing the game. You need to quit and reenter the stage to see the changes. Changes on splines and start positions require you to close the game though.

Note: if you're working with multiple stages in the same mod, be sure to have only one setidtbl.bin file which is common to all of them. This file is loaded by all stages and the mod will not solve conflicts if there are different setidtbl.bin files in multiple stage folders.