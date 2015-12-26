﻿/*
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Core.Medication
{
    public class Medication : DomainObject
    {
        public int DrugCode { get; set; }
        public Company Company { get; set; }
        public DrugProduct Product { get; set; }
        public IList<Form> Forms { get; private set; }
        public IList<Ingredient> Ingredients { get; private set; }
        public IList<Package> Packages { get; private set; }
        public IList<Pharmaceutical> Pharmaceuticals { get; private set; }
        public IList<Route> Routes { get; private set; }
        public IList<Schedule> Schedules { get; private set; }
        public IList<Status> Statuses { get; private set; }
        public IList<Therapeutic> Therapeutics { get; private set; }
       
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

        public void AddForm(Form form)
        {
            this.Forms.Add(form);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            this.Ingredients.Add(ingredient);
        }

        public void AddPackage(Package package)
        {
            this.Packages.Add(package);
        }

        public void AddPharmaceutical(Pharmaceutical pharmaceutical)
        {
            this.Pharmaceuticals.Add(pharmaceutical);
        }

        public void AddRoute(Route route)
        {
            this.Routes.Add(route);
        }

        public void AddSchedule(Schedule schedule)
        {
            this.Schedules.Add(schedule);
        }

        public void AddStatus(Status status)
        {
            this.Statuses.Add(status);
        }

        public void AddTherapeutic(Therapeutic therapeutic)
        {
            this.Therapeutics.Add(therapeutic);
        }
    }
}
