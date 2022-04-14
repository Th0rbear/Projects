# A class that showcases the number of integrand evaluations for python/numpy's integration routines.
from scipy.integrate import quad
import numpy as np

def invSqrt(x):
    return 1/np.sqrt(x)

def logInvSqrt(x):
    return np.log(x)/np.sqrt(x)

a = 0
b = 1
δ = 1e-3
ε = 1e-3

res1, abserr1, infodict1 = quad(invSqrt, a, b, full_output=1, epsabs=δ, epsrel=ε)
res2, abserr2, infodict2 = quad(logInvSqrt, a, b, full_output=1, epsabs=δ, epsrel=ε)

print("1/Sqrt(x) python #calls = ", infodict1["neval"])
print("log(x)/Sqrt(x) python #calls = ", infodict2["neval"])
