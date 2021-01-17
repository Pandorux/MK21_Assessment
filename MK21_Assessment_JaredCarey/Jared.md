
# Things to Consider
- Instead of doing null checks, do exception handling
- Should I really be using StopWatch for time tracking?
    - My solution for StopWatch and Countdown need to be the same for consistent behavior 
- How am I going to validate time inputs? 
    - Regex is always a nice option
- Should I store generic information like DateFormats into a static class??
- Making the total number of time modes adjustable would be super nice 
- Saving and Loading for all of the clocks would be nice 
    - I like doing persistence way too much
- Would a static class for pausing button clicks and button click timings be helpful for this??
- Giving Clocks Names
- Choosing different time zones for clocks
# TimeMode Interface Methods
    - StartTimeMode
    - ResetTimeMode
    - StopTimeMode
    - ValidateInputtedTime
    - Event UpdateTimeDisplay

# Visual Appeal - How am I going to?
- Do the blinky blinky for time display?

# Bugs / Improvements
- If you edit without stoping the countdown then the stopWatch will continue counting up
- Buttons need to be deactivated when appropriate