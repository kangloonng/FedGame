from SchemeBase import scheme as schemeBase
import numpy as np

class scheme(object):
    schemeName = "Equal Division Scheme"
    
    def calculate(self):
        return schemeBase.numParticipants

operator = schemeBase("Alex",1,1,1,1)
scheme.calculate()
