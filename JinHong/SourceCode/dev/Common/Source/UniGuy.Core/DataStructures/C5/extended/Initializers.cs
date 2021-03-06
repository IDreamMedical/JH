/*
 Copyright (c) 2003-2006 Niels Kokholm and Peter Sestoft
 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:
 
 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.
 
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/

// C5 and C# 3.0 collection initializers

// Compile with 
//   csc /r:C5.dll Initializers.cs 

using System;
using UniGuy.Core.DataStructures.C5;
using SCG = System.Collections.Generic;

namespace UniGuy.Core.DataStructures.C5.Initializers
{
  class MyTest
  {
    public static void Main(String[] args)
    {
      var list = new HashSet<int> { 2, 3, 5, 7, 11 };
      foreach (var x in list) 
	Console.WriteLine(x);
      var dict = new HashDictionary<int,String> { 
	{ 2, "two" },
	{ 3, "three" }
      };
      foreach (var x in dict) 
	Console.WriteLine(x.Value);
    }
  }
}
