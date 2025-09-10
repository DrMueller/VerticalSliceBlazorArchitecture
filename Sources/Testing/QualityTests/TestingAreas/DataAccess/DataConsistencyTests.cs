//using FluentAssertions;
//using Microsoft.Extensions.DependencyInjection;
//using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories;
//using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;
//using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Quality;
//using VerticalSliceBlazorArchitecture.Testing.Common.Data;
//using Xunit;

//namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.DataAccess
//{
//    public class DataConsistencyTests(QualityTestFixture fixture) : QualityTestBase(fixture)
//    {
//        [Fact]
//        public async Task HavingMultipleUnitOfWork_FromMultipleFactories_SavingBoth_SavesBoth()
//        {
//            var benutzerEditBo1 = new BenutzerEditBo
//            {
//                BenutzerGruppeRefId = RefIdProvider.BenutzerGruppe.FeldkalenderUser,
//                ClientGroupCode = CodesProvider.ClientGroups.Neutral.Value,
//                EmailAddress = "Test1@gmx.ch",
//                FirstName = "Matthias",
//                LastName = "Müller",
//                MobilePhone = "079 762 47 77"
//            };

//            var benutzerEditBo2 = new BenutzerEditBo
//            {
//                BenutzerGruppeRefId = RefIdProvider.BenutzerGruppe.FeldkalenderUser,
//                ClientGroupCode = CodesProvider.ClientGroups.Neutral.Value,
//                EmailAddress = "Test2@gmx.ch",
//                FirstName = "Matthias",
//                LastName = "Müller",
//                MobilePhone = "079 762 47 77"
//            };

//            var uowFactory1 = AppFactory.Services.GetRequiredService<IUnitOfWorkFactory>();
//            var uowFactory2 = AppFactory.Services.GetRequiredService<IUnitOfWorkFactory>();

//            using var uow1 = uowFactory1.Create();
//            using var uow2 = uowFactory2.Create();

//            var benutzerRepo1 = uow1.GetRepository<IBenutzerRepository>();
//            var benutzerRepo2 = uow1.GetRepository<IBenutzerRepository>();

//            await benutzerRepo1.UpsertBenutzerAsync(benutzerEditBo1, CodesProvider.ClientGroups.Neutral);
//            await benutzerRepo2.UpsertBenutzerAsync(benutzerEditBo2, CodesProvider.ClientGroups.Neutral);

//            await uow1.SaveAsync();
//            await uow2.SaveAsync();

//            var dbContextFactory = (TestAppDbContextFactory)AppFactory.Services.GetRequiredService<IAppDbContextFactory>();
//            using var appDbContext = dbContextFactory.Create();

//            var actualBenutzer = appDbContext.DbSet<Benutzer>().AsNoTracking().ToList();
//            actualBenutzer.Count.Should().Be(2);
//        }

//        [Fact]
//        public async Task HavingMultipleUnitOfWork_FromMultipleFactories_SavingOne_SavesOne()
//        {
//            var benutzerEditBo1 = new BenutzerEditBo
//            {
//                BenutzerGruppeRefId = RefIdProvider.BenutzerGruppe.FeldkalenderUser,
//                ClientGroupCode = CodesProvider.ClientGroups.Neutral.Value,
//                EmailAddress = "Test1@gmx.ch",
//                FirstName = "Matthias",
//                LastName = "Müller",
//                MobilePhone = "079 762 47 77"
//            };

//            var benutzerEditBo2 = new BenutzerEditBo
//            {
//                BenutzerGruppeRefId = RefIdProvider.BenutzerGruppe.FeldkalenderUser,
//                ClientGroupCode = CodesProvider.ClientGroups.Neutral.Value,
//                EmailAddress = "Test2@gmx.ch",
//                FirstName = "Matthias",
//                LastName = "Müller",
//                MobilePhone = "079 762 47 77"
//            };

//            var uowFactory1 = AppFactory.Services.GetRequiredService<IUnitOfWorkFactory>();
//            var uowFactory2 = AppFactory.Services.GetRequiredService<IUnitOfWorkFactory>();

//            using var uow1 = uowFactory1.Create();
//            using var uow2 = uowFactory2.Create();

//            var benutzerRepo1 = uow1.GetRepository<IBenutzerRepository>();
//            var benutzerRepo2 = uow2.GetRepository<IBenutzerRepository>();

//            await benutzerRepo1.UpsertBenutzerAsync(benutzerEditBo1, CodesProvider.ClientGroups.Neutral);
//            await benutzerRepo2.UpsertBenutzerAsync(benutzerEditBo2, CodesProvider.ClientGroups.Neutral);

//            await uow1.SaveAsync();

//            var dbContextFactory = (TestAppDbContextFactory)AppFactory.Services.GetRequiredService<IAppDbContextFactory>();
//            using var appDbContext = dbContextFactory.Create();

//            var actualBenutzer = appDbContext.DbSet<Benutzer>().AsNoTracking().ToList();
//            actualBenutzer.Count.Should().Be(1);
//            actualBenutzer.Single().BenutzerEmail.Should().Be(benutzerEditBo1.EmailAddress);
//        }

//        [Fact]
//        public async Task HavingMultipleUnitOfWork_FromSameFactory_SavingBoth_SavesBoth()
//        {
//            var benutzerEditBo1 = new BenutzerEditBo
//            {
//                BenutzerGruppeRefId = RefIdProvider.BenutzerGruppe.FeldkalenderUser,
//                ClientGroupCode = CodesProvider.ClientGroups.Neutral.Value,
//                EmailAddress = "Test1@gmx.ch",
//                FirstName = "Matthias",
//                LastName = "Müller",
//                MobilePhone = "079 762 47 77"
//            };

//            var benutzerEditBo2 = new BenutzerEditBo
//            {
//                BenutzerGruppeRefId = RefIdProvider.BenutzerGruppe.FeldkalenderUser,
//                ClientGroupCode = CodesProvider.ClientGroups.Neutral.Value,
//                EmailAddress = "Test2@gmx.ch",
//                FirstName = "Matthias",
//                LastName = "Müller",
//                MobilePhone = "079 762 47 77"
//            };

//            var uowFactory = AppFactory.Services.GetRequiredService<IUnitOfWorkFactory>();

//            using var uow1 = uowFactory.Create();
//            using var uow2 = uowFactory.Create();

//            var benutzerRepo1 = uow1.GetRepository<IBenutzerRepository>();
//            var benutzerRepo2 = uow1.GetRepository<IBenutzerRepository>();

//            await benutzerRepo1.UpsertBenutzerAsync(benutzerEditBo1, CodesProvider.ClientGroups.Neutral);
//            await benutzerRepo2.UpsertBenutzerAsync(benutzerEditBo2, CodesProvider.ClientGroups.Neutral);

//            await uow1.SaveAsync();
//            await uow2.SaveAsync();

//            var dbContextFactory = (TestAppDbContextFactory)AppFactory.Services.GetRequiredService<IAppDbContextFactory>();
//            using var appDbContext = dbContextFactory.Create();

//            var actualBenutzer = appDbContext.DbSet<Benutzer>().AsNoTracking().ToList();
//            actualBenutzer.Count.Should().Be(2);
//        }

//        [Fact]
//        public async Task HavingMultipleUnitOfWork_FromSameFactory_SavingOne_SavesOne()
//        {
//            var benutzerEditBo1 = new BenutzerEditBo
//            {
//                BenutzerGruppeRefId = RefIdProvider.BenutzerGruppe.FeldkalenderUser,
//                ClientGroupCode = CodesProvider.ClientGroups.Neutral.Value,
//                EmailAddress = "Test1@gmx.ch",
//                FirstName = "Matthias",
//                LastName = "Müller",
//                MobilePhone = "079 762 47 77"
//            };

//            var benutzerEditBo2 = new BenutzerEditBo
//            {
//                BenutzerGruppeRefId = RefIdProvider.BenutzerGruppe.FeldkalenderUser,
//                ClientGroupCode = CodesProvider.ClientGroups.Neutral.Value,
//                EmailAddress = "Test2@gmx.ch",
//                FirstName = "Matthias",
//                LastName = "Müller",
//                MobilePhone = "079 762 47 77"
//            };

//            var uowFactory = AppFactory.Services.GetRequiredService<IUnitOfWorkFactory>();

//            using var uow1 = uowFactory.Create();
//            using var uow2 = uowFactory.Create();

//            var benutzerRepo1 = uow1.GetRepository<IBenutzerRepository>();
//            var benutzerRepo2 = uow2.GetRepository<IBenutzerRepository>();

//            await benutzerRepo1.UpsertBenutzerAsync(benutzerEditBo1, CodesProvider.ClientGroups.Neutral);
//            await benutzerRepo2.UpsertBenutzerAsync(benutzerEditBo2, CodesProvider.ClientGroups.Neutral);

//            await uow1.SaveAsync();

//            var dbContextFactory = (TestAppDbContextFactory)AppFactory.Services.GetRequiredService<IAppDbContextFactory>();
//            using var appDbContext = dbContextFactory.Create();

//            var actualBenutzer = appDbContext.DbSet<Benutzer>().AsNoTracking().ToList();
//            actualBenutzer.Count.Should().Be(1);
//            actualBenutzer.Single().BenutzerEmail.Should().Be(benutzerEditBo1.EmailAddress);
//        }

//        [Fact]
//        public void RequestingMultipleUnitOfWorks_RequestsMultipleUnitOfWorks()
//        {
//            var uowFactory = AppFactory.Services.GetRequiredService<IUnitOfWorkFactory>();

//            using var uow1 = uowFactory.Create();
//            using var uow2 = uowFactory.Create();

//            uow1.Should().BeSameAs(uow1);
//            uow2.Should().BeSameAs(uow2);
//            uow1.Should().NotBeSameAs(uow2);
//        }

//        [Fact]
//        public void RequestingSameRepositoryMultipleTimes_ReturnsSameRepository()
//        {
//            var uowFactory = AppFactory.Services.GetRequiredService<IUnitOfWorkFactory>();
//            using var uow = uowFactory.Create();

//            var individualRepo1 = uow.GetRepository<IBenutzerRepository>();
//            var individualRepo2 = uow.GetRepository<IBenutzerRepository>();

//            individualRepo1.Should().BeSameAs(individualRepo2);
//        }
//    }
//}

