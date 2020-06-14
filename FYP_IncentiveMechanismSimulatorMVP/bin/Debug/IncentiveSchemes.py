import math
import numpy as np
from copy import deepcopy

class util:
    def __init__(self):
        self.listBuild = []        

    def inputList(self, param1 ,param2,param3,param4,param5,param6):
        self.listBuild.append([param1 ,param2,param3,param4,param5,param6])

    def reInit(self):
        self.listBuild=[]

    def convertToArray(self):
        return np.array(self.listBuild)

#Base Federation Environment
class env:
    def __init__(self, budget, numPlayers):
        self.budget = budget #amount budget, will be initialize every round of payoff calculation
        self.N_players = numPlayers #num players
        self.R2T = 20 
        self.t=0 #no of rounds
        #parameter settings
        self.w = 1.0
        self.lya_lamb_choice=0
        self.N_parameters = 6 #number of input parameters from C# program
        self.t = 0
    #? Marginal calculation   
    def calc_budget(self, current_bids):
        #new round
        budget =0
        N_S = (np.array(current_bids)[:,3]).sum() #model quality at 3 
        if N_S ==0:
            return 0

        #use log(1+x) for now
        budget = self.budget * math.log(1+self.DataQ_sum(current_bids))

        self.last_budget = budget

        return budget
    #3=model quality
    def DataQ_sum(self, current_bids):
        return np.sum(np.array(current_bids)[:,3])

#Reference and Credits: Base incentive schemes coded are referenced and implemented with reference to the following paper:
#H. Yu et al., "A Fairness-aware Incentive Scheme for Federated Learning," presented at the Proceedings of the AAAI/ACM Conference on AI, Ethics, and Society, New York, NY, USA, 2020.
#[Online]. Available: https://doi.org/10.1145/3375627.3375840.
#-------------------------------------------Base Incentive Schemes-----------------------------------------------#
#- If designing own incentive scheme:
#    Essential Methods: __init__ & payout_calculation(self, current_bids) for calculation of payout
#       -current_bids comes in the form of a list & output of this method should be an **array** for it to work properly.
#       -format of single row in list (bids): id, data quantity, data quality, model quality, placeholder (for future implementation if needed) , cost
#    Add class to list at the end of this file, scheme initialization if not following fixed parameters follows the form of i%number schemes
#       -i.e. if number of schemes is 4, federation 1's scheme is 0%4, federation 2's scheme is 1%4, ..., federation n's scheme is n%4
    
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
        
        S=0
        mu = [0] * self.Env.N_players
        mu_real = [0] * self.Env.N_players
        budget = self.Env.budget
        for one_bid in current_bids:
            id = one_bid[0]
            D = one_bid[1]
            q = one_bid[2]
            c = one_bid[3]
            if c==0 and D==0:
                continue
            mu[id-1]=self.Env.calc_budget(np.array([one_bid]))
            S+=mu[id-1]

        id_list = np.array(current_bids)[:, 0].astype(int)
        for id in id_list:
            if S==0 :
                mu_real[id-1]=0
            else:
                mu_real[id-1]=mu[id-1]*budget/S

        return np.array(mu_real)
        
##        for index, one_bid in enumerate(current_bids):
##            data_quality = one_bid[1]
##            data_quantity = one_bid[2]
##            model_quality = one_bid[3]
##            c = one_bid[5]
##            if c == 0:
##                continue
##            
##            payout_list[index] = (model_quality/S) * self.Env.budget    
##        return np.array(payout_list)

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
        
        S = sum(np.array(current_bids)[:,3])
        #print("Sum:", N_total_model)

        for index, one_bid in enumerate(current_bids):
            data_quality = one_bid[1]
            data_quantity = one_bid[2]
            model_quality = one_bid[3]
            c = one_bid[5]

            if c ==0:
                continue            
            
            payout_list[index] = (model_quality/S) * self.Env.budget #((1.0*modelquality)/N_total_model) * self.Env.budget 
            #print(modelquality)
            
        return np.array(payout_list)#payout_list.tolist()

#Shapley calculation
class FL_shapley(federal):
    def __init__(self,id,Env):
        federal.__init__(self,id,Env)
        self.Name='Shapley'
        self.last_mu=[]
        self.m=100 #no of iterations
        
    def payout_calculation(self, current_bids):
        N_participants = sum(np.array(current_bids)[:,1]>0) #No of actual participants
        #print(N_participants)
        payout_list=[]
        payout_list = np.zeros(self.Env.N_players)

        mu = np.zeros(self.Env.N_players)
        o = deepcopy(current_bids)

        #Shapley random sampling of m size
        for i in range(self.m):
            np.random.shuffle(o) #shuffle randomly 
            mu+=self.sample_union_share(np.array(o),-1, self.Env.budget) #array returned
            
        for i in range(self.Env.N_players):
            mu[i]=max(0,mu[i])

        S=mu.sum()
        #print("S ->" ,S)
        payout_list=mu*self.Env.budget/S
            
        return payout_list
    def sample_union_share(self,o,t, budget):
        mu_sample = np.zeros(self.Env.N_players)
        pay_pre=0
        budget_now =0
        for index, one_bid in enumerate(o):
            id = one_bid[0].astype(int) #id starts from 1
            D = one_bid[1]
            q = one_bid[2]
            m = one_bid[3]
            c = one_bid[5]
            if c == 0:
                continue
            N_S = o[:,1].sum()
            if N_S ==0:
                return 0
            #o = copy of current_bid
            budget_now =  self.Env.calc_budget(o[0:(index+1),:]) #(budget * np.sum(o[0:(index+1),3]))
            mu_sample[id-1] = (budget_now-pay_pre) / self.m
            pay_pre = budget_now

        #print("MU Sample : ",mu_sample)
        return np.array(mu_sample)

class FL_union(federal):
    def __init__(self,id,Env):
        federal.__init__(self,id,Env)
        self.Name='Union'
        self.last_mu=[]

    def payout_calculation(self, current_bids):
        N_participants = sum(np.array(current_bids)[:,1]>0)
        S = 0
        mu = [0] * self.Env.N_players
        mu_real = [0] * self.Env.N_players
        pay_now=0
        pay_pre=0
        N_total_model = sum(np.array(current_bids)[:,3])
        
        for index, one_bid in enumerate(current_bids):
            id = int(one_bid[0])
            D = one_bid[1]
            q = one_bid[2]
            m = one_bid[3]
            c = one_bid[5]
            if c == 0:
                continue
            #cur = np.sum(np.array(current_bids)[0:(index+1),3]) / (N_participants*1.0)
            #print(np.array(current_bids)[0:(index+1),:])
            pay_now = self.Env.calc_budget(np.array(current_bids)[0:(index+1),:]) #cur

            mu[id-1] = max(0,(pay_now-pay_pre))
            pay_pre = pay_now

            S += mu[id-1] 
            #print("S ->" ,S)
        id_list = np.array(current_bids)[:, 0].astype(int)
        for id in id_list:
            #print(mu[id-1])
            if S==0:
                mu_real[id-1] = 0
            else:
                mu_real[id-1] = mu[id-1] * self.Env.budget/S

        return np.array(mu_real)

class FL_Lyapunov_marginal(federal):
    def __init__(self,id,Env = None):
        federal.__init__(self,id,Env)
        self.Name='Lyapunov Marginal'
        #para paper
        if(self.Env!=None):
            self.Y=[0]*self.Env.N_players
            self.Q=[0]*self.Env.N_players
            self.S = 0
            self.Cost_History = np.zeros((self.Env.N_players,1,1)).tolist()

    def payout_calculation(self, current_bids):
        print(self.Env.t)
        #print(self.Cost_History)
        budget = self.Env.budget
        mini_budget = budget/ self.Env.R2T
        S= 0
        mu = [0]*self.Env.N_players
        mu_real_R = [0] * self.Env.N_players
        mu_real = [0]*self.Env.N_players
        marginal_value_q = [0]* self.Env.N_players
        t=self.Env.t
        

        for rnd in range(self.Env.R2T):
            S=0
            c_list = [0]*self.Env.N_players
            for index, one_bid in enumerate(current_bids):
                id = int(one_bid[0])
                D = one_bid[1]
                q = one_bid[2]
                D_q = one_bid[3] #Model quality 
                #initial
                if rnd ==0:
                    c = one_bid[5] #Cost of participation
                    if c>0 and D_q>0:
                        if t ==0:
                            #print("id" ,id)
                            self.Cost_History[id-1][0][0] = c
                        else:
                            self.Cost_History[id-1][0].append(c)

                else:
                    c=0

                c_list[id-1] = c

                if rnd == 0:
                    less_bids = deepcopy(current_bids)
                    less_bids[index] = np.zeros(self.Env.N_parameters)
                    marginal_value_q[id-1] = max(0, self.Env.calc_budget(current_bids) - self.Env.calc_budget(less_bids))
                    #print(id-1, " . marginal_value + ",margval)

                mu[id-1] = 0.5 * (self.Env.w * marginal_value_q[id-1] + self.Y[id-1] + c+self.Q[id-1]+self.lamb(id-1))
                #print(mu[id-1])
                S+=mu[id-1]
                #print("S ->",S)

            id_list = np.array(current_bids)[:,0].astype(int)
            for id in id_list:
                #print(id,". Mu :",mu[id-1])
                if S==0:
                    mu_real_R[id-1] = 0
                else:
                    mu_real_R[id-1] = mu[id-1] * mini_budget/S #mini_budget= budget/R2T

                mu_real[id-1]+= mu_real_R[id-1]
                self.Y[id-1] = max(self.Y[id-1]+c_list[id-1] - mu_real_R[id-1],0)
                self.Q[id-1] = max(self.Q[id-1]+self.lamb(id-1)- mu_real_R[id-1],0)

        #end of rnd range
                
        return np.array(mu_real)
    
    #id = id-1
    def lamb(self,id):
        if self.Y[id]>0:
            if self.Env.lya_lamb_choice==0:
                return np.mean(self.Cost_History[id]) / self.Env.R2T
            elif self.Env.lya_lamb_choice==1:
                return 10.0
        return 0



list_fl = []
list_fl.append(FL_equal(0,None))
list_fl.append(FL_indi(0,None))
list_fl.append(FL_linear(0,None))
list_fl.append(FL_union(0,None))
list_fl.append(FL_shapley(0,None))
list_fl.append(FL_Lyapunov_marginal(0,None))

print(list_fl)
