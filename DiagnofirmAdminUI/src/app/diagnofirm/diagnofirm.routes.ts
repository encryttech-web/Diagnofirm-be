import { Routes } from '@angular/router';
import { Diagnofirm } from './diagnofirm';
import { Category } from './category/category';
import { Subcategory } from './subcategory/subcategory';
import { Product } from './product/product';
import { Usermaster } from './usermaster/usermaster';
import { Healthcondition } from './healthcondition/healthcondition';
import { Organ } from './organ/organ';
import { Packages } from './packages/packages';
import { Testdirectory } from './testdirectory/testdirectory';
import { Faq } from './faq/faq';
import { Feedback } from './feedback/feedback';
import { Newsletter } from './newsletter/newsletter';
import { Contact } from './contact/contact';

export default [

    // Default page for /diagnofirm
    {
        path: '',
        component: Diagnofirm
    },

    {
        path: 'category',
        component: Category
    },

    {
        path: 'subcategory',
        component: Subcategory
    },

    {
        path: 'healthcondition',
        component: Healthcondition
    },

    {
        path: 'organ',
        component: Organ
    },

    {
        path: 'packages',
        component: Packages
    },

    {
        path: 'product',
        component: Product
    },

    {
        path: 'faq',
        component: Faq
    },

    {
        path: 'testdirectory',
        component: Testdirectory
    },

    {
        path: 'feedback',
        component: Feedback
    },

    {
        path: 'newsletter',
        component: Newsletter
    },

    {
        path: 'contact',
        component: Contact
    },

    {
        path: 'user',
        component: Usermaster
    },

    {
        path: '**',
        redirectTo: '/notfound'
    }

] as Routes;