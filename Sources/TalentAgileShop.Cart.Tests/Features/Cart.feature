Feature: Cart
	In order to sell products I need to calculate the cart price and the delivery price




@ignore
Scenario: Simple product and delivery price
	Given a cart
	And 3 Small product with a price of 20
	When I calculate the total price
	Then the product price should be 60
	Then the delivery price should be 15

@ignore
Scenario: Complex cart 
	Given a cart
	And 3 Small product with a price of 20
	And 1 ExtraLarge product with a price of 40
	When I calculate the total price
	Then the product price should be 100
	Then the delivery price should be 35

@ignore
Scenario: Complex cart with FREESMALL discount
	Given a cart
	And the discountCode FREESMALL
	And 3 Small product with a price of 20
	And 1 ExtraLarge product with a price of 40
	When I calculate the total price
	Then the product price should be 100
	Then the delivery price should be 20
@ignore
Scenario: Complex cart with delivery price higher that 50
	Given a cart
	And 3 Small product with a price of 20
	And 3 ExtraLarge product with a price of 40
	When I calculate the total price
	Then the delivery price should be 50

# Implement the Then step and uncomment this scenario
#Scenario: Invalid discount code
#	Given a cart
#	And the discountCode FREESMALL
#	When I calculate the total price
#	Then the discount code is invalid