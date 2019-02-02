# Console_Exam_Practise_Feb2019_Solution

What's covered...
* Menu implementation using a finite state machine (https://brilliant.org/wiki/finite-state-machines), consisting of while loops & switch-case statements (as you've probably been using).
  * See "state_diagram.jpg" which shows the different program states and what conditions cause them to transition.
  * Sketching out a quick state diagram, with maybe some simple flow charts / pseudocode (https://www.bbc.com/bitesize/guides/z3bq7ty/revision/2) showing the operation of each state, is a good way of helping to well stuctured. It only takes a few minutes, and can save alot of confusion, which in an exam you don't have time for!
* Example of how to split code across multiple classes (HelperFunctions class).
* Validation of user-supplied console input (see HelperFunctions -> ValidateString())
* Safely parsing integers from strings, without using try-catch or string validation (see RunCalculator(), line 184)
* I've also completed the challenge requirements, which requires the stored names to be saved to a text file. Functions for achieving this are at the bottom of the HelperFunctions class.

IMPORTANT NOTE: The brief seems to indicate that the solution needs to save MULTIPLE names, hence my use of a string List (customer_names).

As with other solutions, each part of the code is well documented. If you discount all of the comments, the program isn't that long.
