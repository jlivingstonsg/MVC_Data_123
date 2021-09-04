﻿using MVCBasics.Models;
using MVCBasics.Repository;
using System;
using System.Linq;

namespace MVCBasics.Services
{
    public class PeopleService : IPeopleService
    {
        InMemoryPeopleRepo PeopleDatabase = new InMemoryPeopleRepo();
        public Person Add(CreatePersonViewModel person)
        {
            PeopleDatabase.Create(person.Name, person.PhoneNumber, person.city);
            return person.Model;
        }
        PeopleViewModel pvm = new PeopleViewModel();
        public PeopleViewModel All()
        {
            var people = PeopleDatabase.Read();
            pvm.people = people;
            return pvm;
        }

        public Person Edit(int ID, Person person)
        {
            return PeopleDatabase.Update(person);
        }

        public PeopleViewModel FindBy(PeopleViewModel Search)
        {
            string[] parameters = Search.SearchPhrase.Split(new char[' ']);
            var people = PeopleDatabase.Read();
            pvm.people = people.Where(person => parameters.Any(param =>
                person.Name.Contains(param) ||
                person.PhoneNumber.ToString().Contains(param) ||
                person.city.Contains(param)
                )).ToList();
            return pvm;
        }

        public Person FindBy(int ID)
        {
            return PeopleDatabase.Read(ID);
        }

        public bool Remove(int ID)
        {
            var people = PeopleDatabase.Read();
            var person = people.Where(p => p.ID==ID).FirstOrDefault();
            return PeopleDatabase.Delete(person);
        }
    }
}
