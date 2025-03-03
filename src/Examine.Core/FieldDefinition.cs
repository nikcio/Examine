using System;

namespace Examine
{
    /// <summary>
    /// Defines a field to be indexed
    /// </summary>
    public struct FieldDefinition : IEquatable<FieldDefinition>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public FieldDefinition(string name, string type)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(type));
            Name = name;
            Type = type;
        }

        /// <summary>
        /// The name of the index field
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The data type
        /// </summary>
        public string Type { get; }

        /// <inheritdoc/>
        public bool Equals(FieldDefinition other) => string.Equals(Name, other.Name) && string.Equals(Type, other.Type);

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FieldDefinition definition && Equals(definition);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ Type.GetHashCode();
            }
        }

        /// <inheritdoc/>
        public static bool operator ==(FieldDefinition left, FieldDefinition right) => left.Equals(right);

        /// <inheritdoc/>
        public static bool operator !=(FieldDefinition left, FieldDefinition right) => !left.Equals(right);
    }
}
