/*
The MIT License (MIT)

Copyright (c) 2015 Peter Cresswell (pcresswell@gmail.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Beaker.Core.Medication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Medication.
    /// </summary>
    public class Medication : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Medication"/> class.
        /// </summary>
        public Medication()
        {
            this.Ingredients = new List<Ingredient>();
            this.Forms = new List<Form>();
            this.Packages = new List<Package>();
            this.Pharmaceuticals = new List<Pharmaceutical>();
            this.Routes = new List<Route>();
            this.Schedules = new List<Schedule>();
            this.Statuses = new List<Status>();
            this.Therapeutics = new List<Therapeutic>();
        }

        /// <summary>
        /// Gets or sets the drug code.
        /// </summary>
        /// <value>The drug code.</value>
        public int DrugCode { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        public DrugProduct Product { get; set; }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>The forms.</value>
        public IList<Form> Forms { get; private set; }

        /// <summary>
        /// Gets the ingredients.
        /// </summary>
        /// <value>The ingredients.</value>
        public IList<Ingredient> Ingredients { get; private set; }

        /// <summary>
        /// Gets the packages.
        /// </summary>
        /// <value>The packages.</value>
        public IList<Package> Packages { get; private set; }

        /// <summary>
        /// Gets the pharmaceuticals.
        /// </summary>
        /// <value>The pharmaceuticals.</value>
        public IList<Pharmaceutical> Pharmaceuticals { get; private set; }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <value>The routes.</value>
        public IList<Route> Routes { get; private set; }

        /// <summary>
        /// Gets the schedules.
        /// </summary>
        /// <value>The schedules.</value>
        public IList<Schedule> Schedules { get; private set; }

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        /// <value>The statuses.</value>
        public IList<Status> Statuses { get; private set; }

        /// <summary>
        /// Gets the therapeutics.
        /// </summary>
        /// <value>The therapeutics.</value>
        public IList<Therapeutic> Therapeutics { get; private set; }

        /// <summary>
        /// Adds the form.
        /// </summary>
        /// <param name="form">Form.</param>
        public void AddForm(Form form)
        {
            this.Forms.Add(form);
        }

        /// <summary>
        /// Adds the ingredient.
        /// </summary>
        /// <param name="ingredient">Ingredient.</param>
        public void AddIngredient(Ingredient ingredient)
        {
            this.Ingredients.Add(ingredient);
        }

        /// <summary>
        /// Adds the package.
        /// </summary>
        /// <param name="package">Package.</param>
        public void AddPackage(Package package)
        {
            this.Packages.Add(package);
        }

        /// <summary>
        /// Adds the pharmaceutical.
        /// </summary>
        /// <param name="pharmaceutical">Pharmaceutical.</param>
        public void AddPharmaceutical(Pharmaceutical pharmaceutical)
        {
            this.Pharmaceuticals.Add(pharmaceutical);
        }

        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <param name="route">Route.</param>
        public void AddRoute(Route route)
        {
            this.Routes.Add(route);
        }

        /// <summary>
        /// Adds the schedule.
        /// </summary>
        /// <param name="schedule">Schedule.</param>
        public void AddSchedule(Schedule schedule)
        {
            this.Schedules.Add(schedule);
        }

        /// <summary>
        /// Adds the status.
        /// </summary>
        /// <param name="status">Status.</param>
        public void AddStatus(Status status)
        {
            this.Statuses.Add(status);
        }

        /// <summary>
        /// Adds the therapeutic.
        /// </summary>
        /// <param name="therapeutic">Therapeutic.</param>
        public void AddTherapeutic(Therapeutic therapeutic)
        {
            this.Therapeutics.Add(therapeutic);
        }
    }
}