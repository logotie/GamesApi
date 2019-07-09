# GamesApi
An Asp.Net Core Web API that provides access to games stored on a Mongo DB local instance.

The entire JSON array collection used in the app, is displayed here:
</br>
https://raw.githubusercontent.com/logotie/GamesApi/master/GamesApi/defaultjson.json

Please clone the code and open the solution 'GamesApi.sln' using Visual Studio.  I used Visual Studio 2019
</br>

There is a project for the API and another project for the XUnit tests.
<br>

APIs:
<br> <b> localhost:8080/games/{id} </b>
</br>
Displays the game object with the id.
</br>
<br> <b> localhost:8080/games/? 
<br> Accepts any of the following query strings and returns a list of game objects
</b>
<ul>
<li>likesGreater - games that have a greater amount of likes than the specified number
<li>platform - games that are publised on the platform
<li>publisher - games that were published by a certain company
<li>publishedAfter - games that were published after a certain epoch time (in seconds)
<li>minAge - games that have a higher age rating than the specified number
<li>maxAge - games that are have a lower age rating than the specified number
</ul>
Example: http://localhost:8080/games/?platform=ps4&minage=1
<br>
<br>
<b> localhost:8080/games/report </b>
<br> Displays a report similar to https://gist.github.com/divya051988/cfe18cbd24bbeec62eb2444ff55f3c34

On starting the web app:
<ol>
<li> The JSON array is converted from JSON into a list of game objects
<li> The game objects are stored in a local temporary MongoDb instance
<li> The game objects are exposed via different APIs
</ol>

External libraries used:
<ul>
<li> Microsoft.AspNetCore.App 
<li> Microsoft.AspNetCore.Mvc.Core
<li> Microsoft.Net.Test.Sdk
<li> Microsoft.AspNetCore.Razor.Design
<li> Mongo2Go - Local instance for debugging
<li> MongoCSharpDriver - Mongo DB driver
<li> MongoDB.Driver - Mongo DB driver
<li> Moq - For mocking objects when testing 
<li> Newtonsoft.Json - Json manipulation
<li> xunit - creating unit tests
<li> xunit.runner.visualstudio - displaying the unit tests in Visual studio
</ul>

