import { Routes } from '@angular/router';
import { authGuard } from '../guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { UserPageComponent } from './user-page/user-page.component';
import { CommandComponent } from './commands/command/command.component';
import { ConnectionComponent } from './user-connections/connection/connection.component';
import { ConnectionsComponent } from './connections/connections.component';
import {CommandsComponent} from './commands/commands.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegistrationComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    {
        path: '',
        component: TopMenuComponent,
        children: [
            { path: 'user-page', component: UserPageComponent },
            { path: 'home', component: HomeComponent },
            { path: "execute", component: CommandComponent },
            { path: "connections", component: ConnectionsComponent},
            { path: "commands", component: CommandsComponent},
            { path: "command", component: CommandComponent },
        ]
    },
];
