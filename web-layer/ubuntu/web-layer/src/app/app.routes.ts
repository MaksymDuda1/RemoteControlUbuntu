import { Routes } from '@angular/router';
import { authGuard } from '../guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { UserPageComponent } from './user-page/user-page.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegistrationComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    {
        path: '',
        component: TopMenuComponent,
        children: [
            {path: 'user-page', component: UserPageComponent},
            { path: 'home', component: HomeComponent, canActivate: [authGuard] },
        ]
    },
];
