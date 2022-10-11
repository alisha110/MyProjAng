import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterNewUserComponent } from './register-new-user/register-new-user.component';
import { UserListComponent } from './user-list/user-list.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SetActivatioUserComponent } from './set-activatio-user/set-activatio-user.component';
import { ReportComponent } from './report/report.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'Login', component: LoginComponent },
  { path: 'Login/:RDPage', component: LoginComponent },
  { path: 'list-new-users', component: UserListComponent },
  { path: 'register-new-user/:id', component: RegisterNewUserComponent },
  { path: 'aap-header', component: HeaderComponent },
  { path: 'app-footer', component: FooterComponent },
  { path: 'Set-activation-user', component: SetActivatioUserComponent },
  { path: 'Report', component: ReportComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
