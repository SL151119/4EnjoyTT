# Task

General requirements: 
- The game must be in horizontal orientation
- The game should look without distortion on any screen with aspect ratios from 4x3 to 16x9.
- The pop-up window must occupy the same proportion of the screen in height at any of the screen at any resolution.
- The colors, placement of elements, and size should be consistent with the layout.
- All labels should use the font provided in the resources

Rules of lives: 
- There can be a maximum of 5 lives.
- If there are less than five lives, the next life is automatically granted after 20 seconds
- The time in the interface should be updated every second.

Scene requirements: 
1. The scene should be filled with a uniform color as on the mockup.
2. A widget with lives should be displayed in the upper left corner.
3. When you click on the widget, a popup with lives should open.
![Scene](https://user-images.githubusercontent.com/42243509/217857832-018ebae2-17fe-4b77-8d71-ffda316f9866.png)

Requirements for the lives widget: 
1. Time should be formatted as MM:SS
2. The text with the time should have a shadow like on the layout.
3. If there is a maximum of lives, "Full" should be displayed instead of time until the next one.

Requirements for the pop-up window: 
1. The scene should be darkened with transparent black.
2. When the window is opened, the darkening should appear smoothly (transparency increases from 0).  When you close the window, the darkening should disappear smoothly.
3. The window should have a title and a close button.
4. When you click on the close button or leave the window, the window should close.
5. When opened, the contents of the window should be animated to enter the stage from the left from outside the stage. When closed, the contents of the window should move animatedly offstage.
6. The window should display the current number of lives, a timer until the next life, and two buttons Refill Lives and Use Life. The timer should be updated every second.
7. The Refill Lives button fills lives to the maximum.
8. The Use Life button subtracts one life.
9. If there are 0 lives, the Use Life button is not displayed.
10. If there are maximum lives, the Refill Lives button and the timer to the next life are not is not displayed. 
11. The layout of the elements in the window differs depending on the number of lives (layouts below)

Normal view
![PopUp-NotFull](https://user-images.githubusercontent.com/42243509/217858116-c363b693-ba04-4cf9-a59a-252140817c3e.png)

No lives
![PopUp-NoLives](https://user-images.githubusercontent.com/42243509/217858055-036640e9-5ada-4826-b9bf-269b80e8a71a.png)

Maximum number of lives
![PopUp-Full](https://user-images.githubusercontent.com/42243509/217858102-f9e75216-975a-438b-b2b2-dd63da3669ae.png)

Requirements for project delivery: 
1. The project must be archived in ZIP
2. It must be indicated how long it took to complete the project.

Daily Bonus. 
1. When entering the game, a window with the name Daily Bonus should be displayed. It should contain the button Claim button and an inscription with the number of coins. When you click the Claim button, the window should simply close.
2. The number of coins depends on the current day of the season. 
	1. On the first day of the season (for example, September 1 or December 1), the player receives 2 coins (note, on October 2 it will be the 32nd day of autumn)
	2. On the second day of the season, the player receives 3 coins. (September 2)
	3. On the third and next days, the player receives the sum equal to 100% of the coins from two days ago and 60% of the coins from the previous day. The number of received coins is rounded.
Example: On September 3, the player will receive 60% of the coins from September 2 and 100% of the coins from September 1
