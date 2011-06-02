﻿using System;

namespace VGMToolbox.util
{
    public class MathUtil
    {
        public static string Evaluate(string expression)
        {
            object results = null;
            string outputValue;

            try
            {
                results = JSUtil.Util.Eval(expression);
                outputValue = Convert.ToString((long)Convert.ToUInt64(results), 10);
            }
            catch (Exception ex)
            {
                outputValue = null;
            }

            return outputValue;
        }
    }

}
