// <copyright file="PexAssemblyInfo.cs">Copyright ©  2015</copyright>
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("PGP.Infrastructure.Framework")]
[assembly: PexInstrumentAssembly("KissSpecifications")]
[assembly: PexInstrumentAssembly("HelperSharp")]
[assembly: PexInstrumentAssembly("System.Core")]
[assembly: PexInstrumentAssembly("Skahal.Infrastructure.Framework")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "KissSpecifications")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "HelperSharp")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Core")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Skahal.Infrastructure.Framework")]

