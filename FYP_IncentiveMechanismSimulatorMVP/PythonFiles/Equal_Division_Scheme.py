import numpy as np

class scheme(object):
    def __init__(self,name):
        self.name = name

    def giveName(self,name):
        self.name = name
    
    def whatIsName(self):
        return self.name


