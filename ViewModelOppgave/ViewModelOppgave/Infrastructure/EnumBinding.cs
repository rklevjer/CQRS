using System;
using System.Resources;

namespace ViewModelOppgave.Infrastructure
{
	public class EnumBinding<T> // where T is Enum (But compiler doesn't accept this
	{
		readonly string m_name;
		readonly T m_value;

		public string Name { get { return m_name; } }

		public T Value { get { return m_value; } }

		public char ShortName
		{
			get
			{
				if (!string.IsNullOrEmpty(m_name))
				{
					return m_name[0];
				}
				else
				{
					return ' ';
				}
			}
		}

		public override string ToString()
		{
			return Name;
		}

		public static implicit operator T(EnumBinding<T> eb)
		{
			return eb.Value;
		}

		public EnumBinding(T value) : this(value.ToString(), value)
		{
		}

		public EnumBinding(string name, T value)
		{
			if (name == "Blank")
			{
				m_name = String.Empty;
			}
			else
			{
				m_name = name;
			}
			m_value = value;
		}

		public override bool Equals(object obj)
		{
			EnumBinding<T> other = obj as EnumBinding<T>;
			if (other == null)
			{
				return false;
			}

			return Object.Equals(this.Value, other.Value);
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public static EnumBinding<T>[] GetEnumList()
		{
			return GetEnumList(null);
		}

		public static EnumBinding<T>[] GetEnumList(ResourceManager resourceManager)
		{

			Type enumType = FindEnumType();

			//If type can be null, add empty null item to list!
			EnumBinding<T>[] retVal;
			int offset = 0;
			System.Array values = Enum.GetValues(enumType);

			if (default(T) == null)
			{
				retVal = new EnumBinding<T>[values.Length + 1];
				retVal[0] = new EnumBinding<T>(String.Empty, default(T));
				offset = 1;
			}
			else
			{
				retVal = new EnumBinding<T>[values.Length];
			}

			int i = offset;
			foreach (T value in values)
			{
				retVal[i] = CreateEnumBinding(resourceManager, value);
				i++;
			}
			return retVal;
		}

		public static EnumBinding<T> CreateEnumBinding(ResourceManager resourceManager,
			T value)
		{
			System.Type enumType = FindEnumType();

			string name = null;
			if (resourceManager != null && value != null)
			{
				name = resourceManager.GetString(enumType.FullName + "." + Enum.GetName(enumType, value));
			}
			if (name == null && value != null)
			{
				name = Enum.GetName(enumType, value);
			}
			if (name == null)
			{
				name = "";
			}
			return new EnumBinding<T>(name, value);
		}

		private static System.Type FindEnumType()
		{
			System.Type enumType;
			if (default(T) == null)
			{
				enumType = System.Nullable.GetUnderlyingType(typeof(T));
			}
			else
			{
				enumType = typeof(T);
			}

			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Supplied type is not an enum!");
			}
			return enumType;
		}

		/// <summary>
		/// Used to get EnumBinding<Enum?> from EnumBinding<Enum>
		/// </summary>
		/// <typeparam name="TE"></typeparam>
		/// <returns></returns>
		public EnumBinding<TE?> ToNullableEnumBinding<TE>() where TE : struct, T
		{
			dynamic value = Value;
			return new EnumBinding<TE?>(Name, value);
		}

	}
}
