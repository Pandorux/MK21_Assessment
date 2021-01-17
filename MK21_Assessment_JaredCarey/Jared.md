
# Things to Consider
- Instead of doing null checks, do exception handling
- Should I really be using StopWatch for time tracking?
    - My solution for StopWatch and Countdown need to be the same for consistent behavior 
- How am I going to validate time inputs? 
    - Regex is always a nice option
- Should I store generic information like DateFormats into a static class??
- Making the total number of time modes adjustable would be super nice 
# TimeMode Interface Methods
    - StartTimeMode
    - ResetTimeMode
    - StopTimeMode
    - ValidateInputtedTime
    - Event UpdateTimeDisplay

# Visual Appeal - How am I going to?
- Do the blinky blinky for time display?

# Bugs
- If you edit without stoping the countdown then the stopWatch will continue counting up