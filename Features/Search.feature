Feature: Search

Ensure that Users can search for items using the search bar and view relevant results

@Search
Scenario: User searches for an existing items
    Given User navigate to url
    When User input an items in the search bar
    And User click the search icon
    Then User should see the existing items
