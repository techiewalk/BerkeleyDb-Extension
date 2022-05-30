# BerkeleyDb API


[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Berkeley DB (BDB) is a software library intended to provide a high-performance embedded database for **key/value** data. Berkeley DB is written in C with API bindings.

  - BDB stores arbitrary key/data pairs as byte arrays, and supports multiple data items for a single key.
  - BDB can support thousands of simultaneous threads of control or concurrent processes manipulating databases as large as 256 terabytes

# Why new API?

A new API eases bdb operations by providing interactive operations on Data and Keys, like any other collection we use C#. Another major advantages you can provide your own serialization/message protocols and use them without modifying underline implementation. 

### Tech

Dillinger uses a number of open source projects to work properly:

* [C#] 
* [Visual Studio] - Editor
* [VS Code] - Editor
* [.Net] 


### Installation

Download either Libraries or Source code( and compile it locally) and refer those libraries in your projects.


License
----

MIT




   [VS Code]: <https://code.visualstudio.com/>
   [.Net]: <https://dotnet.microsoft.com/download/dotnet-framework>
   [C#]: <https://github.com/dotnet/csharplang>
   [Visual Studio]: <https://visualstudio.microsoft.com/>

   [PlGh]: <https://github.com/joemccann/dillinger/tree/master/plugins/github/README.md>
   [PlGd]: <https://github.com/joemccann/dillinger/tree/master/plugins/googledrive/README.md>
   [PlOd]: <https://github.com/joemccann/dillinger/tree/master/plugins/onedrive/README.md>
   [PlMe]: <https://github.com/joemccann/dillinger/tree/master/plugins/medium/README.md>
   [PlGa]: <https://github.com/RahulHP/dillinger/blob/master/plugins/googleanalytics/README.md>
