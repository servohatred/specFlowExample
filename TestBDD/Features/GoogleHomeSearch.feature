Feature: Google Homepage Search

Scenario: User can search with 'Google Search'
	Given I’m on the homepage
	When I type a 'The name of the wind' into the search field
	And I click the Google Search button
	Then I go to the search results page
	And the 'First' result is 'The Books - Patrick Rothfuss'
	When I click on the 'First' result link
	Then I go to the 'Patrick Rothfuss - The Books' page

Scenario: User can search by using the suggestions
	Given I’m on the homepage
	When I type a 'The name of the w' into the search field
	And the suggestions list is displayed
	And I click on the 'First' suggestion in the list
	Then I go to the search results page
	And the 'First' result is 'The Books - Patrick Rothfuss'
	When I click on the 'First' result link
	Then I go to the 'Patrick Rothfuss - The Books' page

#Scenario Outline: User can search with 'Google Search'
#	Given I’m on the homepage
#	When I type a '<Search Word>' into the search field
#	And I click the Google Search button
#	Then I go to the search results page
#	And the '<Result Position>' result is '<Expected Result>'
#	When I click on the '<Result Position>' result link
#	Then I go to the '<Expected Page>' page
#
#	Examples:
#		| Search Word          | Result Position | Expected Result                         | Expected Page                |
#		| The name of the wind | First           | The Name of the Wind - Patrick Rothfuss | Patrick Rothfuss - The Books | #FAILURE CASE
#		| The name of the wind | Fourth          | The Books - Patrick Rothfuss            | The Books - Patrick Rothfuss | #SUCCESS CASE
#
#Scenario Outline: User can search by using the suggestions
#	Given I’m on the homepage
#	When I type a '<Search Word>' into the search field
#	And the suggestions list is displayed
#	And I click on the '<Sugestion Position>' suggestion in the list
#	Then I go to the search results page
#	And the '<Result Position>' result is '<Expected Result>'
#	When I click on the '<Result Position>' result link
#	Then I go to the '<Expected Page>' page
#	Examples:
#		| Search Word          | Sugestion Position | Result Position | Expected Result                         | Expected Page                |
#		| The name of the wd   | First              | First           | The Name of the Wind - Patrick Rothfuss | Patrick Rothfuss - The Books | #FAILURE CASE
#		| The name of the wind | First              | Fourth          | The Books - Patrick Rothfuss            | The Books - Patrick Rothfuss | #SUCCESS CASE