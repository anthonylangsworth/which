which
===
A very simple implementation of GNU "which" for Windows. Given commands to
search for, this scans the PATH (to find the directories) and PATHEXT (for the
extensions) environment variables to find the script, batch file or similar 
that would be executed. 

License
---
See LICENSE for the license (BSD).

Platform
---
Developed on Visual Studio 2012 and .Net Framework version 4.5 on Windows 8. 
Untested on earlier compiler versions or Mono.

Dependencies
---
 - CommandLineParser 1.9.3.34 (https://github.com/gsscoder/commandline)
 - NUnit 2.6.2 (testing only, http://nunit.org/)
