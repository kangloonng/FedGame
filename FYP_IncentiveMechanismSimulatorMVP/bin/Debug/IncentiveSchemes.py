import math
import numpy as np
from copy import deepcopy

class util:
    def __init__(self):
        self.listBuild = []        

    def inputList(self, test1,test2,test3,test4,test5,test6):
        self.listBuild.append([test1,test2,test3,test4,test5,test6])

    def reInit(self):
        self.listBuild=[]

    def convertToArray(self):
        return np.array(self.listBuild)

#Base Federation Environment
class env:
    def __init__(self, budget, numPlayers):
        self.budget = budget
        self.N_players = numPlayers
        self.R2T = 20
        self.t=0
        #parameter settings
        self.w = 1.0
        self.N_parameters = 6 #number of input parameters from C# program
        self.t = 0
    #? Marginal calculation   
    def calc_budget(self, current_bids):
        #new round
        budget =0
        N_S = (np.array(current_bids)[:,3]).sum() #model quality at 3 #np.sum(np.array(current_bids)[:,5])
        if N_S ==0:
            return 0

        #use log(1+x) for now
        budget = self.budget * math.log(1+self.DataQ_sum(current_bids))

        self.last_budget = budget

        return budget
    #3=model quality
    def DataQ_sum(self, current_bids):
        return np.sum(np.array(current_bids)[:,3])

#---------------------------Base Incentive Schemes-----------------------------#

    
class federal(object):
    #def __init__(self,id,Env):
    def __init__(self,id,Env = None):
        self.id=id
        self.Name = "Base"
        self.Env = None
        if Env != None:
            self.Env=Env
            #self.federal_name='Equal'
            self.last_budget=0

#Split equally amongst participants        
class FL_equal(federal):
    def __init__(self,id,Env):
        federal.__init__(self,id,Env)
        self.Name='Equal'
        self.last_mu=[]
        
    def payout_calculation(self, current_bids):
        #print("test")
        unit_share =0
        N_participants = sum(np.array(current_bids)[:,5]>0)
        #print(N_participants)
        payout_list=[]
        payout_list = np.zeros(self.Env.N_players)

        (payout_list[np.array(current_bids)[:,5]>0]) = 1.0*self.Env.budget/N_participants

        return np.array(payout_list)
    
#considers data quantity, data quality and resource contributed (overall contribution).     
class FL_indi(federal):
    def __init__(self,id,Env):
        federal.__init__(self,id,Env)
        self.Name='Individual'
        #para FL
        self.last_mu=[]

    def payout_calculation(self, current_bids):
        N_participants = sum(np.array(current_bids)[:,1]>0)
        #print(N_participants)
        payout_list=[]
        
        payout_list = np.zeros(self.Env.N_players)
        
        S = np.sum(np.array(current_bids)[:,3])
        print("SUM ",S)
        for index, one_bid in enumerate(current_bids):
            data_quality = one_bid[1]
            data_quantity = one_bid[2]
            model_quality = one_bid[3]
            c = one_bid[5]
            if c == 0:
                continue
            
            payout_list[index] = (model_quality/S) * self.Env.budget    
        return np.array(payout_list)

#considers total quantity of data contributed weighted by data quality   
class FL_linear(federal):
    def __init__(self, id, Env):
        federal.__init__(self, id, Env)
        self.Name = 'Linear'
        self.last_mu = []

    def payout_calculation(self, current_bids):
        N_participants = sum(np.array(current_bids)[:,1]>0)
        #print(N_participants)
        payout_list=[]
        payout_list = np.zeros(self.Env.N_players)
        
        S = sum(np.array(current_bids)[:,1])
        #print("Sum:", N_total_model)

        for index, one_bid in enumerate(current_bids):
            data_quality = one_bid[1]
            data_quantity = one_bid[2]
            model_quality = one_bid[3]
            c = one_bid[5]

            if c ==0:
                continue            
            
            payout_list[index] = data_quality * self.Env.budget / S #((1.0*modelquality)/N_total_model) * self.Env.budget 
            #print(modelquality)
            
        return np.array(payout_list)#payout_list.tolist()



list_fl = []
list_fl.append(FL_equal(0,None))
list_fl.append(FL_indi(0,None))
list_fl.append(FL_linear(0,None))

print(list_fl)
