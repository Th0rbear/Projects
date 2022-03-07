using static System.Math;
public static class sfuns{

	public static double gamma(double x){
		///single precision gamma function (Gergo Nemes, from Wikipedia)
		if(x<0)return PI/Sin(PI*x)/gamma(1-x);
		if(x<9)return gamma(x+1)/x;
		double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Exp(lngamma);
		}

	public static double ex(double x) {
		if(x<0) return 1/ex(-x);
		if(x>1.0/8) return Pow(ex(x/2),2);
		return 1+x*(1+x/2*(1+x/3*(1+x/4*(1+x/5*(1+x/6*(1+x/7*(1+x/8*(1+x/9*(1+x/10)))))))));
	}
				
}
