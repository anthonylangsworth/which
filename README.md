which
===
A very simple implementation of GNU "which" for Windows. Given commands to
search for, this scans the PATH environment variable to find the executable, 
script, batch file or similar will be executed. It scans the PATHEXT
environment variable to match the extensions correctly, too.

License
---
See [LICENSE](./LICENSE) for the license (BSD).

Platform
---
Developed on Visual Studio 2012 and .Net Framework version 4.5 on Windows 8. 
Untested on earlier compiler versions or Mono.

Dependencies
---
 - CommandLineParser 1.9.3.34 (https://github.com/gsscoder/commandline)
 - NUnit 2.6.2 (testing only, http://nunit.org/)
