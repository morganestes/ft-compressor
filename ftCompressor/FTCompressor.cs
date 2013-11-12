//
//  FTCompressor.cs
//
//  Author:
//       Morgan Estes <morgan.estes@gmail.com>
//
//  Copyright (c) 2013 Morgan Estes
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Text;

namespace MorganEstes
{
	class FTCompressor
	{
		public static string CompressString(string s)
		{
			// pad the string if necessary
			if ((s.Length & 1) != 0) {
				s += " ";
			}

			// compress the string
			int intStr1, intStr2;
			StringBuilder strOut = new StringBuilder();

			for (int i = 0; i < s.Length; i += 2)
			{
				// Char.ConvertToUtf32(string, pos) is JS string.charCodeAt(pos)
				intStr1 = Char.ConvertToUtf32(s, i) * 256;
				intStr2 = Char.ConvertToUtf32(s, i + 1);

				// Char.ConvertFromUtf32(int) is JS String.fromCharCode(int)
				strOut.Append(Char.ConvertFromUtf32(intStr1 + intStr2));
			}

			// Prepend the snowman character to the string
			return strOut.Insert(0, Convert.ToChar(9731)).ToString();
		}

		public static string DecompressString(string s)
		{
			// If not prefixed with a snowman, just return the (already uncompressed) string
			if (s [0] != Convert.ToChar (9731)) {
				return s;
			}

			int m, n;
			string strOut = "";

			for (int i = 1, l = s.Length; i < l; i++) {
				n = Char.ConvertToUtf32 (s, i);
				m = (int)Math.Floor ((decimal)n / 256);

				strOut += (Char.ConvertFromUtf32 (m) + Char.ConvertFromUtf32 (n % 256));
			}

			return strOut;
		}
	}
}
