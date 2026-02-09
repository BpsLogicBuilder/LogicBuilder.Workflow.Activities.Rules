using System;
using System.Collections.Generic;
using System.Reflection;

namespace LogicBuilder.Workflow.Activities.Rules.UnitTests.Rules
{
    public class SimpleRunTimeTypeProviderTest
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidAssembly_InitializesProvider()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;

            // Act
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Assert
            Assert.NotNull(provider);
            Assert.Equal(assembly, provider.LocalAssembly);
        }

        [Fact]
        public void Constructor_WithAssemblyAndReferences_InitializesProvider()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var references = new List<Assembly> { typeof(object).Assembly };

            // Act
            var provider = new SimpleRunTimeTypeProvider(assembly, references);

            // Assert
            Assert.NotNull(provider);
            Assert.Equal(assembly, provider.LocalAssembly);
        }

        #endregion

        #region GetType Tests

        [Fact]
        public void GetType_WithValidTypeInLocalAssembly_ReturnsType()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);
            var typeName = typeof(SimpleRunTimeTypeProviderTest).FullName;

            // Act
            var type = provider.GetType(typeName);

            // Assert
            Assert.NotNull(type);
            Assert.Equal(typeof(SimpleRunTimeTypeProviderTest), type);
        }

        [Fact]
        public void GetType_WithValidTypeInMscorlib_ReturnsType()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var type = provider.GetType("System.String");

            // Assert
            Assert.NotNull(type);
            Assert.Equal(typeof(string), type);
        }

        [Fact]
        public void GetType_WithValidTypeInReferencedAssembly_ReturnsType()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);
            var typeName = typeof(RuleSet).FullName;

            // Act
            var type = provider.GetType(typeName);

            // Assert
            Assert.NotNull(type);
            Assert.Equal(typeof(RuleSet), type);
        }

        [Fact]
        public void GetType_WithInvalidType_ReturnsNull()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var type = provider.GetType("NonExistent.Type.Name");

            // Assert
            Assert.Null(type);
        }

        [Fact]
        public void GetType_WithThrowOnError_ThrowsTypeLoadException()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act & Assert
            Assert.Throws<TypeLoadException>(() => provider.GetType("NonExistent.Type.Name", true));
        }

        [Fact]
        public void GetType_WithAssemblyQualifiedName_ReturnsType()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);
            var typeName = typeof(string).AssemblyQualifiedName;

            // Act
            var type = provider.GetType(typeName);

            // Assert
            Assert.NotNull(type);
            Assert.Equal(typeof(string), type);
        }

        [Fact]
        public void GetType_WithProvidedReferences_FindsTypeInReferences()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var references = new List<Assembly> { typeof(RuleSet).Assembly };
            var provider = new SimpleRunTimeTypeProvider(assembly, references);
            var typeName = typeof(RuleSet).FullName;

            // Act
            var type = provider.GetType(typeName);

            // Assert
            Assert.NotNull(type);
            Assert.Equal(typeof(RuleSet), type);
        }

        [Fact]
        public void GetType_WithNestedType_ReturnsType()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);
            var nestedTypeName = typeof(TestHelperClass.NestedClass).FullName;

            // Act
            var type = provider.GetType(nestedTypeName);

            // Assert
            Assert.NotNull(type);
            Assert.Equal(typeof(TestHelperClass.NestedClass), type);
        }

        #endregion

        #region GetTypes Tests

        [Fact]
        public void GetTypes_ReturnsTypesFromLocalAssembly()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var types = provider.GetTypes();

            // Assert
            Assert.NotNull(types);
            Assert.NotEmpty(types);
            Assert.Contains(typeof(SimpleRunTimeTypeProviderTest), types);
        }

        [Fact]
        public void GetTypes_ReturnsTypesFromReferencedAssemblies()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var types = provider.GetTypes();

            // Assert
            Assert.NotNull(types);
            Assert.NotEmpty(types);
            // Should contain types from referenced assemblies
            Assert.True(types.Length > assembly.GetTypes().Length);
        }

        [Fact]
        public void GetTypes_WithProvidedReferences_IncludesReferencedTypes()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var references = new List<Assembly> { typeof(RuleSet).Assembly };
            var provider = new SimpleRunTimeTypeProvider(assembly, references);

            // Act
            var types = provider.GetTypes();

            // Assert
            Assert.NotNull(types);
            Assert.Contains(typeof(RuleSet), types);
        }

        #endregion

        #region LocalAssembly Tests

        [Fact]
        public void LocalAssembly_ReturnsCorrectAssembly()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var localAssembly = provider.LocalAssembly;

            // Assert
            Assert.NotNull(localAssembly);
            Assert.Equal(assembly, localAssembly);
        }

        #endregion

        #region ReferencedAssemblies Tests

        [Fact]
        public void ReferencedAssemblies_ReturnsReferencedAssemblies()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var references = provider.ReferencedAssemblies;

            // Assert
            Assert.NotNull(references);
            Assert.NotEmpty(references);
        }

        [Fact]
        public void ReferencedAssemblies_WithProvidedReferences_ReturnsProvidedReferences()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var providedReferences = new List<Assembly> { typeof(RuleSet).Assembly };
            var provider = new SimpleRunTimeTypeProvider(assembly, providedReferences);

            // Act
            var references = provider.ReferencedAssemblies;

            // Assert
            Assert.NotNull(references);
            Assert.Contains(typeof(RuleSet).Assembly, references);
        }

        [Fact]
        public void ReferencedAssemblies_CalledMultipleTimes_ReturnsSameCollection()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var references1 = provider.ReferencedAssemblies;
            var references2 = provider.ReferencedAssemblies;

            // Assert
            Assert.Same(references1, references2);
        }

        [Fact]
        public void ReferencedAssemblies_DoesNotIncludeRootAssembly()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var references = provider.ReferencedAssemblies;

            // Assert
            Assert.DoesNotContain(assembly, references);
        }

        #endregion

        #region ReferencedAssembliesLookup Tests

        [Fact]
        public void ReferencedAssembliesLookup_ReturnsDictionary()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var lookup = provider.ReferencedAssembliesLookup;

            // Assert
            Assert.NotNull(lookup);
            Assert.NotEmpty(lookup);
        }

        [Fact]
        public void ReferencedAssembliesLookup_ContainsAllReferencedAssemblies()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var lookup = provider.ReferencedAssembliesLookup;
            var references = provider.ReferencedAssemblies;

            // Assert
            Assert.Equal(references.Count, lookup.Count);
            foreach (var reference in references)
            {
                Assert.True(lookup.ContainsKey(reference.FullName));
                Assert.Equal(reference, lookup[reference.FullName]);
            }
        }

        [Fact]
        public void ReferencedAssembliesLookup_KeyedByFullName()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var lookup = provider.ReferencedAssembliesLookup;

            // Assert
            foreach (var kvp in lookup)
            {
                Assert.Equal(kvp.Value.FullName, kvp.Key);
            }
        }

        #endregion

        #region TypeLoadErrors Tests

        [Fact]
        public void TypeLoadErrors_ReturnsNull()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act
            var errors = provider.TypeLoadErrors;

            // Assert
            Assert.Null(errors);
        }

        #endregion

        #region Event Tests

        [Fact]
        public void TypesChanged_CanSubscribe()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);
            var eventRaised = false;
            void handler(object? sender, EventArgs args) => eventRaised = true;

            // Act
            provider.TypesChanged += handler;
            _ = provider.TypeLoadErrors; // This triggers the event

            // Assert
            Assert.True(eventRaised);
        }

        [Fact]
        public void TypeLoadErrorsChanged_CanSubscribe()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);
            var eventRaised = false;
            void handler(object? sender, EventArgs args) => eventRaised = true;

            // Act
            provider.TypeLoadErrorsChanged += handler;
            _ = provider.TypeLoadErrors; // This triggers the event

            // Assert
            Assert.True(eventRaised);
        }

        #endregion

        #region Integration Tests

        [Fact]
        public void Provider_CompleteWorkflow_WorksCorrectly()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var provider = new SimpleRunTimeTypeProvider(assembly);

            // Act & Assert - Get local assembly
            Assert.Equal(assembly, provider.LocalAssembly);

            // Get type from local assembly
            var localType = provider.GetType(typeof(SimpleRunTimeTypeProviderTest).FullName);
            Assert.NotNull(localType);
            Assert.Equal(typeof(SimpleRunTimeTypeProviderTest), localType);

            // Get type from system assembly
            var systemType = provider.GetType("System.String");
            Assert.NotNull(systemType);
            Assert.Equal(typeof(string), systemType);

            // Get type from referenced assembly
            var referencedType = provider.GetType(typeof(RuleSet).FullName);
            Assert.NotNull(referencedType);
            Assert.Equal(typeof(RuleSet), referencedType);

            // Get all types
            var types = provider.GetTypes();
            Assert.NotEmpty(types);
            Assert.Contains(typeof(SimpleRunTimeTypeProviderTest), types);

            // Get referenced assemblies
            var references = provider.ReferencedAssemblies;
            Assert.NotEmpty(references);

            // Get lookup
            var lookup = provider.ReferencedAssembliesLookup;
            Assert.NotEmpty(lookup);
        }

        [Fact]
        public void Provider_WithProvidedReferences_CompleteWorkflow()
        {
            // Arrange
            var assembly = typeof(SimpleRunTimeTypeProviderTest).Assembly;
            var references = new List<Assembly> 
            { 
                typeof(RuleSet).Assembly,
                typeof(object).Assembly 
            };
            var provider = new SimpleRunTimeTypeProvider(assembly, references);

            // Act & Assert
            Assert.Equal(assembly, provider.LocalAssembly);

            // Verify provided references are available
            var providerReferences = provider.ReferencedAssemblies;
            Assert.Contains(typeof(RuleSet).Assembly, providerReferences);

            // Get types from provided references
            var type = provider.GetType(typeof(RuleSet).FullName);
            Assert.NotNull(type);
            Assert.Equal(typeof(RuleSet), type);

            // Get all types includes references
            var types = provider.GetTypes();
            Assert.Contains(typeof(RuleSet), types);
        }

        #endregion

        #region Helper Classes

        // Helper class for nested type testing
        public class TestHelperClass
        {
            public class NestedClass
            {
                public string? TestProperty { get; set; }
            }
        }

        #endregion
    }
}