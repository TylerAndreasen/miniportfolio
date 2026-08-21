#!bin/sh
# NOTE: Give separating lines such that the ending semi-colon does not influnce the next command.

# Git Commands

alias ga='git add';

alias gaa='git add .';

# alias gam='git commit -a -m ';

alias toodoo="bash ~/toodooscript.sh $@"

alias minetest="bash minetest.sh a"

alias kanri="cd '/c/Program Files/kanri/' && start kanri.exe"

alias pyhome="cd /c/Users/CanaDev/AppData/Local/Programs/Python/Python313/Scripts"

alias pymines="kanri && cd /c/Users/CanaDev/AppData/Local/Programs/Python/Python313/Scripts/MineSweeper && code ."

alias pysnake="kanri && cd /c/Users/CanaDev/AppData/Local/Programs/Python/Python313/Scripts/Snake && code ."

alias visualstudiosolutions="cd /c/Users/CanaDev/source/repos"
alias vssh="visualstudiosolutions"
alias vss="start *.sln"

# File System Commands

alias explore='explorer .';

alias linecount="wc -l $@"


# Development Commands

# alias vcode="code . --new-window"; # Redundant

# Bash Helpers

alias rebash=". ~/.bashrc"; # DO NOT REMOVE

alias editbash="code /c/Users/CanaDev/.bashrc"
alias ebash="editbash"
alias eb="editbash"

alias installers="cd c/Users/CanaDev/Desktop/Installers"

alias bashhelp="cd c/Users/CanaDev && code .bashrc"


# Code-Related Environment Tools

alias eeck="echo 'a mouse' && code . && explore"
alias eck="eeck"

alias pnotes="code /c/Users/CanaDev/GitLocal/ProgrammingNotes"

alias pyprograms="cd /c/Users/CanaDev/AppData/Local/Programs/Python/Python313/Scripts && explore"
alias pyprog="pyprograms"
alias pypy="pyprograms"
alias py="echo 'Did you mean :pyprograms:"

#alias linerange="cat -n $0 | awk '\$1>=$1 && \$1<=$2 {sub(/^\s*[0-9]+\s*/,""); print}'"

alias music="cd '/c/Program Files/Windows Media Player/' && start wmplayer.exe"

alias devtools="kanri && pnotes && music && cd ~/GitLocal"

alias minemod="cd ~/GitLocal/MinecraftModdingFiles"

# Meme

alias indend="echo 'Not a word. Did you mean: indent, intend, intent'"