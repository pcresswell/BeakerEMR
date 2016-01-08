using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Core.Medication;
using Beaker.Repository;
using Beaker.Repository.SQLite.Tables.Medication;
using AutoMapper;
using Beaker.Core;
using Beaker.Core.Authorize;

namespace Beaker.Repository.SQLite
{
    public class MedicationRepository : SQLiteRepository<Medication, MedicationTable>, IMedicationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Repository.SQLite.MedicationRepository"/> class.
        /// </summary>
        public MedicationRepository()
            : base()
        {
        }

        /// <summary>
        /// Finds the medication by drug code.
        /// </summary>
        /// <returns>The by drug code.</returns>
        /// <param name="drugCode">Drug code.</param>
        public Medication FindByDrugCode(int drugCode)
        {
            MedicationTable t = this.Connection.Table<MedicationTable>().Where(m => m.DrugCode == drugCode).SingleOrDefault<MedicationTable>();
            if (t == null)
            {
                return default(Medication);
            }

            return this.Find(t.DomainObjectID);
        }

        /// <summary>
        /// Register this repository against the registrar.
        /// </summary>
        /// <param name="registrar"></param>
        public override void Register(IRepositoryRegistrar registrar)
        {
            registrar.RegisterRepository<IMedicationRepository, Medication>(this);
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            this.Connection.Initialize<CompanyTable>();
            this.Connection.Initialize<DrugProductTable>();
            this.Connection.Initialize<FormTable>();
            this.Connection.Initialize<IngredientTable>();
            this.Connection.Initialize<PackageTable>();
            this.Connection.Initialize<PharmaceuticalTable>();
            this.Connection.Initialize<RouteTable>();
            this.Connection.Initialize<ScheduleTable>();
            this.Connection.Initialize<StatusTable>();
            this.Connection.Initialize<TherapeuticTable>();
        }

        #region implemented abstract members of SQLiteRepository

        protected override void InitializeAutoMapper(IConfiguration autoMapper)
        {
            
            this.CreateTwoWayMap<Company, CompanyTable>(autoMapper);
            this.CreateTwoWayMap<DrugProduct, DrugProductTable>(autoMapper);
            this.CreateTwoWayMap<Form, FormTable>(autoMapper);
            this.CreateTwoWayMap<Ingredient, IngredientTable>(autoMapper);
            this.CreateTwoWayMap<Package, PackageTable>(autoMapper);
            this.CreateTwoWayMap<Pharmaceutical, PharmaceuticalTable>(autoMapper);
            this.CreateTwoWayMap<Route, RouteTable>(autoMapper);
            this.CreateTwoWayMap<Schedule, ScheduleTable>(autoMapper);
            this.CreateTwoWayMap<Status, StatusTable>(autoMapper);
            this.CreateTwoWayMap<Therapeutic, TherapeuticTable>(autoMapper);
        }

        #endregion

        protected override void Insert(Medication persistable)
        {
            MedicationTable medicationTable = new MedicationTable();
            medicationTable.Update(persistable);
            medicationTable.DrugCode = persistable.DrugCode;

            this.Connection.Insert(medicationTable);
            this.CopyDomainObjectProperties(persistable, persistable.Company);
            this.CopyDomainObjectProperties(persistable, persistable.Product);
            persistable.Company.ID = Guid.NewGuid();
            persistable.Product.ID = Guid.NewGuid();

            this.Connection.Insert(Mapper.Map<CompanyTable>(persistable.Company));
            this.Connection.Insert(Mapper.Map<DrugProductTable>(persistable.Product));

            foreach (var form in persistable.Forms)
            {
                this.CopyDomainObjectProperties(persistable, form);
                form.ID = Guid.NewGuid();
                this.Connection.Insert(Mapper.Map<FormTable>(form));
            }

            foreach (var ingredient in persistable.Ingredients)
            {
                this.CopyDomainObjectProperties(persistable, ingredient);
                ingredient.ID = Guid.NewGuid();
                this.Connection.Insert(Mapper.Map<IngredientTable>(ingredient));
            }

            foreach (var package in persistable.Packages)
            {
                this.CopyDomainObjectProperties(persistable, package);
                package.ID = Guid.NewGuid();
                this.Connection.Insert(Mapper.Map<PackageTable>(package));
            }

            foreach (var pharma in persistable.Pharmaceuticals)
            {
                this.CopyDomainObjectProperties(persistable, pharma);
                pharma.ID = Guid.NewGuid();
                this.Connection.Insert(Mapper.Map<PharmaceuticalTable>(pharma));
            }

            foreach (var route in persistable.Routes)
            {
                this.CopyDomainObjectProperties(persistable, route);
                route.ID = Guid.NewGuid();
                this.Connection.Insert(Mapper.Map<RouteTable>(route));
            }

            foreach (var schedule in persistable.Schedules)
            {
                this.CopyDomainObjectProperties(persistable, schedule);
                schedule.ID = Guid.NewGuid();
                this.Connection.Insert(Mapper.Map<ScheduleTable>(schedule));
            }

            foreach (var status in persistable.Statuses)
            {
                this.CopyDomainObjectProperties(persistable, status);
                status.ID = Guid.NewGuid();
                this.Connection.Insert(Mapper.Map<StatusTable>(status));
            }

            foreach (var therapeutic in persistable.Therapeutics)
            {
                this.CopyDomainObjectProperties(persistable, therapeutic);
                therapeutic.ID = Guid.NewGuid();
                this.Connection.Insert(Mapper.Map<TherapeuticTable>(therapeutic));
            }
        }

        protected override void Update(Medication persistable)
        {
            MedicationTable medicationTable = this.Connection.Get<MedicationTable>(persistable.ID);
            medicationTable.Update(persistable);
            this.Connection.Update(medicationTable);
        }

        private void CopyDomainObjectProperties(IPersistable source, IPersistable destination)
        {
            destination.AuthorID = source.AuthorID;
            destination.RecordEndDateTime = source.RecordEndDateTime;
            destination.RecordStartDateTime = source.RecordStartDateTime;
            destination.ValidEndDateTime = source.ValidEndDateTime;
            destination.ValidStartDateTime = source.ValidStartDateTime;
        }

        #region implemented abstract members of SQLiteRepository

        protected override void CustomMappingToPersistable(Medication persistable, MedicationTable table)
        {
            DateTime onDateTime = persistable.RecordEndDateTime;

            AssignCompany(onDateTime, persistable);
            AssignDrugProduct(onDateTime, persistable);
            AssignForm(onDateTime, persistable);
            AssignIngredients(onDateTime, persistable);
            AssignPackages(onDateTime, persistable);
            AssignPharmaceuticals(onDateTime, persistable);
            AssignRoutes(onDateTime, persistable);
            AssignSchedules(onDateTime, persistable);
            AssignStatuses(onDateTime, persistable);
            AssignTherapeutics(onDateTime, persistable);
        }

        #endregion

        public override Medication Find(Guid domainObjectID, DateTime onDateTime)
        {
            MedicationTable table = this.Connection.Table<MedicationTable>().Where(t => t.DomainObjectID == domainObjectID && t.RecordStartDateTime <= onDateTime && t.RecordEndDateTime >= onDateTime).SingleOrDefault<MedicationTable>();
            if (table == null)
            {
                return default(Medication);
            }

            return this.Get(table.ID);
        }

        public override Medication Get(Guid id)
        {
            MedicationTable table = this.Connection.Get<MedicationTable>(id);
            Medication medication = this.CreateMedication(table, DateTime.UtcNow);
            return medication;
        }

        #region implemented abstract members of SQLiteRepository

        protected override void CustomMappingToTable(MedicationTable table, Medication persistable)
        {
            // nothing to do.
        }

        #endregion

        #region Assignments

        private void AssignTherapeutics(DateTime onDateTime, Medication medication)
        {
            IList<TherapeuticTable> therapeuticTables = this.Connection.Table<TherapeuticTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime ).ToList();

            foreach (var therapeuticTable in therapeuticTables)
            {
                Therapeutic therapeutic = Mapper.Map<Therapeutic>(therapeuticTable);
                medication.AddTherapeutic(therapeutic);
            }
        }

        private Medication CreateMedication(MedicationTable medicationTable, DateTime onDateTime)
        {
            // Create the medication object
            Medication medication = new Medication();
            medicationTable.CopyTo(medication);
            medication.DrugCode = medicationTable.DrugCode;

            // Now assign the associated values.

            AssignCompany(onDateTime, medication);
            AssignDrugProduct(onDateTime, medication);
            AssignForm(onDateTime, medication);
            AssignIngredients(onDateTime, medication);
            AssignPackages(onDateTime, medication);
            AssignPharmaceuticals(onDateTime, medication);
            AssignRoutes(onDateTime, medication);
            AssignSchedules(onDateTime, medication);
            AssignStatuses(onDateTime, medication);
            AssignTherapeutics(onDateTime, medication);

            return medication;
        }

        private void AssignStatuses(DateTime onDateTime, Medication medication)
        {
            IList<StatusTable> statusTables = this.Connection.Table<StatusTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime).ToList();

            foreach (var statusTable in statusTables)
            {
                Status status = Mapper.Map<Status>(statusTable);
                medication.AddStatus(status);
            }
        }

        private void AssignSchedules(DateTime onDateTime, Medication medication)
        {
            IList<ScheduleTable> scheduleTables = this.Connection.Table<ScheduleTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime ).ToList();

            foreach (var scheduleTable in scheduleTables)
            {
                Schedule schedule = Mapper.Map<Schedule>(scheduleTable);
                medication.AddSchedule(schedule);
            }
        }

        private void AssignRoutes(DateTime onDateTime, Medication medication)
        {
            IList<RouteTable> routeTables = this.Connection.Table<RouteTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime ).ToList();

            foreach (var routeTable in routeTables)
            {
                Route route = Mapper.Map<Route>(routeTable);
                medication.AddRoute(route);
            }
        }

        private void AssignPharmaceuticals(DateTime onDateTime, Medication medication)
        {
            IList<PharmaceuticalTable> pharmaceuticalTables = this.Connection.Table<PharmaceuticalTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime ).ToList();

            foreach (var pharmaceuticalTable in pharmaceuticalTables)
            {
                Pharmaceutical pharma = Mapper.Map<Pharmaceutical>(pharmaceuticalTable);
                medication.AddPharmaceutical(pharma);
            }
        }

        private void AssignPackages(DateTime onDateTime, Medication medication)
        {
            IList<PackageTable> packageTables = this.Connection.Table<PackageTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime ).ToList();

            foreach (var packageTable in packageTables)
            {
                Package package = Mapper.Map<Package>(packageTable);
                medication.AddPackage(package);
            }
        }

        private void AssignIngredients(DateTime onDateTime, Medication medication)
        {
            IList<IngredientTable> ingredientTables = this.Connection.Table<IngredientTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime ).ToList();

            foreach (var ingredientTable in ingredientTables)
            {
                Ingredient ingredient = Mapper.Map<Ingredient>(ingredientTable);
                medication.AddIngredient(ingredient);
            }
        }

        private void AssignForm(DateTime onDateTime, Medication medication)
        {
            IList<FormTable> formTables = this.Connection.Table<FormTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime).ToList();

            foreach (var formTable in formTables)
            {
                Form form = Mapper.Map<Form>(formTable);
                medication.AddForm(form);
            }
        }

        private void AssignDrugProduct(DateTime onDateTime, Medication medication)
        {
            IList<DrugProductTable> drugProductTables = this.Connection.Table<DrugProductTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime).ToList();

            foreach (var drugProductTable in drugProductTables)
            {
                DrugProduct drugProduct = Mapper.Map<DrugProduct>(drugProductTable);
                medication.Product = drugProduct;
            }
        }

        private void AssignCompany(DateTime onDateTime, Medication medication)
        {
            IList<CompanyTable> companyTables = this.Connection.Table<CompanyTable>()
                .Where(c => c.DrugCode == medication.DrugCode && c.RecordStartDateTime <= onDateTime && c.RecordEndDateTime >= onDateTime).ToList();

            foreach (var companyTable in companyTables)
            {
                Company company = Mapper.Map<Company>(companyTable);
                medication.Company = company;
            }
        }

        #endregion
    }
}
