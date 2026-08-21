# Mini Portfolio

If you are reading this, you were likely directed here by by resume. I appreciate you taking the time to look into the projects I have worked on and felt were in a state to present (not necessarily complete), there are many more half finished ideas sitting around on my hard drive. Among these projects are a variety of different types of endeavors: everything from a notes compendium, text games, a neural network library, and more practical software tools. The goal of the diversity in this portfolio is to demonstrate my technical flexibility and breadth-over-depth of my current skillset.

## Contents

1. Programming Notes: A Compendium
2. Lascii: Crouching Project, Hidden Lessons
3. The Minion: A Rapid Change in the Plan
4. Snake AI: A Lesson in Context
5. Budget Tracking: An Exercise in Not Revealing My Financial Details
6. Bash Script: The Current Iteration of My Hand-Made Multi-Tool

## 1. Programming Notes: A Compendium

Throughout my education, the single most important tool I had for remembering and building on what I have learned was consistent note-taking. And by extention, knowing where those notes are when I need to reference them later. But in my early years of learning to write code, I did not have a consistent place for where to store them. Many important revelations were only commented on in readmes and scratch files inside of projects or folders dedicated to a single language or environment. This was made even harder to operate within once I split my work between a laptop and a desktop; the notes were everywhere and I needed a comprehensive solution. So I consolidated the notes I had into one folder, initialized a git repo and called it [ProgrammingNotes](https://github.com/TylerAndreasen/ProgrammingNotes) (form over function for the practical nature of the tool). While there is a substantial amount of notes that are now too niche to be useful (discussing issues from projects that are not fully explained or cited) or simply need rewriting, there is a key utility to this collection. And that is the simplicity of knowing exactly where to go when I want to refer back to things I have done and where to record my expanding knowledge base. *If I didn't write it down, did I really learn it?* For all of it's utility, I doubt that many will ever gain much direct benefit from it other than myself.

## 2. Lascii: Crouching Project, Hidden Lessons

To see the source code, see this [link](https://github.com/TylerAndreasen/LasciiEngine), though it is far from a stellar piece of software.

Several years ago, I dreamt of a game that gave the player a vast world to move through and, if they chose to, make world altering decisions, or to simply settle down and become a farmer. And I knew that such ambition would take time and a fair bit of luck to achieve. So I tried to narrow the scope significantly with a text-based game engine, moreso in the vain of Zork than that of Rogue. And as I contemplated the initial structure, I made comparisions to Choose Your Own Adventure books, despite such books not playing a significant part in my life. And with a high level design, I set to work. I would represent places in the world as `Page`s, each of which had different `Action`s that the player could choose to take and move the player to a different `Page`. After a number of hours of development, it became painfully clear that stateless `Page` objects are not a good way to represent even a tiny 2-Room D&D style dungeon. Multiple `Page` Objects had to be created and carefully linked together to ensure that the player would be able to unlock a door after they picked up a key from under a bowl on a bookshelf and not be able to pick the key up again, not to mention actually going into the next room. (The structure of such books is fine, but is not desirable for the given usecase.)

During this project I also implemented the single most time-consuming bug of my career thus far. In short, a quick-and-dirty data creation list written into the source code had a very similar name to the dynamic list used during the game's main logic loop: `traits` and `allTraits`. Specifically, I implemented two methods related to the traits associated with the player, which could be expanded over time as they gained new abilities (or in the sample game, picked up the key). The first method checked to see if the player had a `Trait` (a key-value pair) with a given name/key, and this method correctly referenced the `allTraits` variable (an `ArrayList<Trait>` object in Java). The second method returned entire `Trait` object matching the provided name from the `traits` array. This had two consequences. First, because the `traits` list never had its contents removed, changes made to `Trait` Objects which were defined in the source code reflected those changes made during game play when attempting to utilize their values, making me think the code worked as intended. Second, because the `traits` list never had new elements added (while `allTraits` did), `Trait` Objects could be given to the player without error, despite no *apparent* change to the `Trait`s the player had. This cost me at least ten hours of debugging time.

This bug was introduced via a confluence of multiple things going wrong at once.

1. Poor Variable Naming: Including two variables with such similar names was begging to create an issue like this, though this would not likely have consumed so much time on it's own.
2. Poor Data Management: I created the array `traits` largely as a list of objects initially to test the `Trait` class and kept most entries around as an homage to Dungeons and Dragons, a game that has inspired me greatly. I kept the list in the source code because it was an easy way to set up the list of traits that the player would need to move around the world at the start of the game. This is not a good strategy, as changes to data then require a recompile of the code before the game can be tested, wasting time that could be spent playtesting, implementing other features, and more.
3. Lacking Knowledge: The worst part of this bug was actually when it occurred relative to advancements in Language Models. While I do not have any meaningful experience with long form conversations with language models, I do find search result summaries to sometimes be useful. And such a tool would have been invaluable. I knew when I was creating Lascii that games like Skyrim (my biggest source of inspiration at the time) must have some structured way of writing data about the game world to a file and reading it back later. That is the digital interpretation of saving a game state. But I had no knowledge of the language I would need to use to find that information, and built a custom solution to read and write every object type in Lascii to and from a file. In theory, patient Google-foo, scouting of StackOverflow, or asking software people I knew would have turned up the phrase Object Serialization, but I decided to build an incredibly complicated system instead. This foolish-in-retrospect determination to do things myself and limited knowledge enforced a bad idea about data management, which combined with one typo on a variable name to devestate the pace of progress on the project.

It has been five or more years since I wrote Lascii, and I have learned a number of fundemental lessons about software development since, but this project will always be a major part of my history as a developer. Given the distance from it and how new to code I was, I am not ashamed of the mistake. Truth be told, I am glad I made such an error so early in my career. And I plan to utilize this project and the lessons it taught me to help me in the future. Not every decision I make will have this in mind, but it is one guide star I hope to never lose sight of.

For the curious, the Lascii has a mildly interesting origin for me. I knew very early in development that I wanted to include viewable ASCII art vingettes of key characters, items, places, events and more. And on my desktop I named the folder for the project ASCII, after the organization that has had an enourmous impact on global digital communication standards. When I returned to campus the fall after starting the engine, I knew I would want to copy files I had made on my laptop back to my desktop without risk of over writting files and needed some way to ensure I wouldn't. Because I was working from a laptop, I put an `L` at the start of the folder name and Lascii has stuck as the name of the engine since. And I have since learned about version control, which would also have made things simpler.

## 3. The Minion: A Rapid Change in the Plan

TODO expand this list of bullet points
- Lascii was cool, but this game jam only last 48hours, and the sample dungeon I made really doesn't fit the theme.
- If it was in a dungeon, you could play as a little gremlin hiding objects in differnet places and a hero will try to come in and solve the puzzle you laid out.
- This is going well.
- I need to sleep
- TODO look up review notes on the project and why I didn't submit anything

## 4. Snake AI: A Lesson in Context

Code [link](https://github.com/TylerAndreasen/NN)

- Skyrim is cool, what if I could write scripts that learned how beat Skyrim using just it's own engine?
- That is really complicated, how do I scale back the project to learn about the fundementals?
- Write an AI for Snake! After you build snake. Also graphics in C++ are fun (these actually weren't bad with raylib)
- This didn't take a huge amount of time to write, but doesn't actually learn what I am trying to teach it and I am still not certain why.
- Link Coding Train tutorial and mention attempted meta-training script
- This is something I want to come back to in the near future.


## 5. Budget Tracking: An Exercise in Not Revealing My Financial Details

Given my current and future living situations, I want to have a better understanding of the money I am making and spending on a monthly basis. A daily analysis is not useful basically at all, and weekly will still contain significant enough variation between weeks that pattern recognition is going to be very difficult. And many bills are charged on a monthly basis, so it feels like the most natural fit. I had previously used a tracker that laid out every day in its own row from whenever you started the top line fo the sheet. And the idea was that all incomes and outgoes for a given day would be totalled and assigned to their respective cells. I found this not particularly helpful, as I was doing little other than parroting my bank statements into a spreadsheet and gaining no real insight into amounts and dates. This was made worse by a income adder table that would not match when I was paid in a month, making proejcts of maximum and minumum balances questionable at best. So, I considered what it was I wanted to understand from my finances and contemplated what I needed to get that information. I knew that I wanted to look at my finances on a monthly basis, and be able to total how much I was spending on various categories of outgoes. I also did not want to manually total the amounts for a given day, a well put together spreadsheet should be able to handle that (though my interation could be cleaner in implementation). So I created a sample sheet that tracks all expenditures for a month, and gives a report of the resultant totals and percentages. I included X categories, including Other, generally separating charges into the most common things I spend money on including multiple bills. And at any time, I can scroll down from the main Day-Categories-Costs region to the review region to see the total of how much I spent on lunches and what percentage of my income and outgo that total represents, and how much the average Lunch charge represents of my income and outgo. This largely tells me that while I am spending a non-trivial amount on my lunches at work in total in a month, getting a bag of chips on occasion will not be what makes or breaks my bank.

## 6. Bash Script: The Current Iteration of my Hand-Made Multi-Tool

- I have done some bash scripting, and want to do more.
- given the stage in my career and experimenting with many languages and tools, I am not doing the same things (other than git) with much frequency, so I won't get much benefit from expanding it now.
- Though 'gaa' will follow me for many years.