Battleships
--
This is a classic battleship game that developed in Unity with C# as programming language. Aim of this project is to improve my knowledge and ability on Unity, networking and mobile technologies. Game will be developed as an open source project to let other people benefit who wants to learn or improve things related to this project.

I hope to publish the game on Google Play Store for Android phones to have a public executable if the final result satisfies me. Since some keys or addresses are private (about database and networking) I may not be able to share everything. I might hide a file that saves those private things as constants or find template accounts for this. I don't have a strict idea but I'll try to exhibit as much as I can.

A rough plan of the development and further informations can be found on this Trello board: https://trello.com/b/7HvgfiWf/battleship

Feel free to ask, copy or advise anything.

Prerequisites
--
- I am using Firebase as authentication and database service. In order to use/build the current version of the project you will need to import FirebaseAuth and FirebaseDatabase Unity packages. Detailed information:

  https://firebase.google.com/docs/auth/unity/start

  https://firebase.google.com/docs/database/unity/start

- There is class named 'Secrets' that you might be confronted many times, which would be missing. That file is a C sharp class inside the 'Assets' folder and ignored by the gitignore. It only has constant variables to be used as Secrets.SOMETHING_API_KEY. As you can guess from the example and the name of the class, variables are my secret information like API keys, links or passwords that I don't want to make public (as I already stated up above). I try to name variables as clean as possible. So all you need to do is replacing your own 'secret' wherever you see it.
