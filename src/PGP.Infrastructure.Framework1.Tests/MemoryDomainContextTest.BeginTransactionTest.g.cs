using Microsoft.VisualStudio.TestTools.UnitTesting;
using PGP.Infrastructure.Framework.DomainContext;
using Microsoft.Pex.Framework.Generated;
// <copyright file="MemoryDomainContextTest.BeginTransactionTest.g.cs">Copyright ©  2015</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace PGP.Infrastructure.Framework.DomainContext.Tests
{
    public partial class MemoryDomainContextTest
    {

[TestMethod]
[PexGeneratedBy(typeof(MemoryDomainContextTest))]
public void BeginTransactionTest815()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      MemoryDomainContext memoryDomainContext;
      memoryDomainContext = MemoryDomainContextFactory.Create();
      disposables.Add((IDisposable)memoryDomainContext);
      this.BeginTransactionTest(memoryDomainContext);
      disposables.Dispose();
      Assert.IsNotNull((object)memoryDomainContext);
      Assert.AreEqual<bool>(false, memoryDomainContext.HasPendingTransaction);
    }
}

[TestMethod]
[PexGeneratedBy(typeof(MemoryDomainContextTest))]
public void BeginTransactionTest323()
{
    using (PexDisposableContext disposables = PexDisposableContext.Create())
    {
      MemoryDomainContext memoryDomainContext;
      memoryDomainContext = MemoryDomainContextFactory.CreateWithTransaction();
      disposables.Add((IDisposable)memoryDomainContext);
      this.BeginTransactionTest(memoryDomainContext);
      disposables.Dispose();
      Assert.IsNotNull((object)memoryDomainContext);
      Assert.AreEqual<bool>(false, memoryDomainContext.HasPendingTransaction);
    }
}
    }
}
