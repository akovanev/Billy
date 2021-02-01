# This script will help you download, build and run Billy!
#
# Make sure that you have git and .net core 3.1 installed and enjoy!
#
# If PowerShell blocks the script - run next line first.
# Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
#
# Let Billy get started!

# Comment next line if you already have Billy.
git clone https://github.com/akovanev/Billy.git

# Let Billy walk your computer.
$pwd = pwd
cd Billy

# And throw out the trash. 
Get-ChildItem .\ -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }

# Finally Billy wants to make it fun.
cd src\Billy.Console

# Are you ready? Never mind, Billy is getting started.
dotnet run -- ..\..\tests\aaa.file

# Wow, take it easy! Billy doesn't know what it is.
dotnet run -- ..\..\tests\宿宿.txt -encoding utf8

cd $pwd