using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beaker.Repository;
using Beaker.Core.Medication;

namespace Beaker.Update.Medication.June2015
{
    public class Medication20150601Migration : Migration
    {
        protected override void ApplyMigration(IMigratable migratableDatabase)
        {
            DrugProductList productList = new DrugProductList();
            CompanyList companyList = new CompanyList();
            FormList formList = new FormList();
            IngredientList ingredientList = new IngredientList();
            PackageList packageList = new PackageList();
            PharmaceuticalList pharmaceuticalList = new PharmaceuticalList();
            RouteList routeList = new RouteList();
            ScheduleList sheduleList = new ScheduleList();
            StatusList statusList = new StatusList();
            TherapeuticList therapeuticalList = new TherapeuticList();
          
            foreach (var product in productList)
            {
                Beaker.Core.Medication.Medication medication = new Core.Medication.Medication();
                int drugCode = product.DRUG_CODE;
                Beaker.Update.Medication.Company company = companyList.Where(c => c.DRUG_CODE == drugCode).SingleOrDefault();
                AssignCompany(medication, company);
                AssignDrugProduct(productList, medication, drugCode);
                AssignForms(formList, medication, drugCode);
                AssignIngredients(ingredientList, medication, drugCode);
                AssignPackages(packageList, medication, drugCode);
                AssignPharmaceuticals(pharmaceuticalList, medication, drugCode);
                AssignRoutes(routeList, medication, drugCode);
                AssignSchedules(sheduleList, medication, drugCode);
                AssignStatuses(statusList, medication, drugCode);
                AssignTherapeutics(therapeuticalList, medication, drugCode);

                IMedicationRepository medicationRepository = migratableDatabase.Repository<IMedicationRepository>();
                medicationRepository.Save(medication);
            }
        }

        private void AssignTherapeutics(TherapeuticList therapeuticalList, Core.Medication.Medication medication, int drugCode)
        {
            IList<Beaker.Update.Medication.Therapeutic> sourceTherapeutics = therapeuticalList.Where(t => t.DRUG_CODE == drugCode).ToList();
            foreach (var sourceTherapeutic in sourceTherapeutics)
            {
                Beaker.Core.Medication.Therapeutic therapeutic = new Core.Medication.Therapeutic(sourceTherapeutic.DRUG_CODE, sourceTherapeutic.TC_ATC_NUMBER, sourceTherapeutic.TC_ATC, sourceTherapeutic.TC_AHFS_NUMBER, sourceTherapeutic.TC_AHFS);
                medication.AddTherapeutic(therapeutic);
            }
        }

        private void AssignStatuses(StatusList statusList, Core.Medication.Medication medication, int drugCode)
        {
            IList<Beaker.Update.Medication.Status> sourceStatuses = statusList.Where(s => s.DRUG_CODE == drugCode).ToList<Status>();
            foreach (var sourceStatus in sourceStatuses)
            {
                Beaker.Core.Medication.Status status = new Core.Medication.Status(sourceStatus.DRUG_CODE, sourceStatus.CURRENT_STATUS_FLAG, sourceStatus.STATUS, sourceStatus.HISTORY_DATE);
                medication.AddStatus(status);
            }
        }

        private void AssignSchedules(ScheduleList sheduleList, Core.Medication.Medication medication, int drugCode)
        {
            IList<Schedule> sourceSchedules = sheduleList.Where(s => s.DRUG_CODE == drugCode).ToList();
            foreach (var sourceSchedule in sourceSchedules)
            {
                Beaker.Core.Medication.Schedule schedule = new Core.Medication.Schedule(sourceSchedule.DRUG_CODE, sourceSchedule.SCHEDULE);
                medication.AddSchedule(schedule);
            }
        }

        private void AssignRoutes(RouteList routeList, Core.Medication.Medication medication, int drugCode)
        {
            IList<Route> sourceRoutes = routeList.Where(r => r.DRUG_CODE == drugCode).ToList<Route>();
            foreach (var sourceRoute in sourceRoutes)
            {
                Beaker.Core.Medication.Route route = new Core.Medication.Route(sourceRoute.DRUG_CODE, sourceRoute.ROUTE_OF_ADMINISTRATION_CODE, sourceRoute.ROUTE_OF_ADMINISTRATION);
                medication.AddRoute(route);
            }
        }

        private void AssignPharmaceuticals(PharmaceuticalList pharmaceuticalList, Core.Medication.Medication medication, int drugCode)
        {
            IList<Pharmaceutical> sourcePharmaceuticals = pharmaceuticalList.Where(x => x.DRUG_CODE == drugCode).ToList<Pharmaceutical>();
            foreach (var sourcePharmaceutical in sourcePharmaceuticals)
            {
                Beaker.Core.Medication.Pharmaceutical pharmaceutical = new Core.Medication.Pharmaceutical(sourcePharmaceutical.DRUG_CODE, sourcePharmaceutical.PHARMACEUTICAL_STD);
                medication.AddPharmaceutical(pharmaceutical);
            }
        }

        private void AssignPackages(PackageList packageList, Core.Medication.Medication medication, int drugCode)
        {
            IList<Beaker.Update.Medication.Package> sourcePackages = packageList.Where<Package>(x => x.DRUG_CODE == drugCode).ToList<Package>();
            foreach (var sourcePackage in sourcePackages)
            {
                Beaker.Core.Medication.Package package = new Core.Medication.Package(sourcePackage.DRUG_CODE, sourcePackage.UPC, sourcePackage.PACKAGE_SIZE_UNIT, sourcePackage.PACKAGE_TYPE, sourcePackage.PACKAGE_SIZE, sourcePackage.PRODUCT_INFORMATION);
                medication.AddPackage(package);
            }
        }

        private void AssignIngredients(IngredientList ingredientList, Core.Medication.Medication medication, int drugCode)
        {
            List<Beaker.Update.Medication.Ingredient> sourceIngredients = ingredientList.Where<Ingredient>(i => i.DRUG_CODE == drugCode).ToList<Ingredient>();
            foreach (var sourceIngredient in sourceIngredients)
            {
                Beaker.Core.Medication.Ingredient ingredient = new Core.Medication.Ingredient(sourceIngredient.DRUG_CODE, sourceIngredient.ACTIVE_INGREDIENT_CODE, sourceIngredient.INGREDIENT, sourceIngredient.INGREDIENT_SUPPLIED_IND, sourceIngredient.STRENGTH, sourceIngredient.STRENGTH_UNIT, sourceIngredient.STRENGTH_TYPE, sourceIngredient.DOSAGE_VALUE, sourceIngredient.BASE, sourceIngredient.DOSAGE_UNIT, sourceIngredient.NOTES);
                medication.AddIngredient(ingredient);
            }
        }

        private void AssignForms(FormList formList, Core.Medication.Medication medication, int drugCode)
        {
            IList<Form> sourceForms = formList.Where(f => f.DRUG_CODE == drugCode).ToList<Form>();
            foreach (var sourceForm in sourceForms)
            {
                Beaker.Core.Medication.Form form = new Core.Medication.Form(sourceForm.DRUG_CODE, sourceForm.PHARM_FORM_CODE, sourceForm.PHARMACEUTICAL_FORM);
                medication.AddForm(form);
            }
        }

        private void AssignDrugProduct(DrugProductList productList, Core.Medication.Medication medication, int drugCode)
        {
            Beaker.Update.Medication.DrugProduct drugProduct = productList.Where(pr => pr.DRUG_CODE == drugCode).First();

            Beaker.Core.Medication.DrugProduct p = new Beaker.Core.Medication.DrugProduct(
                drugProduct.DRUG_CODE,
                drugProduct.PRODUCT_CATEGORIZATION,
                drugProduct.CLASS,
                drugProduct.DRUG_IDENTIFICATION_NUMBER,
                drugProduct.BRAND_NAME,
                drugProduct.DESCRIPTOR,
                drugProduct.PEDIATRIC_FLAG,
                drugProduct.ACCESSION_NUMBER,
                drugProduct.NUMBER_OF_AIS,
                drugProduct.LAST_UPDATE_DATE,
                drugProduct.AI_GROUP_NO);

            medication.Product = p;
        }

        private void AssignCompany(Core.Medication.Medication medication, Beaker.Update.Medication.Company company)
        {
            if (company == null) return;

            Beaker.Core.Medication.Company c = new Beaker.Core.Medication.Company(
                                company.DRUG_CODE,
                                company.MFR_CODE,
                                company.COMPANY_CODE,
                                company.COMPANY_NAME,
                                company.COMPANY_TYPE,
                                company.ADDRESS_MAILING_FLAG,
                                company.ADDRESS_BILLING_FLAG,
                                company.ADDRESS_NOTIFICATION_FLAG,
                                company.ADDRESS_OTHER,
                                company.SUITE_NUMBER,
                                company.STREET_NAME,
                                company.CITY_NAME,
                                company.PROVINCE,
                                company.COUNTRY,
                                company.POSTAL_CODE,
                                company.POST_OFFICE_BOX);

            medication.Company = c;
        }
    }
}
