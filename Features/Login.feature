Feature: Login

To ensure the login functionality works correctly.

@LoginPage
Scenario: Successful Login
    Given User navigate to url
    When User enter valid credentials
    And User click the login button
    Then User should see the dashboard page

Scenario: Failed Login
    Given User navigate to url
    When User enter invalid credentials
    And User click the login button
    Then User should see the error