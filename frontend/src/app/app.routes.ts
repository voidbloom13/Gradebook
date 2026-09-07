import { Routes } from '@angular/router';

import { SessionCheck } from './pages/session-check/session-check';
import { Login } from './pages/login/login';
import { Dashboard } from './pages/dashboard/dashboard';
import { VerifyEmail } from './pages/user/verify-email/verify-email';
import { ChangePassword } from './pages/user/change-password/change-password';
import { ForgotPassword } from './pages/user/forgot-password/forgot-password';

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
        path: 'dashboard',
        component: Dashboard,
    },
    {
        path: 'user',
        children: [
            { path: 'verify-email', component: VerifyEmail },
            { path: 'change-password', component: ChangePassword },
            { path: 'forgot-password', component: ForgotPassword }
        ]
    }
];
