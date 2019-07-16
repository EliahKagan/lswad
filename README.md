# lswad - *a WAD directory lister*

*Written in 2016 by Eliah Kagan \<degeneracypressure@gmail.com\>. Documentation
added in 2019.*

*To the extent possible under law, the author(s) have dedicated all copyright
and related and neighboring rights to this software to the public domain
worldwide. This software is distributed without any warranty.*

*You should have received a copy of the CC0 Public Domain Dedication along with
this software. If not, see
<http://creativecommons.org/publicdomain/zero/1.0/>.*

Doom, and other games that used the classic *id tech 1* engine, store most of
their game data in WAD files ("Where's All the Data"). Each WAD contains a data
structure known as a *directory* that identifies the locations of each entry in
the WAD file.

lswad lists the contents of that directory, showing each entry's name, byte
offset in the file, and length. This utility does not dump, view, or validate
data stored *within* an entry; it's just a WAD directory lister.

**lswad runs in LINQPad 5**, which can be obtained at https://www.linqpad.net.

I'd like to acknowledge Joe Pantuso, whose excellent 1995 book *The Doom Game
Editor* got my friends and me into making maps for Doom. I used chapter 2 of
that book ("Master WAD and PWAD Structure") as a reference while writing lswad.
