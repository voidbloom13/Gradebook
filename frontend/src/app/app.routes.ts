import { Routes } from '@angular/router';

import { SessionCheck } from './pages/session-check/session-check';
import { Login } from './pages/login/login';
import { Signup } from './pages/signup/signup';
import { Dashboard } from './pages/dashboard/dashboard';

export const routes: Routes = [
    {
        path: '',
        component: SessionCheck,
    },
    {
        path: 'login',
        component: Login,
    },
    {
        path: 'signup',
        component: Signup,
    },
    {
        path: 'dashboard',
        component: Dashboard,
    }
];
