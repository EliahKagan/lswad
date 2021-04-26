# lswad - a WAD directory lister

*Written in 2016 by Eliah Kagan \<degeneracypressure@gmail.com\>. Documentation
added in 2019 and updated in 2021.*

*To the extent possible under law, the author(s) have dedicated all copyright
and related and neighboring rights to this software to the public domain
worldwide. This software is distributed without any warranty.*

*You should have received a copy of the CC0 Public Domain Dedication along with
this software. If not, see
<http://creativecommons.org/publicdomain/zero/1.0/>.*

[Doom](https://en.wikipedia.org/wiki/Doom_(1993_video_game)), and other games
that used the classic [id tech 1](https://en.wikipedia.org/wiki/Doom_engine)
engine, store most of their game data in [WAD](https://doomwiki.org/wiki/WAD)
files (&ldquo;Where&rsquo;s All the Data?&rdquo;). Each WAD contains a data
structure known as a [*directory*](https://doomwiki.org/wiki/WAD#Directory)
that identifies the locations of each entry in the WAD file.

**lswad** lists the contents of that directory, showing each
[lump](https://doomwiki.org/wiki/Lump)&rsquo;s name, byte offset in the file,
and length. This utility does not dump, view, or validate data stored *within*
a lump; it&rsquo;s just a WAD directory lister.

**This is lswad version 1, which runs in LINQPad 5 or later**. LINQPad can be
obtained at [linqpad.net](https://www.linqpad.net). Open `lswad.linq` in
LINQPad and click &#9654; (or press <kbd>F5</kbd>) to run it.

I&rsquo;d like to acknowledge Joe Pantuso, whose excellent 1995 book [*The Doom
Game Editor*](https://doom.fandom.com/wiki/The_Doom_Game_Editor) got my friends
and me into making maps for Doom. I used chapter 2 of that book (&ldquo;Master
WAD and PWAD Structure&rdquo;) as a reference while writing lswad.

NOTE: This project is ***not*** related to
[**xwadtools**](https://github.com/Doom-Utils/xwadtools) by Udo Munk, which
also contains a program called `lswad`, a command-line utility written in C. (I
wasn&rsquo;t aware of xwadtools when I wrote the graphical C#/LINQPad app
lswad.) Those tools are pretty cool, though&mdash;you should check them out!
