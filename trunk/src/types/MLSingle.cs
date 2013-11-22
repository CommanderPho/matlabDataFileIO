using System;
using csmatio.common;

namespace csmatio.types
{
	/// <summary>
	/// This class represents an single-precision (32-bit) floating-point array (matrix)
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLSingle : MLNumericArray<float>
	{
		/// <summary>
		/// Normally this constructor is used only by <c>MatFileReader</c> and <c>MatFileWriter</c>
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		/// <param name="Type">Array type: here <c>mxSINGLE_CLASS</c></param>
		/// <param name="Attributes">Array flags</param>
		public MLSingle(string Name, int[] Dims, int Type, int Attributes)
			: base(Name, Dims, Type, Attributes) { }

		/// <summary>
		/// Create a <c>MLSingle</c> array with given name and dimensions.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		public MLSingle(string Name, int[] Dims)
			: base(Name, Dims, MLArray.mxSINGLE_CLASS, 0) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="vals">One-dimensional array of floats, packed by columns</param>
		/// <param name="m">Number of rows</param>
		public MLSingle(string Name, float[] vals, int m)
			: base(Name, MLArray.mxSINGLE_CLASS, vals, m) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D real matrix from <c>byte[][]</c>.
		/// </summary>
		/// <remarks>Note: Array is converted to <c>byte[]</c></remarks>
		/// <param name="Name">Array name</param>
		/// <param name="vals">Two-dimensional array of values</param>
		public MLSingle(string Name, float[][] vals)
			: this(Name, Float2DToFloat(vals), vals.Length) { }

		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">One-dimensional array of float for <i>real</i> values, packed by columns</param>
		/// <param name="Imag">One-dimensional array of float for <i>imaginary</i> values, packed by columns</param>
		/// <param name="M">Number of rows</param>
		public MLSingle(string Name, float[] Real, float[] Imag, int M)
			: base(Name, MLArray.mxSINGLE_CLASS, Real, Imag, M) { }


		/// <summary>
		/// <a href="http://math.nist.gov/javanumerics/jama/">Jama</a> [math.nist.gov] style:
		/// construct a 2D imaginary matrix from a one-dimensional packed array.
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Real">array of arrays of float for <i>real</i> values</param>
		/// <param name="Imag">array of arrays of float for <i>imaginary</i> values</param>
		public MLSingle(string Name, float[][] Real, float[][] Imag)
			: this(Name, Float2DToFloat(Real), Float2DToFloat(Imag), Real.Length) { }

		/// <summary>
		/// Creates a generic 1D array.
		/// </summary>
		/// <param name="m">The number of columns in the array</param>
		/// <param name="n">The number of rows in the array</param>
		/// <returns>A generic array.</returns>
		public override float[] CreateArray(int m, int n)
		{
			return new float[m * n];
		}

		/// <summary>
		/// Gets a two-dimensional array.
		/// </summary>
		/// <returns>2D real array.</returns>
		public float[][] GetArray()
		{
			float[][] result = new float[M][];

			for (int m = 0; m < M; m++)
			{
				result[m] = new float[N];

				for (int n = 0; n < N; n++)
				{
					result[m][n] = GetReal(m, n);
				}
			}
			return result;
		}

		/// <summary>
		/// Converts float[][] to float[]
		/// </summary>
		/// <param name="dd"></param>
		/// <returns></returns>
		private static float[] Float2DToFloat(float[][] dd)
		{
			float[] d = new float[dd.Length * dd[0].Length];
			for (int n = 0; n < dd[0].Length; n++)
			{
				for (int m = 0; m < dd.Length; m++)
				{
					d[m + n * dd.Length] = dd[m][n];
				}
			}
			return d;
		}

		/// <summary>
		/// Gets the number of bytes allocated for a type
		/// </summary>
		unsafe public override int GetBytesAllocated
		{
			get
			{
				return sizeof(float);
			}
		}
		/// <summary>
		/// Builds a numeric object from a byte array.
		/// </summary>
		/// <param name="bytes">A byte array containing the data.</param>
		/// <returns>A numeric object</returns>
		public override object BuildFromBytes(byte[] bytes)
		{
			if (bytes.Length != GetBytesAllocated)
				throw new ArgumentException(
					"To build from a byte array, I need an array of size: " + GetBytesAllocated);
			return BitConverter.ToSingle(bytes, 0);
		}

		/// <summary>
		/// Gets the type of numeric object that this byte storage represents
		/// </summary>
		public override Type GetStorageType
		{
			get
			{
				return typeof(float);
			}
		}

		/// <summary>
		/// Gets a <c>byte[]</c> for a particular float value.
		/// </summary>
		/// <param name="val">The float value</param>
		/// <returns>A byte array</returns>
		public override byte[] GetByteArray(object val)
		{
			return BitConverter.GetBytes((float)val);
		}
	}
}
