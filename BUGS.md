# Known Bugs in lswad

*This file is part of lswad, a WAD directory lister.*

*Written in 2016 by Eliah Kagan \<degeneracypressure@gmail.com\>. Documentation
added in 2019.*

*To the extent possible under law, the author(s) have dedicated all copyright
and related and neighboring rights to this software to the public domain
worldwide. This software is distributed without any warranty.*

*You should have received a copy of the CC0 Public Domain Dedication along with
this software. If not, see
<http://creativecommons.org/publicdomain/zero/1.0/>.*

On Windows 10, the "Open File" dialog box often appears *behind* the
application and other open windows, behaving non-modally (allowing interaction
with LINQPad). When this occurs, it creates the impression that lswad has hung.
I don't know why this happens or how to reliably trigger or avoid it. When I
wrote and tested lswad six years ago, I never observed this.

The workaround I recommend is simply to press *Alt+Tab* and select the dialog
box, if you don't see it immediately upon running lswad.
