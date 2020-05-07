import IncentiveSchemes as IS
import numpy as np
import math

#create participants list
#id, data quantity, data quality, model quality, contribution, cost
participants = []
participants.append([1,0.3,0.5, 0.8,0.285714,100])
participants.append([2,0.3,0.2, 0.3,0.107143,100])
participants.append([3,0.1  ,0 ,0.5,0.178571,100 ])
participants.append([4,0.3,0.2, 0.6,0.2142286,100])
participants.append([5,0.1,0.6, 0.2,0.071429,100])
participants.append([6,0.2,0.4, 0.4,0.142857,100])
participants.append([7,0, 0,0,0,0])
participants = np.array(participants)
budget = 1000.00
test_env_obj = IS.env(budget, len(participants)) #federation

test_list = [[1, 0.0, 0.0, 0.0, 0.0, 0.0],
             [2, 0.49410000000000004, 0.8063999999999999, 0.58779, 0.0, 50.0],
             [3, 0.4392, 0.273, 0.38934, 0.0, 50.0],
             [4, 0.3784, 0.5124, 0.41859999999999997, 0.0, 50.0],
             [5, 0.6264, 0.5780000000000001, 0.61188, 0.0, 50.0],
             [6, 0.1938, 0.195, 0.19416, 0.0, 50.0],
             [7, 0.34680000000000005, 0.5076, 0.39504000000000006, 0.0, 50.0],
             [8, 0.26789999999999997, 0.23120000000000004, 0.25688999999999995, 0.0, 50.0],
             [9, 0.5185, 0.5428, 0.52579, 0.0, 50.0], [10, 0.6006, 0.2881, 0.50685, 0.0, 50.0],
             [11, 0.6830999999999999, 0.33, 0.57717, 0.0, 50.0]]
print(np.array(test_list))
test_env_obj = IS.env(budget, len(test_list)) #federation
#create scheme obj
obj1 = IS.FL_equal(0,test_env_obj)
obj2 = IS.FL_indi(0,test_env_obj)
obj3 = IS.FL_linear(0,test_env_obj)

#Testing participant list
print(type(participants))


##test payout_calculation
payout_list = obj1.payout_calculation(test_list)#participants)
print("Payout for Equal")
print(np.array(payout_list))


payout_list = obj2.payout_calculation(test_list)
print("Payout for Indi")
print(payout_list)

obj3.Env = IS.env(budget,len(test_list))
payout_list = obj3.payout_calculation(test_list)
print("Payout for Linear")
print(payout_list)
