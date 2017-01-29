Feature: Navigation
	In order to avoid broken pages, I need to check that the navigation is OK


Scenario: Homepage
	Given I navigate to the homepage
	Then I should see the welcome text

Scenario: Catalog
	Given I navigate to the homepage
	And I click on the catalog button on the menu
	Then I should see the product list

Scenario: Catalog filter
	Given I navigate to the homepage
	And I click on the catalog button on the menu
	Then I should see the category filters

Scenario: Add a product to the cart
	Given I navigate to the product page with the id 'tshirt-standup-meeting-m'
	And I click on the add to cart button
	Then There is 1 log 'Added to cart!'

Scenario: Add a product twice to the cart
	Given I navigate to the product page with the id 'tshirt-standup-meeting-m'
	And I click on the add to cart button
	And I click on the add to cart button
	Then There is 2 log 'Added to cart!'

Scenario: Empty cart price is 0
	Given I navigate to the cart page
	Then The product cost is 0 €
	And The delivery cost is 0 €

@ignore
Scenario: Catalog Thumbnail View visible
	Given I navigate to the homepage
	And I click on the catalog button on the menu
	Then I can switch to thumbnail view

@ignore
Scenario: Catalog List View visible on thumbnail view
	Given I navigate to the homepage
	And I click on the catalog button on the menu
	And I switch on the thumbnail view
	Then I can switch to list view

@ignore
Scenario: Cart price with a tshirt size M
	Given I navigate to the product page with the id 'tshirt-standup-meeting-m'
	And I click on the add to cart button
	And I navigate to the cart page
	Then The product cost is 20 €
	And The delivery cost is 5 €

@ignore
Scenario: Cart price with a tshirt size M and with a FREESMALL discount
	Given I navigate to the product page with the id 'tshirt-standup-meeting-m'
	And I click on the add to cart button
	And I navigate to the cart page
	And I enter the discount code 'FREESMALL'
	Then The product cost is 20 €
	And The delivery cost is 0 €