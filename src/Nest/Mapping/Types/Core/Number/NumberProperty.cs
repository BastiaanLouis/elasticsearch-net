using System;
using System.Diagnostics;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface INumberProperty : IDocValuesProperty
	{
		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		double? NullValue { get; set; }

		[JsonProperty("include_in_all")]
		[Obsolete("Scheduled to be removed in 6.0")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }

		[JsonProperty("scaling_factor")]
		double? ScalingFactor { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class NumberProperty : DocValuesPropertyBase, INumberProperty
	{
		public NumberProperty() : base(FieldType.Float) { }
		public NumberProperty(NumberType type) : base(type.ToFieldType()) { }
		[Obsolete("Please use overload taking NumberType")]
		protected NumberProperty(string type) : base(type) { }

		public bool? Index { get; set; }
		public double? Boost { get; set; }
		public double? NullValue { get; set; }
		[Obsolete("Scheduled to be removed in 6.0")]
		public bool? IncludeInAll { get; set; }
		public bool? IgnoreMalformed { get; set; }
		public bool? Coerce { get; set; }
		public INumericFielddata Fielddata { get; set; }
		public double? ScalingFactor { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class NumberPropertyDescriptorBase<TDescriptor, TInterface, T>
		: DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, INumberProperty
		where TDescriptor : NumberPropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, INumberProperty
		where T : class
	{
		protected NumberPropertyDescriptorBase() : base(FieldType.Float) { }

		[Obsolete("Please use overload taking FieldType")]
		protected NumberPropertyDescriptorBase(string type) : base(type) { }

		bool? INumberProperty.Index { get; set; }
		double? INumberProperty.Boost { get; set; }
		double? INumberProperty.NullValue { get; set; }
		[Obsolete("Scheduled to be removed in 6.0")]
		bool? INumberProperty.IncludeInAll { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }
		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }
		double? INumberProperty.ScalingFactor { get; set; }

		public TDescriptor Type(NumberType type) => Assign(a => a.Type = type.GetStringValue());

		public TDescriptor Index(bool index) => Assign(a => a.Index = index);

		public TDescriptor Boost(double boost) => Assign(a => a.Boost = boost);

		public TDescriptor NullValue(double nullValue) => Assign(a => a.NullValue = nullValue);

		[Obsolete("Scheduled to be removed in 6.0")]
		public TDescriptor IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);

		public TDescriptor IgnoreMalformed(bool ignoreMalformed = true) => Assign(a => a.IgnoreMalformed = ignoreMalformed);

		public TDescriptor Coerce(bool coerce = true) => Assign(a => a.Coerce = coerce);

		public TDescriptor Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));

		public TDescriptor ScalingFactor(double scalingFactor) => Assign(a => a.ScalingFactor = scalingFactor);
	}

	public class NumberPropertyDescriptor<T>
		: NumberPropertyDescriptorBase<NumberPropertyDescriptor<T>, INumberProperty, T>, INumberProperty
		where T : class
	{
	}
}
