# Objective
You act as an enterprise holding a given amount of resources and data. The objective of this game is to achieve the highest possible asset before the maximum turn reached. Asset can be earned by joining Federations with different incentive-schemes. 

To join, a bid has to be made to a Federation during Bid Round in which, the Federation will decide whether to accept or reject the bid submitted. 

Bid submitted represents the amount of resources and data you want to commit to training of the model. Maximize earned assets by either committing all your resources to a Federation or dividing of resources to available Federations.

# Resources & Data
The amount of resource committed determines the speed of training the local model.

Data quality & quantity determines the quality of the trained model.

This game follows the following formula to simulate the quality of trained model

z= x* w1+ y*w2 , where x and y represents the quality and quantity of data while w1 and w2 are some constant weights. 

# Market Share
During each increment in turns, each Federation will be allocate a portion of the global market asset based on their market share. A portion of this asset will then be used to disseminate profit to the participants. The market share of the Federation will be updated as well.

# Incentive Schemes
## Equal Distribution Scheme
Profit is split equally to all the participants in the Federation during Training Round
## Contribution Based Scheme
Profit is split based on individual contribution (Model Quality) to the Federation during Training Round.
