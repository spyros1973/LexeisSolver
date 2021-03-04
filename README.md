# Lexeis Solver
A small program written in Visual Basic (.NET Framework 4.5), that gives all possible Greek words for board game Boggle and the various versions of it (like Words by Fugo Games or Wordament by Microsoft)

## About the word games this program targets
In these games, you get a 4x4 grid of letters, and you have to form words three letters or more long, by starting on a tile and continuing to an adjacent tile, vertically, horizontally or diagonally.


For example, in a starting board such as:

    B | A | W | K
 
    R | Z | E | K
 
    C | D | S | M
 
    P | N | W | K

one can start at the letter B, on the top left, move right to A, the diagonally down-left to R, then diagonally down-right to D and horizontally to S, creating the word BARDS.

The goal is to find as many words as possible. Points are awarded for word length and there may be points based on the letters used (more rare letters give more points than more common ones).


## About Lexeis Solver
For the program's dictionary, I used the Greek dicitonary supplied with OpenOffice (el_gr.txt) and kept all words from 3 to 10 characters.

![Sample screenshot](/images/scr1.png)
