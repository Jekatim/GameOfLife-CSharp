using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife_CSharp
{
    class Config
    {
        private static int mWorldWidth = 10;
        private static int mWorldHeight = 10;
        private static int mSeed = 5;

	    Config(int wnd_width, int wnd_height)
        {
            mWorldWidth = wnd_width;
	        mWorldHeight = wnd_height;
        }

	    public static int getWidth(){return mWorldWidth;}
	    public static int getHeight(){return mWorldHeight;}
	    public static int getSeed(){return mSeed;}

	    public static void setWidth(int w){mWorldWidth = w;}
	    public static void setHeight(int h){mWorldHeight = h;}
	    public static void setSeed(int s){mSeed = s;}

    }
}